using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Physics_fighter.Src;
using Microsoft.AspNet.SignalR;
namespace Physics_fighter
{
    public class LobbyHub : Hub
    {
        List<String> PlayerList = new List<String>();
        public void AddPlayer(string Name)
        {
            Physics_fighter.Src.Lobby.Lobby;
            PlayerList.Add(Name);
            Clients.All.setPlayerList(PlayerList);
        }
        public void RemovePlayer(string Name)
        {
            //PlayerList.Remove(Name);
            //Clients.All.setPlayerList(PlayerList);
        }
    }
}