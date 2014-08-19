(function ($) {
    $.QueryString = (function (a) {
        if (a == "") return {};
        var b = {};
        for (var i = 0; i < a.length; ++i) {
            var p = a[i].split('=');
            if (p.length != 2) continue;
            b[p[0]] = decodeURIComponent(p[1].replace(/\+/g, " "));
        }
        return b;
    })(window.location.search.substr(1).split('&'))
})(jQuery);
$(document).ready(function () {
    var CurrentFrame;
    var FrameCount;
    var ObjectCount;
    var Frames;
    var Canvas;
    var ctx;
    var PlayerName = $.QueryString["name"];
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
        //2 = Contract 
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
        var Joints = new Array(count);
        for(var i = 0;i < count;++i)
        {
            Joints[i] = new Joint(a[i],b[i],mid[i],x[i], y[i], colour[i], state[i]);
        }
        RenderJoints(Joints);
    };
    document.getElementById("Ready").onclick = function ()
    {
        if (document.getElementById("Ready").value === "Ready") {
            game.server.readyState(PlayerName, new Array(), new Array());
            document.getElementById("Ready").value = "Readyed";
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
        var Size = 5;
        for (var i = 0; i < joints.length;++i)
        {
            ctx.fillRect(joints[i].X - Size, joints[i].Y - Size, joints[i].X + Size, joints[i].Y + Size);
        }
    };
    function RenderFrames(FrameStart, frames, replay) {
        RenderFrame(FrameStart + CurrentFrameOn);
        if (++CurrentFrameOn < frames) {
            document.getElementById("Ready").disabled = true;
            setTimeout(function () { RenderFrames(FrameStart, frames, replay); }, 50);
            document.getElementById("Frame").innerHTML = "Frame:" + (FrameStart + CurrentFrameOn);
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
    };
    $.connection.hub.start().done(function ()
    {
        game.server.requestSettings();
    });
});