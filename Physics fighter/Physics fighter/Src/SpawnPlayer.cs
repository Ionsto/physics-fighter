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

            int LowerLegACon = world.AddConnection(new ConnectionStaticDistance(world, LowerLegA, MidLegA));
            int LowerLegBCon = world.AddConnection(new ConnectionStaticDistance(world, LowerLegB, MidLegB));
            int HigherLegACon = world.AddConnection(new ConnectionStaticDistance(world, MidLegA, HigherLegA));
            int HigherLegBCon = world.AddConnection(new ConnectionStaticDistance(world, MidLegB, HigherLegB));
            world.AddConnection(new ConnectionStaticDistance(world, HigherLegA, HigherLegB));
            world.AddConnection(new ConnectionStaticDistance(world, HigherLegA, LowerAbs));
            world.AddConnection(new ConnectionStaticDistance(world, HigherLegB, LowerAbs));

            world.AddConnection(new ConnectionPushClose(world, LowerLegA, HigherLegA, 90, LowerLegACon, HigherLegACon));
            world.AddConnection(new ConnectionPushClose(world, LowerLegB, HigherLegB, 90, LowerLegBCon, HigherLegBCon));

        }
    }
}