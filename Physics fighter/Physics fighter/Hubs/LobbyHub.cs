﻿using System;
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
        public void AddPlayer(string Name)
        {
            Game.MakeSafe();
            Game.Lobby.AddPlayer(Name);
            Clients.All.setPlayerList(Game.Lobby.PlayerList);
        }
        public void RemovePlayer(string Name)
        {
            Game.MakeSafe();
            Game.Lobby.RemovePlayer(Name);
            Clients.All.setPlayerList(Game.Lobby.PlayerList,Game.Lobby.PlayerReady);
        }
        public void ReadyPlayer(string Name)
        {
            Game.MakeSafe();
            Game.Lobby.AddPlayer(Name);
            Clients.All.setPlayerList(Game.Lobby.PlayerList);
        }
        public void UnreadyPlayer(string Name)
        {
            Game.MakeSafe();
            Game.Lobby.UnReadyPlayer(Name);
            Clients.All.setPlayerList(Game.Lobby.PlayerList);
        }
    }
}