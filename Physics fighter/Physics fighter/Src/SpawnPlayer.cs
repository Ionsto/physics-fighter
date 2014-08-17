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
            float LegWidth = 20;
            int LowerLegA = world.AddPointMass(new PointMass(new Vector_2d(loc_floor.X + LegWidth, loc_floor.Y)));
            int LowerLegB = world.AddPointMass(new PointMass(new Vector_2d(loc_floor.X - LegWidth, loc_floor.Y)));
            float LegTaper = 4;
            float LegHeight = 40;
            int MidLegA = world.AddPointMass(new PointMass(new Vector_2d(loc_floor.X + LegWidth - LegTaper, loc_floor.Y + (LegHeight / 2))));
            int MidLegB = world.AddPointMass(new PointMass(new Vector_2d(loc_floor.X - LegWidth + LegTaper, loc_floor.Y + (LegHeight / 2))));
            float HipSize = 10;
            int HigherLegA = world.AddPointMass(new PointMass(new Vector_2d(loc_floor.X + HipSize, loc_floor.Y + LegHeight)));
            int HigherLegB = world.AddPointMass(new PointMass(new Vector_2d(loc_floor.X - HipSize, loc_floor.Y + LegHeight)));
            int LowerAbs = world.AddPointMass(new PointMass(new Vector_2d(loc_floor.X, loc_floor.Y + LegHeight + HipSize)));

            world.AddConnection(new ConnectionStaticDistance(world, LowerLegA, MidLegA));
            world.AddConnection(new ConnectionStaticDistance(world, LowerLegB, MidLegB));
            world.AddConnection(new ConnectionStaticDistance(world, MidLegA, HigherLegA));
            world.AddConnection(new ConnectionStaticDistance(world, MidLegB, HigherLegB));
            world.AddConnection(new ConnectionStaticDistance(world, HigherLegA, HigherLegB));
            world.AddConnection(new ConnectionStaticDistance(world, HigherLegA, LowerAbs));
            world.AddConnection(new ConnectionStaticDistance(world, HigherLegB, LowerAbs));



        }
    }
}