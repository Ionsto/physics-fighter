using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Physics_fighter.Src
{
    public class ConnectionPushClose : Connection
    {
        public ConnectionPushClose(World world, int a, int b, float MinDistanceSet = -1)
            : base(world, a, b)
        {
            Vector_2d Dist = world.PointMassList[PointA].Pos.Sub(world.PointMassList[PointB].Pos);
            //Minamum distance to apply
            if (MinDistanceSet == -1)
            {
                UsedDistance = (float)Math.Sqrt((double)Dist.Dot(Dist));
            }
            else
            {
                UsedDistance = MinDistanceSet;
            }
        }
        public ConnectionPushClose(World world, int a, int b, float MinAngle,int cona,int conb)
            : base(world, a, b)
        {
            Vector_2d Dist = world.PointMassList[PointA].Pos.Sub(world.PointMassList[PointB].Pos);
            float asqd = world.ConnectionList[cona].UsedDistance * world.ConnectionList[cona].UsedDistance;
            float bsqd = world.ConnectionList[conb].UsedDistance * world.ConnectionList[conb].UsedDistance;
            float distance = (float)Math.Sqrt((float)(asqd + bsqd - (2 * world.ConnectionList[cona].UsedDistance * world.ConnectionList[conb].UsedDistance * Math.Cos((float)MinAngle * (3.14/180)))));
            //Cosine rule
            UsedDistance = distance;
        }
        public override void ResolveConstraint(World world)
        {
            Vector_2d Dist = world.PointMassList[PointA].Pos.Sub(world.PointMassList[PointB].Pos);
            float DistDot = Dist.Dot(Dist);
            float Distance = (float)Math.Sqrt(DistDot);
            float Difference = (UsedDistance - Distance) / Distance;
            if (Math.Abs(Distance - UsedDistance) > ForceHeld)//Give before taking damadge
            {
                Damadge -= 5 * Math.Abs(Distance - UsedDistance) / ForceHeld;
                Damadge = Math.Max(0, Damadge);//For colour
            }
            else
            {
                Damadge = Math.Min(Damadge + 1,100);
            }
            if (Difference > 0)
            {
                Vector_2d translate = new Vector_2d((float)(Dist.X * Difference), (float)(Dist.Y * Difference));
               //translate.Mult(Force);
                translate.Mult(0.5F);
                world.PointMassList[PointA].Pos = world.PointMassList[PointA].Pos.Add(translate);
                world.PointMassList[PointB].Pos = world.PointMassList[PointB].Pos.Sub(translate);
            }
        }
    }
}