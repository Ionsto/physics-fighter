function Object(xa, ya,xb, yb, colour) {
    this.XA = xa;
    this.YA = ya;
    this.XB = xb;
    this.YB = yb;
    this.Colour = colour;
}
var game = $.connection.chatHub;
var CurrentFrame = 0;
var FrameCount = 300;
var ObjectsCount = 12;
var Frames = new Array(FrameCount);
for (var i = 0; i < FrameCount; ++i)
{
    Frames[i] = new Array(ObjectCount);
    for (var j = 0; j < ObjectsCount;++j)
    {
        Frames[i][j] = null;
    }
}
var Canvas = document.getElementById("Render");
var ctx = Canvas.getContext("2d");
ctx.fillRect(0, 0, 100, 100);

chat.client.setObjectFrame = function SetObjectFrame(id, frame, posxa, posya, posx b, posyb, size, colour)
{
    Frames[frame][id] = Object(posx, posy, posrotation, size, colour);
}
function RenderObject(object)
{
    CanvasRenderingContext2D.prototype.moveTo();
    ctx.beginPath();
    ctx.moveTo(object.X, object.Y);
    ctx.lineTo(object.X, object.Y);
    ctx.stroke();
}
function RenderFrameSet()
{
    ctx.clearRect(0, 0, 500, 500);

}