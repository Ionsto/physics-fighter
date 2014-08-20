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
            int LowerLegA = world.AddPointMass(new PointMass(new Vector_2d(loc_floor.X + LegWidth, loc_floor.Y),40));
            int LowerLegB = world.AddPointMass(new PointMass(new Vector_2d(loc_floor.X - LegWidth, loc_floor.Y),40));
            float LegTaper = 4;
            float LegHeight = 40;
            int MidLegA = world.AddPointMass(new PointMass(new Vector_2d(loc_floor.X + LegWidth - LegTaper, loc_floor.Y + (LegHeight / 2)), 10, 50));
            int MidLegB = world.AddPointMass(new PointMass(new Vector_2d(loc_floor.X - LegWidth + LegTaper, loc_floor.Y + (LegHeight / 2)), 10, 50));
            float HipSize = 10;
            int HigherLegA = world.AddPointMass(new PointMass(new Vector_2d(loc_floor.X + HipSize, loc_floor.Y + LegHeight), 10, 100));
            int HigherLegB = world.AddPointMass(new PointMass(new Vector_2d(loc_floor.X - HipSize, loc_floor.Y + LegHeight), 10, 100));
            int LowerBody = world.AddPointMass(new PointMass(new Vector_2d(loc_floor.X, loc_floor.Y + LegHeight + HipSize)));
            int LowerHip = world.AddPointMass(new PointMass(new Vector_2d(loc_floor.X, loc_floor.Y + LegHeight - HipSize)));

            int LowerLegACon = world.AddConnection(new ConnectionStaticDistance(world, LowerLegA, MidLegA));
            int LowerLegBCon = world.AddConnection(new ConnectionStaticDistance(world, LowerLegB, MidLegB));
            int HigherLegACon = world.AddConnection(new ConnectionStaticDistance(world, MidLegA, HigherLegA));
            int HigherLegBCon = world.AddConnection(new ConnectionStaticDistance(world, MidLegB, HigherLegB));
            int HipConA = world.AddConnection(new ConnectionStaticDistance(world, HigherLegA, LowerBody));
            int HipConB = world.AddConnection(new ConnectionStaticDistance(world, HigherLegB, LowerBody));
            int HipConC = world.AddConnection(new ConnectionStaticDistance(world, HigherLegA, LowerHip));
            int HipConD = world.AddConnection(new ConnectionStaticDistance(world, HigherLegB, LowerHip));
            int HipConE = world.AddConnection(new ConnectionStaticDistance(world, HigherLegA, HigherLegB));
            int HipConF = world.AddConnection(new ConnectionStaticDistance(world, LowerBody, LowerHip));
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
            float NeckHeight = 30;
            int LowerNeck = world.AddPointMass(new PointMass(new Vector_2d(loc_floor.X, loc_floor.Y + LegHeight + BodyHeight + ShoulderHeight + (NeckHeight/2))));
            int LowerBodyCon = world.AddConnection(new ConnectionStaticDistance(world, LowerBody, MidBody));
            int HigherBodyCon = world.AddConnection(new ConnectionStaticDistance(world, MidBody, HigherBody));
            int ShoulderConA = world.AddConnection(new ConnectionStaticDistance(world, HigherBody, ShoulderA));
            int ShoulderConB = world.AddConnection(new ConnectionStaticDistance(world, HigherBody, ShoulderB));
            int ShoulderConC = world.AddConnection(new ConnectionStaticDistance(world, HigherBody, LowerNeck));
            int ShoulderConE = world.AddConnection(new ConnectionStaticDistance(world, LowerNeck, ShoulderA));
            int ShoulderConF = world.AddConnection(new ConnectionStaticDistance(world, LowerNeck, ShoulderB));
            int ShoulderCon = world.AddConnection(new ConnectionStaticDistance(world, ShoulderA, ShoulderB));

            world.AddConnection(new ConnectionPushClose(world, HigherLegA, MidBody, 90, HipConA, LowerBodyCon));
            world.AddConnection(new ConnectionPushClose(world, HigherLegB, MidBody, 90, HipConB, LowerBodyCon));
            world.AddConnection(new ConnectionPushClose(world, LowerHip, MidBody, 90, HipConF, LowerBodyCon));
            world.AddConnection(new ConnectionPushClose(world, LowerBody, HigherBody, 90, LowerBodyCon, HigherBodyCon));

            world.AddConnection(new ConnectionPushClose(world, MidBody, LowerNeck, 90, HigherBodyCon, ShoulderConC));
            //world.AddConnection(new ConnectionPushClose(world, HigherLegB, MidBody, 90, HipConB, LowerBodyCon));
            if (player != null)
            {
                player.JointsId.Add(new int[] { LowerLegA, MidLegA, HigherLegA });
                player.JointsId.Add(new int[] { LowerLegB, MidLegB, HigherLegB });
                player.JointsId.Add(new int[] { MidLegA, HigherLegA, LowerBody });
                player.JointsId.Add(new int[] { MidLegB, HigherLegB, LowerBody });
                player.JointsId.Add(new int[] { LowerHip, LowerBody, MidBody });
                player.JointsId.Add(new int[] { LowerBody, MidBody, HigherBody });
                player.JointsId.Add(new int[] { MidBody, HigherBody, LowerNeck });
            }
        }
    }
}