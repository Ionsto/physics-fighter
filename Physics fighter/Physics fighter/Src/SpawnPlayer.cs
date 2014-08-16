using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Physics_fighter.Src
{
    public class SpawnPlayer
    {
        public SpawnPlayer(World world, Vector_2d loc_floor)
        {
            float LegWidth = 10;
            int LowerLegA = world.AddPointMass(new PointMass(new Vector_2d(loc_floor.X + LegWidth, loc_floor.Y)));
            int LowerLegB = world.AddPointMass(new PointMass(new Vector_2d(loc_floor.X - LegWidth, loc_floor.Y)));
            float LegTaper = 4;
            float LegHeight = 40;
            int MidLegA = world.AddPointMass(new PointMass(new Vector_2d(loc_floor.X + LegWidth - LegTaper, loc_floor.Y + (LegHeight / 2))));
            int MidLegB = world.AddPointMass(new PointMass(new Vector_2d(loc_floor.X - LegWidth + LegTaper, loc_floor.Y + (LegHeight / 2))));

            int HigherLeg = world.AddPointMass(new PointMass(new Vector_2d(loc_floor.X, loc_floor.Y + LegHeight)));

            world.AddConnection(new Connection(world, LowerLegA, MidLegA));
            world.AddConnection(new Connection(world, LowerLegB, MidLegB));
            world.AddConnection(new Connection(world, MidLegA, HigherLeg));
            world.AddConnection(new Connection(world, MidLegB, HigherLeg));

        }
    }
}