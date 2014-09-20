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
        public float DistanceHeld = 10;
        public float Force = 1F;
        public float ForceStep = 0F;
        public float MaxForce = 1;
        public float Damadge = 100;
        public float UsedDistance = 0;//The lumped distance variable
        public float Stiffness = 1;
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
                Destroy(world);
                BreakTies(PointA, world);
                BreakTies(PointB, world);
            }
        }
        public void BreakTies(int point,World world)
        {
            if (world.PointMassList[point].Player != -1)
            {
                Player player = Game.World.PlayerList[world.PointMassList[point].Player];
                for (int j = 0; j < player.JointActuators.Count; ++j)
                {
                    if (player.JointActuators[j][1] == point)
                    {
                        if (player.JointActuators[j][0] != -1)
                        {
                            if (Game.World.ConnectionList[player.JointActuators[j][0]] != null)
                            {
                                Game.World.ConnectionList[player.JointActuators[j][0]].Destroy(Game.World);
                            }
                        }
                    }
                }
            }
        }
        public void Destroy(World world)
        {
            world.ConnectionList[Id] = null;
            world.PointMassList[PointA].Connected.Remove(Id);
            world.PointMassList[PointB].Connected.Remove(Id);
        }
        public virtual void ResolveConstraint(World world)
        {
        }
    }
}