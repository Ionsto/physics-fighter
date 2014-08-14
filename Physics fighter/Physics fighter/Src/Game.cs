using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Physics_fighter.Src
{
    public class Game
    {
        public static Lobby Lobby = new Lobby();
        public static World World = new World(6,5);
    }
}