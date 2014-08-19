using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Physics_fighter.Src
{
    public class SpawnPlayer
    {
        public SpawnPlayer(World world, Vector_2d loc_floor,Player player = null)
        {
            //Lergs
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
            int LowerBody = world.AddPointMass(new PointMass(new Vector_2d(loc_floor.X, loc_floor.Y + LegHeight + HipSize)));

            int LowerLegACon = world.AddConnection(new ConnectionStaticDistance(world, LowerLegA, MidLegA));
            int LowerLegBCon = world.AddConnection(new ConnectionStaticDistance(world, LowerLegB, MidLegB));
            int HigherLegACon = world.AddConnection(new ConnectionStaticDistance(world, MidLegA, HigherLegA));
            int HigherLegBCon = world.AddConnection(new ConnectionStaticDistance(world, MidLegB, HigherLegB));
            int HipConMid = world.AddConnection(new ConnectionStaticDistance(world, HigherLegA, HigherLegB));
            int HipConA = world.AddConnection(new ConnectionStaticDistance(world, HigherLegA, LowerBody));
            int HipConB = world.AddConnection(new ConnectionStaticDistance(world, HigherLegB, LowerBody));
            world.AddConnection(new ConnectionPushClose(world, LowerLegA, HigherLegA, 90, LowerLegACon, HigherLegACon));
            world.AddConnection(new ConnectionPushClose(world, LowerLegB, HigherLegB, 90, LowerLegBCon, HigherLegBCon));
            world.AddConnection(new ConnectionPushClose(world, LowerBody, MidLegA, 90, HipConA, HigherLegACon));
            world.AddConnection(new ConnectionPushClose(world, LowerBody, MidLegB, 90, HipConB, HigherLegBCon));
            //Body
            
            float BodyHeight = 50;
            int MidBody = world.AddPointMass(new PointMass(new Vector_2d(loc_floor.X, loc_floor.Y + LegHeight + (BodyHeight / 2))));
            int HigherBody = world.AddPointMass(new PointMass(new Vector_2d(loc_floor.X, loc_floor.Y + LegHeight + BodyHeight)));
            int ShoulderWidth = 10;
            int ShoulderHeight = 10;
            int ShoulderA = world.AddPointMass(new PointMass(new Vector_2d(loc_floor.X + ShoulderWidth, loc_floor.Y + LegHeight + BodyHeight + ShoulderHeight)));
            int ShoulderB = world.AddPointMass(new PointMass(new Vector_2d(loc_floor.X - ShoulderWidth, loc_floor.Y + LegHeight + BodyHeight + ShoulderHeight)));
            int LowerBodyCon = world.AddConnection(new ConnectionStaticDistance(world, LowerBody, MidBody));
            int HigherBodyCon = world.AddConnection(new ConnectionStaticDistance(world, MidBody, HigherBody));
            int ShoulderConA = world.AddConnection(new ConnectionStaticDistance(world, HigherBody, ShoulderA));
            int ShoulderConB = world.AddConnection(new ConnectionStaticDistance(world, HigherBody, ShoulderB));
            int ShoulderCon = world.AddConnection(new ConnectionStaticDistance(world, ShoulderA, ShoulderB));

            world.AddConnection(new ConnectionPushClose(world, HigherLegA, MidBody, 90, HipConA, LowerBodyCon));
            world.AddConnection(new ConnectionPushClose(world, HigherLegB, MidBody, 90, HipConB, LowerBodyCon));
            if (player != null)
            {
                player.JointsId.Add(new int[] { LowerLegA, MidLegA, HigherLegA });
                player.JointsId.Add(new int[] { LowerLegB, MidLegB, HigherLegB });
                player.JointsId.Add(new int[] { MidLegA, HigherLegA, LowerBody });
                player.JointsId.Add(new int[] { MidLegB, HigherLegB, LowerBody });
                player.JointsId.Add(new int[] { LowerBody, MidBody, HigherBody });
            }
        }
    }
}