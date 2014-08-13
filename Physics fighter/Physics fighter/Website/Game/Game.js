function Object(x,y,r,size,colour) {
    this.X = x;
    this.Y = y;
    this.R = r;
    this.Size = size;
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

chat.client.setObjectFrame = function SetObjectFrame(id, frame, posx, posy, posrotation, size, colour)
{
    Frames[frame][id] = Object(posx, posy, posrotation, size, colour);
}
function RenderObject(object)
{

}
function RenderFrameSet()
{
    ctx.clearRect(0, 0, 500, 500);

}