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
            //Stiffness = 1;
        }
        public ConnectionStaticDistance(World world, int a, int b,float Distance)
            : base(world, a, b)
        {
            //Resting distances
            UsedDistance = Distance;
           //Stiffness = 1;
        }
        public override void ResolveConstraint(World world)
        {
            Vector_2d Dist = world.PointMassList[PointA].Pos.Sub(world.PointMassList[PointB].Pos);
            float Distance = (float)Math.Sqrt(Dist.Dot(Dist));
            float Difference = (UsedDistance - Distance) / Distance;
            if (Math.Abs(Difference) > DistanceHeld)//Give before taking damadge
            {
                Damadge -= 5.0F * Math.Abs(Difference) * world.DeltaTime * world.DeltaConstraint;
                Damadge = Math.Max(0, Damadge);//For colour
            }
            else
            {
                Damadge = Math.Min(Damadge + (1.0F * world.DeltaTime * world.DeltaConstraint),100);
            }
            const float Give = 0;
            if (Math.Abs(Distance - UsedDistance) > Give)//Give before moving
            {
                Vector_2d translate = new Vector_2d((float)(Dist.X * Difference), (float)(Dist.Y * Difference));
                //translate = translate.Mult(world.DeltaConstraint);
                translate = translate.Mult(0.5F);
                float scalarP1 = (world.PointMassList[PointA].InverseMass / (world.PointMassList[PointA].InverseMass + world.PointMassList[PointB].InverseMass)) * Stiffness;
                float scalarP2 = Stiffness - scalarP1;
                //world.PointMassList[PointA].Accerate(translate.Mult(scalarP1),world);
                //world.PointMassList[PointB].Accerate(translate.Mult(scalarP2).Inverted(), world);
                world.PointMassList[PointA].Pos = world.PointMassList[PointA].Pos.Add(translate.Mult(scalarP1));
                world.PointMassList[PointB].Pos = world.PointMassList[PointB].Pos.Sub(translate.Mult(scalarP2));
            }
        }
    }
}