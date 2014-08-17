using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Physics_fighter.Src
{
    public class Connection
    {
        public bool Render = true;
        public int PointA;
        public int PointB;
        public int Id = -1;
        //precalculated
        public float ForceHeld = 100;
        float Force = 0.1F;
        public float Damadge = 100;
        public float UsedDistance = 0;//The lumped distance variable
        public Connection(World world,int a,int b)
        {
            PointA = a;
            PointB = b;

        }
        public void Update(World world)
        {
            ResolveConstraint(world);
            if(Damadge <= 0)
            {
                world.ConnectionList[Id] = null;
            }
        }
        public virtual void ResolveConstraint(World world)
        {
        }
    }
}