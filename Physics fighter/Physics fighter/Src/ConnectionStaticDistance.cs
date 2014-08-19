using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Physics_fighter.Src
{
    public class ConnectionStaticDistance : Connection
    {
        //precalculated
        public ConnectionStaticDistance(World world, int a, int b)
            : base(world, a, b)
        {
            Vector_2d Dist = world.PointMassList[PointA].Pos.Sub(world.PointMassList[PointB].Pos);
            //Resting distances
            UsedDistance = (float)Math.Sqrt((double)Dist.Dot(Dist));
        }
        public ConnectionStaticDistance(World world, int a, int b,float Distance)
            : base(world, a, b)
        {
            Vector_2d Dist = world.PointMassList[PointA].Pos.Sub(world.PointMassList[PointB].Pos);
            //Resting distances
            UsedDistance = Distance;
        }
        public override void ResolveConstraint(World world)
        {

            Vector_2d Dist = world.PointMassList[PointA].Pos.Sub(world.PointMassList[PointB].Pos);
            float Distance = (float)Math.Sqrt(Dist.Dot(Dist));
            float Difference = (UsedDistance - Distance) / Distance;
            if (Math.Abs(Distance - UsedDistance) > ForceHeld)//Give before taking damadge
            {
                Damadge -= 5 * Math.Abs(Distance - UsedDistance) / ForceHeld;
                Damadge = Math.Max(0, Damadge);//For colour
            }
            else
            {
                Damadge = Math.Min(Damadge + (1.0F * world.DeltaTime * world.DeltaConstraint),100);
            }
            float Give = 2;
            if (Math.Abs(Distance - UsedDistance) > Give)//Give before taking damadge
            {
                Vector_2d translate = new Vector_2d((float)(Dist.X * Difference), (float)(Dist.Y * Difference));
                translate.Mult(Force);
                translate.Mult(0.5F);//.Mult(world.DeltaTime).Mult(world.DeltaConstraint);
                world.PointMassList[PointA].Pos = world.PointMassList[PointA].Pos.Add(translate);
                world.PointMassList[PointB].Pos = world.PointMassList[PointB].Pos.Sub(translate);
            }
        }
    }
}