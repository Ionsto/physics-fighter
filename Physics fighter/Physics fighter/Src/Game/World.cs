using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Physics_fighter.Src.Game
{
    public class World
    {
        public Entity[] EntityList;
        public Connection[] ConnectionList;
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
        public void Update()
        {
            for (int i = 0; i < EntityList.Length;++i)
            {
                if(EntityList[i] != null)
                {
                    EntityList[i].Update(this);
                }
            }
        }
    }
}