using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Physics_fighter.Hubs
{
    public class LobbyHub : Hub
    {
        List<String> PlayerList = new List<String>();
        int ClientCount = 0;
        //this function is called by the clients
        public void ReadyState()
        {

            ++ClientCount;
        }
        public void AddPlayer(string Name)
        {
            PlayerList.Add(Name);
            Clients.All.setPlayerList(PlayerList);
        }
        public void RemovePlayer(string Name)
        {
            PlayerList.Remove(Name);
            Clients.All.setPlayerList(PlayerList);
        }
    }
}