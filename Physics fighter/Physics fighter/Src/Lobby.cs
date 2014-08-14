using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Physics_fighter.Src
{
    public class Lobby
    {
        public List<String> PlayerList = new List<String>();
        public int PlayerReady = 0;

        public void AddPlayer(string Name)
        {
            Game.Lobby.PlayerList.Add(Name);
        }
        public void RemovePlayer(string Name)
        {
            Game.Lobby.PlayerList.Remove(Name);
        }
    }
}