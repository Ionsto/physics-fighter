$(document).ready(function () {
    var CurrentFrame;
    var FrameCount;
    var ObjectCount;
    var Frames;
    var Canvas;
    var ctx;
    var PlayerName = document.getElementById("GameInfo").getAttribute("data-name");
    var JointChange = false;
    var JointSize = 5;
    var Joints;
    if (PlayerName == "Spec")
    {
        document.getElementById("Ready").disabled = true;
    }
    function Joint(a,b,mid,x, y, colour, state) {
        this.PointA = a;//Point A to actuate on
        this.PointB = b;//Point B to actuate on
        this.MidPoint = mid;//The point in between
        this.X = x;
        this.Y = y;
        this.Colour = colour;
        //0 = Relax
        //1 = Hold
        //2 = Extend
        //3 = Contract ClockWise
        //4 = Contract Counter ClockWise
        this.State = state;
    };
    function Connection(xa, ya, xb, yb, colour) {
        this.XA = xa;
        this.YA = ya;
        this.XB = xb;
        this.YB = yb;
        this.Colour = colour;
    };
    function RenderEntity(connection)
    {
        ctx.fillStyle = connection.Colour;
        ctx.beginPath();
        ctx.moveTo(connection.XA, connection.YA);
        ctx.lineTo(connection.XB, connection.YB);
        ctx.stroke();
    }
    var game = $.connection.gameHub;
    game.client.gameOver = function GameOver() {
        document.getElementById("Ready").disabled = true;
        DoReplay();
        document.getElementById("Replay").onclick = DoReplay;
    };
    game.client.setObjectFrame = function SetObjectFrame(id, frame, posxa, posya, posxb, posyb, colour) {
        Frames[frame][id] = new Connection(500-posxa, 500-posya, 500-posxb, 500-posyb, colour);
    };
    game.client.setPlayerList = function (names)
    {
        if (names.length == 0 && PlayerName != "Spec")
        {
            document.getElementById("Ready").value = "Ready";
        }
        document.getElementById("ReadyPlayerDisplay").innerHTML = "";
        for (var i = 0; i < names.length; ++i) {
                document.getElementById("ReadyPlayerDisplay").innerHTML += names[i] + " ";
        }
    };
    game.client.sendJoints = function SendJoints(count,a,b,mid,x, y, colour, state)
    {
        Joints = new Array(count);
        for(var i = 0;i < count;++i)
        {
            Joints[i] = new Joint(a[i],b[i],mid[i],500 - x[i],500 - y[i], colour[i], state[i]);
        }
        RenderJoints(Joints);
        JointChange = true;
    };
    document.getElementById("Ready").onclick = function ()
    {
        if (document.getElementById("Ready").value === "Ready") {
            var A = new Array(Joints.length);
            var Mid = new Array(Joints.length);
            var B = new Array(Joints.length);
            var State = new Array(Joints.length);
            for (var i = 0; i < Joints.length;++i)
            {
                A[i] = Joints[i].PointA;
                Mid[i] = Joints[i].MidPoint;
                B[i] = Joints[i].PointB;
                State[i] = Joints[i].State;
            }
            game.server.readyState(PlayerName, A,Mid,B,State);
            document.getElementById("Ready").value = "Readyed";
            JointChange = false;
        }
    };
    var CurrentFrameOn = 0;
    function RenderFrame(frame) {
        ctx.clearRect(0, 0, 500, 500);
        for (var i = 0; i < ObjectCount; ++i) {
            if (Frames[frame][i] != null) {
                RenderEntity(Frames[frame][i]);
            }
        }
    }
    function RenderJoints(joints) {
        //ctx.clearRect(0, 0, 500, 500);
        for (var i = 0; i < joints.length;++i)
        {
            ctx.fillStyle = "#000000";
            ctx.fillRect(joints[i].X - JointSize, joints[i].Y - JointSize,2 * JointSize, 2 * JointSize);
        }
    };
    function RenderFrames(FrameStart, frames, replay) {
        RenderFrame(FrameStart + CurrentFrameOn);
        document.getElementById("Frame").innerHTML = "Frame:" + (FrameStart + CurrentFrameOn);
        if (++CurrentFrameOn < frames) {
            document.getElementById("Ready").disabled = true;
            setTimeout(function () { RenderFrames(FrameStart, frames, replay); }, 50);
        }
        else {
            if (replay) {
                document.getElementById("Replay").disabled = false;
            }
            else {
                if (PlayerName != "Spec") {
                    document.getElementById("Ready").disabled = false;
                    game.server.requestJoints(PlayerName);//this is called when a new game stage starts
                }
            }
            CurrentFrame = FrameStart + CurrentFrameOn;
            CurrentFrameOn = 0;
        }
    };
    function DoReplay() {
        document.getElementById("Replay").disabled = true;
        RenderFrames(0, FrameCount, true);
    }
    game.client.renderFrameSet = function RenderFrameSet(framestart, frames) {
        RenderFrames(framestart, frames,false);
    };
    game.client.initSettings = function InitSettings(settings) {
        CurrentFrame = 0;
        FrameCount = settings[0];
        ObjectCount = settings[1];
        Frames = new Array(FrameCount);
        for (var i = 0; i < FrameCount; ++i) {
            Frames[i] = new Array(ObjectCount);
            for (var j = 0; j < ObjectCount; ++j) {
                Frames[i][j] = null;
            }
        }
        Canvas = document.getElementById("Render");
        ctx = Canvas.getContext("2d");
        $("#Render").on("click",MouseClickCanvas);
        $(document).on("keypress", KeyDown);
    };
    function MouseClickCanvas(event) {
        if(PlayerName != "Spec")
        {
            if(JointChange)
            {
                var mousex = event.pageX - $('#Render').offset().left;
                var mousey = event.pageY - $('#Render').offset().top;
                InteractJoint = -1;
                for (var i = 0; i < Joints.length;++i)
                {
                    if (Math.abs(Joints[i].X - mousex) < JointSize && Math.abs(Joints[i].Y - mousey) < JointSize)
                    {
                        InteractJoint = i;
                        SetJointOutput(InteractJoint);
                        return;
                    }
                }
                document.getElementById("State").innerHTML = "State:";
            }
        }
    };
    function KeyDown(event) {
        if(PlayerName !="Spec" && JointChange)
        {
            if (event.which == 99)
            {
                var IsRel = true;
                for(var i = 0;i < Joints.length;++i)
                {
                    if(Joints[i].State != 0)
                    {
                        IsRel = false;
                    }
                }
                if(IsRel)
                {
                    for (var i = 0; i < Joints.length; ++i) {
                        Joints[i].State = 1;
                    }
                }
                else
                {
                    for (var i = 0; i < Joints.length; ++i) {
                        Joints[i].State = 0;
                    }
                }
            }
            if(InteractJoint != -1)
            {
                if (event.which == 97) {//A
                    if(--Joints[InteractJoint].State < 0)
                    {
                        Joints[InteractJoint].State = 4;
                    }
                }
                if (event.which == 100) {//D
                    if (++Joints[InteractJoint].State > 4)
                    {
                        Joints[InteractJoint].State = 0;
                    }
                }
                SetJointOutput(InteractJoint);
            }
        }
    };
    function SetJointOutput(joint) {
        var Message = "";
        if (Joints[InteractJoint].State == 0) {
            Message = "Relax";
        } if (Joints[InteractJoint].State == 1) {
            Message = "Hold";
        } if (Joints[InteractJoint].State == 2) {
            Message = "Extend";
        } if (Joints[InteractJoint].State == 3) {
            Message = "Contract CounterClockwise";
        } if (Joints[InteractJoint].State == 4) {
            Message = "Contract Clockwise";
        }
        document.getElementById("State").innerHTML = "State:" + Message;

    };
    $.connection.hub.start().done(function ()
    {
        game.server.requestSettings();
        if(PlayerName != "Spec")
        {
            game.server.requestJoints(PlayerName);
        }
        
    });
});