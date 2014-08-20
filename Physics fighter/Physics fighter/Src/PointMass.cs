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
        public float ForceApplied = 0;
        public float JointLimit = 90;
        public bool Render = true;
        public List<int> Connected = new List<int>();//List of all the ids of connections
        public Vector_2d Pos = new Vector_2d();
        public Vector_2d OldPos = new Vector_2d();
        public float Mass = 0;
        public float InverseMass = 0;
        public bool OnGround = false;
        public int Id = -1;
        public int Player = -1;//Neutral
        public PointMass(Vector_2d loc,float mass = 10,float force = 10)
        {
            Pos = loc;
            OldPos = loc;
            SetMass(mass);
            ForceApplied = force;
        }
        public void SetMass(float newmass)
        {
            Mass = newmass;
            InverseMass = 1 / Mass;
        }
        public void Update(World world)
        {
            CheckBounds(world);
            Intergrate(world);
            Pos.Y -= 2 * world.DeltaTime;
        }
        public void Intergrate(World world,float friction = 0.9F)
        {
            Vector_2d newOld = Pos;
            Vector_2d Friction = new Vector_2d(friction,friction);//Normal air friction
            if(OnGround)
            {
                Friction.X /= 1000.0F;
            }
            Pos = Pos.Add(Pos.Sub(OldPos).Mult(Friction).Mult(world.DeltaTime));
            OldPos = newOld;
        }
        public void CheckBounds(World world)
        {
            bool Affect = false;
            Vector_2d Displace = new Vector_2d();
            if (Pos.Y < world.BufferSize)
            {
                Affect = true;
                Displace.Y = (world.BufferSize - Pos.Y);
            }
            if (Math.Abs(Pos.Y - world.BufferSize) < 5 || Pos.Y < world.BufferSize)
            {
                OnGround = true;
            }
            else
            {
                OnGround = false;
            }
            if (Pos.Y > world.Size.Y - world.BufferSize)
            {
                Affect = true;
                Displace.Y = ((world.Size.Y - world.BufferSize) - Pos.Y);
            }
            if (Pos.X < world.BufferSize)
            {
                Affect = true;
                Displace.X = (world.BufferSize - Pos.X);
            }
            if (Pos.X > world.Size.X - world.BufferSize)
            {
                Affect = true;
                Displace.X = ((world.Size.X - world.BufferSize) - Pos.X);
            }
            if (Affect)
            {
                Vector_2d newOld = Pos;
                OldPos = newOld.Sub(Displace.Mult(5));
            }
        }
    }
}