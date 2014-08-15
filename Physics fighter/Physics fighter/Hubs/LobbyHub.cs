using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Physics_fighter;
using Microsoft.AspNet.SignalR;
using Physics_fighter.Src;
namespace Physics_fighter.Hubs
{
    public class LobbyHub : Hub
    {
        public void RefreshClientPlayerList()
        {
            Game.MakeSafe();
            Clients.Caller.setPlayerList(Game.Lobby.PlayerList, Game.Lobby.ReadyList);
        }
        public void AddPlayer(string Name)
        {
            Game.MakeSafe();
            Game.Lobby.AddPlayer(Name);
            Clients.All.setPlayerList(Game.Lobby.PlayerList,Game.Lobby.ReadyList);
        }
        public void RemovePlayer(string Name)
        {
            Game.MakeSafe();
            Game.Lobby.RemovePlayer(Name);
            Clients.All.setPlayerList(Game.Lobby.PlayerList,Game.Lobby.ReadyList);
        }
        public void ReadyPlayer(string Name)
        {
            Game.MakeSafe();
            Game.Lobby.ReadyPlayer(Name);
            Clients.All.setPlayerList(Game.Lobby.PlayerList, Game.Lobby.ReadyList);
            //2 or more players that have all readyed
            if (Game.Lobby.PlayerList.Count >= 2 && Game.Lobby.PlayerList.Count == Game.Lobby.ReadyList.Count)
            {
                //The game must start
                Clients.All.startGame();
                Game.PlayerCount = Game.Lobby.PlayerList.Count;
                Game.MakeWorldSafe();
            }
        }
        public void UnreadyPlayer(string Name)
        {
            Game.MakeSafe();
            Game.Lobby.UnReadyPlayer(Name);
            Clients.All.setPlayerList(Game.Lobby.PlayerList, Game.Lobby.ReadyList);
        }
    }
}