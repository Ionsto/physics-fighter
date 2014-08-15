using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Physics_fighter.Src
{
    public class World
    {
        public List<String> PlayerList = new List<String>();
        public List<String> ReadyList = new List<String>();
        public Connection[] EntityList;
        public Connection[] ConnectionList;
        public World(int EntityCount,int ConnectionCount)
        {
            EntityList = new Connection[EntityCount];
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