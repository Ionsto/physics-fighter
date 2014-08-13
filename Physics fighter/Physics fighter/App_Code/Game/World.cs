using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Physics_fighter.App_Code.Game
{
    public class World
    {
        Entity[] EntityList;
        Connection[] ConnectionList;
        public World(int EntityCount,int ConnectionCount)
        {
            EntityList = new Entity[EntityCount];
            for (int i = 0; i < EntityList.Length; ++i)
            {
                EntityList[i] = null;
            }
            ConnectionList = new Connection[ConnectionCount];
            for (int i = 0; i < EntityList.Length; ++i)
            {
                ConnectionList[i] = null;
            }
        }
    }
}