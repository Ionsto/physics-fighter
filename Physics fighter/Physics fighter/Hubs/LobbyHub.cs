using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Physics_fighter;
using Microsoft.AspNet.SignalR;
namespace Physics_fighter.Hubs
{
    public class LobbyHub : Hub
    {
        List<String> PlayerList = new List<String>();
        public void AddPlayer(string Name)
        {
            Entity c;
            Lobby.MainLobby.PlayerList.Add(Name);
            Clients.All.setPlayerList(PlayerList);
        }
        public void RemovePlayer(string Name)
        {
            //PlayerList.Remove(Name);
            //Clients.All.setPlayerList(PlayerList);
        }
    }
}