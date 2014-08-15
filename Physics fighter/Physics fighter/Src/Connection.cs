using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Physics_fighter.Src
{
    public class Connection
    {
        public PointMass PointA;
        public PointMass PointB;
        int Id = -1;
        //precalculated
        float RestingDiffrenceSqd;
        float RestingDiffrence;
        float ForceHeld = 10;
        float Force = 0.1F;
        float Damadge = 100;
        public Connection()
        {
            Vector_2d Dist = PointA.Pos.Sub(PointB.Pos);
            RestingDiffrence = (float) Math.Sqrt((double)Dist.Dot(Dist));
            RestingDiffrenceSqd = RestingDiffrence * RestingDiffrence;
        }
        public void Update(World world)
        {
            ResolveConstraint();
            if(Damadge <= 0)
            {
                world.EntityList[Id] = null;
            }
        }
        public void ResolveConstraint()
        {
            Vector_2d Dist = PointA.Pos.Sub(PointB.Pos);
            float DistDot = Dist.Dot(Dist);
            float Distance = (float)Math.Sqrt(DistDot);
            float Difference = (RestingDiffrence - Distance) /  Distance;
            if (Math.Abs(Distance - RestingDiffrence) > ForceHeld)//Give before taking damadge
            {
                Damadge -= 5 * Math.Abs(DistDot - RestingDiffrenceSqd) / ForceHeld;
            }
            Vector_2d translate = new Vector_2d((float)(Dist.X * Difference), (float)(Dist.Y * Difference));
            translate.Mult(Force);
            translate.Mult(0.5F);
            PointA.Pos = PointA.OldPos.Sub(translate);
            PointB.Pos = PointB.OldPos.Add(translate);
        }
    }
}