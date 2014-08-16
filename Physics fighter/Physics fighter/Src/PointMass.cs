using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Physics_fighter.Src
{
    public class PointMass
    {
        public bool Render = true;
        List<int> Connected = new List<int>();//List of all the ids of connections
        public Vector_2d Pos = new Vector_2d();
        public Vector_2d OldPos = new Vector_2d();
        public int Id = -1;
        public int Player = -1;
        public PointMass(Vector_2d loc)
        {
            Pos = loc;
            OldPos = loc;
        }
        public void Update(World world)
        {
            Intergrate();
            CheckBounds();
            Pos.Y -= 1;
        }
        public void Intergrate(float friction = 0.5F)
        {
            Vector_2d newOld = Pos;
            Pos = Pos.Add(Pos.Sub(OldPos).Mult(friction));
            OldPos = newOld;
        }
        public void CheckBounds()
        {

            if (Pos.Y < 0 || Pos.Y > 500 || Pos.X < 0 || Pos.X > 500)
            {
                Vector_2d newOld = Pos;
                Pos = OldPos;
                OldPos = newOld;
            }
            if (Pos.Y < 0)
            {
                Pos.Y = 0;
            }
            if (Pos.Y > 500 )
            {
                Pos.Y = 500;
            }
            if (Pos.X < 0)
            {
                Pos.X = 0;
            }
            if (Pos.X > 500)
            {
                Pos.X = 500;
            }
        }
    }
}