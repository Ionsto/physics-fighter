using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Physics_fighter.App_Code.Game
{
    public class Entity
    {
        Vector_2d Pos = new Vector_2d();
        Vector_2d OldPos = new Vector_2d();
        float Rotation = 0;
        float Size = 0;// half the full lenght ( the length between the midle and eather side) 
        float Damadge = 100;
        public void Update()
        {
        }
    }
}