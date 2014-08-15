using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Physics_fighter.Src
{
    public class PointMass
    {
        List<int> Connected = new List<int>();//List of all the ids of connections
        public Vector_2d Pos = new Vector_2d();
        public Vector_2d OldPos = new Vector_2d();
        public int Player = -1;
        public void Update(World world)
        {
            Intergrate();
        }
        public void Intergrate()
        {
            Vector_2d newOld = Pos;
            Pos = Pos.Add(Pos.Sub(OldPos).Mult(0.999F));
            OldPos = newOld;
        }
    }
}