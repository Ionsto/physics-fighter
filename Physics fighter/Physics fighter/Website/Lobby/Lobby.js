$(document).ready(function () {
    var lobby = $.connection.lobbyHub;
    lobby.client.setPlayerList = function (names) {
        document.getElementById("PlayerNameDisplay").innerText = "";
        for (var i = 0; i < names.length; ++i) {
            document.getElementById("PlayerNameDisplay").innerText += names[i];
        }
    };
    var PlayerName = "MissingNo";
    document.getElementById("Ready").onclick = function () {
        alert("fsa");
        if (document.getElementById("Ready").value === "Ready")
        {
            PlayerName = document.getElementById("Name").value;
            lobby.server.addPlayer(PlayerName);
            document.getElementById("Ready").value = "Unready";
        }
        else
        {
            lobby.server.removePlayer(PlayerName);
            document.getElementById("Ready").value = "Ready";
        }
    };
});