$(document).ready(function () {
    var lobby = $.connection.lobbyHub;
    lobby.client.setPlayerList = function (names,ready) {
        document.getElementById("PlayerNameDisplay").innerHTML = "";
        for (var i = 0; i < names.length; ++i) {
            if (ready.indexof(names[i]) === -1) {
                document.getElementById("PlayerNameDisplay").innerHTML += "<p >" + names[i] + "<\p><br/>";
            }
        }
    };
    var PlayerName = "MissingNo";
    document.getElementById("Join").onclick = function () {
        if (document.getElementById("Join").value === "Join") {
            PlayerName = document.getElementById("Name").value;
            lobby.server.addPlayer(PlayerName);
            document.getElementById("Join").value = "Leave";
        }
        else {
            lobby.server.removePlayer(PlayerName);
            document.getElementById("Join").value = "Join";
        }
    };
    document.getElementById("Ready").onclick = function () {
        if (document.getElementById("Join").value === "Leave") {
            if (document.getElementById("Ready").value === "Ready") {
                PlayerName = document.getElementById("Name").value;
                lobby.server.readyPlayer(PlayerName);
                document.getElementById("Ready").value = "Unready";
            }
            else {
                lobby.server.unreadyPlayer(PlayerName);
                document.getElementById("Ready").value = "Ready";
            }
        }
    };
    $.connection.hub.start();
});