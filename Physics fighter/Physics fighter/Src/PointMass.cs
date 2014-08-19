using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Physics_fighter.Src
{
    public class PointMass
    {
        public int Type = 0;//-1 = null 0 = normal, 1 = hand
        //0 = Relax
        //1 = Hold
        //2 = Extend
        //2 = Contract 
        public int State = 0;
        public float JointLimit = 90;
        public bool Render = true;
        public List<int> Connected = new List<int>();//List of all the ids of connections
        public Vector_2d Pos = new Vector_2d();
        public Vector_2d OldPos = new Vector_2d();
        public float Mass = 0;
        public float InverseMass = 0;
        public int Id = -1;
        public int Player = -1;//Neutral
        public PointMass(Vector_2d loc)
        {
            Pos = loc;
            OldPos = loc;
            SetMass(10);
        }
        public void SetMass(float newmass)
        {
            Mass = newmass;
            InverseMass = 1 / Mass;
        }
        public void Update(World world)
        {
            Intergrate(world);
            CheckBounds(world);
            Pos.Y -= 1 * world.DeltaTime;
        }
        public void Intergrate(World world,float friction = 0.5F)
        {
            Vector_2d newOld = Pos;
            Pos = Pos.Add(Pos.Sub(OldPos).Mult(friction).Mult(world.DeltaTime));
            OldPos = newOld;
        }
        public void CheckBounds(World world)
        {
            if (Pos.Y < world.BufferSize)
            {
                Pos.Y += (world.BufferSize - Pos.Y) * world.DeltaTime;
            }
            if (Pos.Y > world.Size.Y - world.BufferSize)
            {
                Pos.Y += ((world.Size.Y - world.BufferSize) - Pos.Y) * world.DeltaTime;
            }
            if (Pos.X < world.BufferSize)
            {
                Pos.X += (world.BufferSize - Pos.X) * world.DeltaTime;
            }
            if (Pos.X > world.Size.X - world.BufferSize)
            {
                Pos.X += ((world.Size.X - world.BufferSize) - Pos.X) * world.DeltaTime;
            }
        }
    }
}