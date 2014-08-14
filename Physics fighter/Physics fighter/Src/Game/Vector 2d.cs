using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Physics_fighter.Src.Game
{
    public class Vector_2d
    {
        public float X, Y;
        public Vector_2d(float x = 0,float y = 0)
        {
            X = x;
            Y = y;
        }
        public Vector_2d Add(Vector_2d v)
        {
            return new Vector_2d(X + v.X, Y + v.Y);
        }
        public Vector_2d Sub(Vector_2d v)
        {
            return new Vector_2d(X - v.X, Y - v.Y);
        }
        public Vector_2d Mult(float v)
        {
            return new Vector_2d(X * v, Y * v);
        }
        public float Dot(Vector_2d v)
        {
            return (X * v.X) + (Y * v.Y);
        }
    }
}