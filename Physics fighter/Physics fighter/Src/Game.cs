using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Physics_fighter.Src
{
    public class Game
    {
        public static bool InGame = false;
        public static Lobby Lobby = null;
        public static int PlayerCount = 0;
        public static World World = null;
        public static void MakeSafe()
        {
            if (Lobby == null)
            {
                Lobby = new Lobby();
            }
        }
        public static void MakeWorldSafe()
        {
            if (World == null)
            {
                /*
                     _
                    \ / 
                   \_|_/
                     |
                    /_\
                   |   |
                   |   |    
                */
                //12 is the minamom for body + 8 is max for wepons
                //14 for body connectisions 10 for wepon connections
                World = new World((17+8) * PlayerCount,(15 + 10) * PlayerCount);
            }
        }
    }
}