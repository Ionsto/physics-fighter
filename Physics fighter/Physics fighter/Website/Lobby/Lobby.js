$(document).ready(function () {
    var Lobby = $.connection.lobbyHub;
    lobby.client.setPlayerList = function (names) {
        document.getElementById("PlayerNameDisplay").innerText = "";
        for (var i = 0; i < names.length; ++i)
        {
            document.getElementById("PlayerNameDisplay").innerText += names[i];
        }
    }
    var PlayerName = "MissingNo";
    function Ready(ready) {
        if(ready)
        {
            PlayerName = document.getElementById("Name");
            lobby.server.addPlayer(PlayerName);
        }
        if(!ready)
        {
            lobby.server.removePlayer(PlayerName);
        }
    }
});