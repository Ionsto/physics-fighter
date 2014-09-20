using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Physics_fighter.Src
{
    public class ConnectionRotateToDirection : ConnectionStaticDistance
    {
        int PointMid = 0;
        float LargestDistance = 0;
        float SmallestDistance = 0;
        float Stiffness = 0.5F;
        bool Direction = false;
        //precalculated
        public ConnectionRotateToDirection(World world, int a,int mid, int b,bool direction/* 0 = 90, 1 = 270*/)
            : base(world, a, b)
        {
            Direction = direction;
            PointMid = mid;
            Vector_2d Dist = world.PointMassList[PointA].Pos.Sub(world.PointMassList[PointB].Pos);
            UsedDistance = (float)Math.Sqrt((double)Dist.Dot(Dist));
            LargestDistance = 1;//To solve bugs
            foreach (int k in Game.World.PointMassList[PointMid].Connected)
            {
                if (k != -1)
                {
                    if (Game.World.ConnectionList[k].PointA == a || Game.World.ConnectionList[k].PointB == a)
                    {
                        LargestDistance += Game.World.ConnectionList[k].UsedDistance;
                    }
                    if (Game.World.ConnectionList[k].PointA == b || Game.World.ConnectionList[k].PointB == b)
                    {
                        LargestDistance += Game.World.ConnectionList[k].UsedDistance;
                    }
                }
            }
            float adis = 0;
            float bdis = 0;
            foreach (int k in Game.World.PointMassList[PointMid].Connected)
            {
                if (k != -1)
                {
                    if (Game.World.ConnectionList[k].PointA == a || Game.World.ConnectionList[k].PointB == a)
                    {
                        adis = Game.World.ConnectionList[k].UsedDistance;
                    }
                    if (Game.World.ConnectionList[k].PointA == b || Game.World.ConnectionList[k].PointB == b)
                    {
                        bdis = Game.World.ConnectionList[k].UsedDistance;
                    }
                }
            }
            SmallestDistance = (float)Math.Sqrt((float)((adis * adis) + (bdis * bdis)- (2 * adis * bdis * Math.Cos((float)Game.World.PointMassList[PointMid].JointLimit * (3.14 / 180)))));

        }
        public override void ResolveConstraint(World world)
        {
            
            Vector_2d Avec = world.PointMassList[PointMid].Pos.Sub(world.PointMassList[PointA].Pos).Normal().Perpendicular();
            //Whats it with?
            if(Direction){Avec.Invert();}
            Vector_2d Bvec = world.PointMassList[PointB].Pos.Sub(world.PointMassList[PointMid].Pos).Normal();
            float Dot = Avec.Dot(Bvec);
            Dot = (float)Math.Sqrt(Math.Abs(Dot)) * (Dot/Math.Abs(Dot));
            float Normal = (Dot / (float)(Math.Sqrt(Avec.Dot(Avec))));
            const float Give = 0;//1.0F/10.0F;
            if(Math.Abs(1.0F - Normal) > Give)
            {
                if(Normal < 0)
                {
                    //Extend
                    UsedDistance = LargestDistance;
                }
                if(Normal > 0)
                {
                    //Contract
                    UsedDistance = SmallestDistance;
                }
                if(Normal == 0)
                {
                    world.PointMassList[PointA].Pos.X += 1;
                    world.PointMassList[PointB].Pos.X -= 1;
                }
                base.ResolveConstraint(world);
            }
        }
    }
}