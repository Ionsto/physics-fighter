using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Physics_fighter.App_Code.Game
{
    public class Entity
    {
        Vector_2d PosA = new Vector_2d();
        Vector_2d OldPosA = new Vector_2d();
        Vector_2d PosB = new Vector_2d();
        Vector_2d OldPosB = new Vector_2d();
        int Id = -1;
        float RestingDiffrenceSqd;
        float RestingDiffrence;
        float ForceHeld = 10;
        float Damadge = 100;
        public Entity()
        {
            Vector_2d Dist = PosA.Sub(PosB);
            RestingDiffrence =(float) Math.Sqrt((double)Dist.Dot(Dist));
            RestingDiffrenceSqd = RestingDiffrence * RestingDiffrence;

        }
        public void Update(World world)
        {
            Intergrate();
            ResolveConstraint();
            if(Damadge <= 0)
            {
                world.EntityList[Id] = null;

            }
        }
        public void Intergrate()
        {
            Vector_2d newOld = PosA;
            PosA = PosA.Add(PosA.Sub(OldPosA).Mult(0.999F));
            OldPosA = newOld;
            newOld = PosB;
            PosB = PosB.Add(PosB.Sub(OldPosB).Mult(0.999F));
            OldPosA = newOld;
        }
        public void ResolveConstraint()
        {
            Vector_2d Dist = PosA.Sub(PosB);
            float DistDot = Dist.Dot(Dist);
            float Distance = (float)Math.Sqrt(DistDot);
            float Difference = (RestingDiffrence - Distance) /  Distance;
            if (Math.Abs(Distance - RestingDiffrence) > ForceHeld)//Give before taking damadge
            {
                Damadge -= 5 * Math.Abs(DistDot - RestingDiffrenceSqd) / ForceHeld;
            }
            Vector_2d translate = new Vector_2d((float)(Dist.X * 0.5 * Difference), (float)(Dist.Y * 0.5 * Difference));
            PosA = PosA.Add(translate);
            PosB = PosB.Sub(translate);
        }
    }
}