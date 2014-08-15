using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Physics_fighter.Src
{
    public class Lobby
    {
        public List<String> PlayerList = new List<String>();
        public List<String> ReadyList = new List<String>();

        public void AddPlayer(string Name)
        {
            PlayerList.Add(Name);
        }
        public void RemovePlayer(string Name)
        {
            PlayerList.Remove(Name);
        }
        public void ReadyPlayer(string Name)
        {
            ReadyList.Add(Name);
        }
        public void UnReadyPlayer(string Name)
        {
            ReadyList.Remove(Name);
        }
    }
}