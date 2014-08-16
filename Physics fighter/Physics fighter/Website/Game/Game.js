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
        Frames[frame][id] = new Connection(posxa, posya, posxb, posyb, colour);
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
    var CurrentFrameOn = 0;
    function RenderFrames(FrameStart, frames) {
        ctx.clearRect(0, 0, 500, 500);
        for (var i = 0; i < ObjectCount; ++i) {
            if (Frames[FrameStart + CurrentFrameOn][i] != null) {
                RenderEntity(Frames[FrameStart + CurrentFrameOn][i]);
            }
        }
        if (++CurrentFrameOn < frames) {
            document.getElementById("Ready").disabled = true;
            setTimeout(function () { RenderFrames(FrameStart, frames); }, 50);
        }
        else {
            document.getElementById("Ready").disabled = false;
            CurrentFrameOn = 0;
        }
    };
    game.client.renderFrameSet = function RenderFrameSet(framestart,frames)
    {
        RenderFrames(framestart, frames);
    }
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