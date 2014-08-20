using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Physics_fighter.Src
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
        public Vector_2d Mult(Vector_2d v)
        {
            return new Vector_2d(X * v.X, Y * v.Y);
        }
        public Vector_2d Div(Vector_2d v)
        {
            return new Vector_2d(X / v.X, Y / v.Y);
        }
        public Vector_2d Div(float v)
        {
            return new Vector_2d(X / v, Y / v);
        }
        public Vector_2d Mult(float v)
        {
            return new Vector_2d(X * v, Y * v);
        }
        public float Dot(Vector_2d v)
        {
            return (X * v.X) + (Y * v.Y);
        }
        public Vector_2d Normal()
        {
            return this.Div((float)Math.Sqrt(this.Dot(this)));
        }
        public void Invert()
        {
            X = -X;
            Y = -Y;
        }
        public Vector_2d Inverted()
        {
            return new Vector_2d(-X, -Y);
        }
        public Vector_2d Perpendicular()
        {
            //to 90
            return new Vector_2d(-Y, X);
        }
    }
}