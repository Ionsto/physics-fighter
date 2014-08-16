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
    var PlayerName = $.QueryString["name"];
    function Connection(xa, ya, xb, yb, colour) {
        this.XA = xa;
        this.YA = ya;
        this.XB = xb;
        this.YB = yb;
        this.Colour = colour;
    }
    function RenderEntity(connection)
    {
        ctx.beginPath();
        ctx.moveTo(connection.XA, connection.YA);
        ctx.lineTo(connection.XB, connection.YB);
        ctx.stroke();
    }
    var game = $.connection.gameHub;
    game.client.setObjectFrame = function SetObjectFrame(id, frame, posxa, posya, posxb, posyb, colour) {
        Frames[frame][id] = new Connection(500-posxa, 500-posya, 500-posxb, 500-posyb, colour);
    };
    game.client.setPlayerList = function (names)
    {
        if (names.length == 0)
        {
            document.getElementById("Ready").value = "Ready";
        }
        document.getElementById("ReadyPlayerDisplay").innerHTML = "";
        for (var i = 0; i < names.length; ++i) {
                document.getElementById("ReadyPlayerDisplay").innerHTML += names[i] + " ";
        }
    };
    document.getElementById("Ready").onclick = function ()
    {
        if (document.getElementById("Ready").value === "Ready") {
            game.server.readyState(PlayerName, new Array(), new Array());
            document.getElementById("Ready").value = "Readyed";
        }
    };
    game.client.renderFrameSet = function RenderFrameSet(framestart)
    {
        ctx.clearRect(0, 0, 500, 500);
        for (var f = framestart; f < framestart + 30;++f)
        {
            for (var i = 0; i < ObjectCount;++i)
            {
                if (Frames[f][i] != null) {
                    RenderEntity(Frames[f][i]);
                }
            }
        }
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
    $.connection.hub.start().done(function ()
    {

    });
});