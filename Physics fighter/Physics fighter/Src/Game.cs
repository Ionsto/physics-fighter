using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Physics_fighter.Src
{
    public class Game
    {
        public static Lobby Lobby = null;
        public static World World = null;
        public static void MakeSafe()
        {
            if (Lobby == null)
            {
                Lobby = new Lobby();
            }
        }
    }
}