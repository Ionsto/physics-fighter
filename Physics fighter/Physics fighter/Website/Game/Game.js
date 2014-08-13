$(document).ready(function () {
    function Entity(xa, ya,xb, yb, colour) {
        this.XA = xa;
        this.YA = ya;
        this.XB = xb;
        this.YB = yb;
        this.Colour = colour;
    }
    function RenderEntity(entity)
    {
        ctx.beginPath();
        ctx.moveTo(entity.XA, entity.YA);
        ctx.lineTo(entity.XB, entity.YB);
        ctx.stroke();
    }
    var game = $.connection.gameHub;
    game.client.setEntityFrame = function SetObjectFrame(id, frame, posxa, posya, posxb, posyb, colour) {
        Frames[frame][id] = Entity(posx, posy, posxb,posyb, colour);
    };
    function RenderFrameSet()
    {
        ctx.clearRect(0, 0, 500, 500);
    }
    var CurrentFrame = 0;
    var FrameCount = 300;
    var ObjectCount = 12;
    var Frames = new Array(FrameCount);
    for (var i = 0; i < FrameCount; ++i)
    {
        Frames[i] = new Array(ObjectCount);
        for (var j = 0; j < ObjectCount;++j)
        {
            Frames[i][j] = null;
        }
    }
    var Canvas = document.getElementById("Render");
    var ctx = Canvas.getContext("2d");
    //ctx.fillRect(0, 0, 100, 100);
    RenderEntity(new Entity(0,0,100,100,"#FFFFFF"));
});