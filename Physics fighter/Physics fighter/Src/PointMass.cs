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
        //3 = Contract 
        //5 = Relax grip
        //6 = Grab
        public int State = 0;
        public float ForceApplied = 1;
        public float JointLimit = 90;
        public bool Render = true;
        public List<int> Connected = new List<int>();//List of all the ids of connections
        public Vector_2d Pos = new Vector_2d();
        public Vector_2d OldPos = new Vector_2d();
        public Vector_2d Acceleration = new Vector_2d();
        public float Mass = 0;
        public float InverseMass = 0;
        public bool OnGround = false;
        public int Id = -1;
        public int Player = -1;//Neutral
        public bool Grabbed = false;
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
            Accerate(new Vector_2d(0,-0.2F),world);
        }
        public void Accerate(Vector_2d vec,World world)
        {
            Acceleration = Acceleration.Add(vec);
        }
        public void Intergrate(World world,float friction = 0)
        {
            friction /= InverseMass;
            Vector_2d newOld = Pos;
            Vector_2d Friction = new Vector_2d(2 - friction, 2 - friction);//Normal air friction
            Vector_2d FrictionOld = new Vector_2d(1 - friction, 1 - friction);//Normal air friction
            if (OnGround)
            {
                //Friction.X = 1 + friction;
                //FrictionOld.X = friction;
            } 
            Pos = Pos.Mult(Friction).Sub(OldPos.Mult(FrictionOld));
            Pos = Pos.Add(Acceleration.Mult(world.DeltaTime * world.DeltaTime));
            OldPos = newOld;
            Acceleration = new Vector_2d(0,0);
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
                Vector_2d newOld = Pos.Add(Displace.Mult(2F));
                Pos = OldPos;
                OldPos = newOld;
            }
        }
    }
}