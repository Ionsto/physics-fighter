$(document).ready(function () {
    var lobby = $.connection.lobbyHub;
    lobby.client.startGame = function ()
    {
        window.location = window.location + "/../../Game/Game.html?name="+PlayerName;
    };
    lobby.client.setPlayerList = function (names, ready)
    {
        document.getElementById("PlayerNameDisplay").innerHTML = "";
        for (var i = 0; i < names.length; ++i)
        {
            if (ready.indexOf(names[i]) === -1)
            {
                document.getElementById("PlayerNameDisplay").innerHTML += names[i] + "<br/>";
            }
            else
            {
                document.getElementById("PlayerNameDisplay").innerHTML += names[i] + " - Ready<br/>";
            }
        }
    };
    var PlayerName = "Spec";
    document.getElementById("Join").onclick = function ()
    {
        if (document.getElementById("Join").value === "Join")
        {
            PlayerName = document.getElementById("Name").value;
            lobby.server.addPlayer(PlayerName);
            document.getElementById("Join").value = "Leave";
        }
        else
        {
            lobby.server.removePlayer(PlayerName);
            lobby.server.unreadyPlayer(PlayerName);
            document.getElementById("Ready").value = "Ready";
            document.getElementById("Join").value = "Join";
        }
    };
    document.getElementById("Ready").onclick = function ()
    {
        if (document.getElementById("Join").value === "Leave") {
            if (document.getElementById("Ready").value === "Ready")
            {
                PlayerName = document.getElementById("Name").value;
                lobby.server.readyPlayer(PlayerName);
                document.getElementById("Ready").value = "Unready";
            }
            else
            {
                lobby.server.unreadyPlayer(PlayerName);
                document.getElementById("Ready").value = "Ready";
            }
        }
    };
    $.connection.hub.start().done(function ()
    {
        lobby.server.refreshClientPlayerList();
    });
});