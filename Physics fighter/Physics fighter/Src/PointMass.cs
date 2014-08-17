using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Physics_fighter.Src
{
    public class PointMass
    {
        float JointLimit = 90;
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
            CheckBounds(world);
            Pos.Y -= 1;
        }
        public void Intergrate(float friction = 0.5F)
        {
            Vector_2d newOld = Pos;
            Pos = Pos.Add(Pos.Sub(OldPos).Mult(friction));
            OldPos = newOld;
        }
        public void CheckBounds(World world)
        {

            if (Pos.Y < world.BufferSize)
            {
                Pos.Y = world.BufferSize;
            }
            if (Pos.Y > world.Size.Y - world.BufferSize)
            {
                Pos.Y = world.Size.Y - world.BufferSize;
            }
            if (Pos.X < world.BufferSize)
            {
                Pos.X = world.BufferSize;
            }
            if (Pos.X > world.Size.X - world.BufferSize)
            {
                Pos.X = world.Size.X - world.BufferSize;
            }
            if (Pos.Y < world.BufferSize || Pos.Y > world.Size.Y - world.BufferSize || Pos.X < world.BufferSize || Pos.X > world.Size.X - world.BufferSize)
            {
                //Vector_2d newOld = Pos;
                //Pos = OldPos;
                //OldPos = newOld;
            }
        }
    }
}