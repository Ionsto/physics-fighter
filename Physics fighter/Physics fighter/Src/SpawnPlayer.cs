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
            float LegWidth = 30;
            int LowerLegA = world.AddPointMass(new PointMass(new Vector_2d(loc_floor.X + LegWidth, loc_floor.Y),50));
            int LowerLegB = world.AddPointMass(new PointMass(new Vector_2d(loc_floor.X - LegWidth, loc_floor.Y),50));
            float LegTaper = 6;
            float LegHeight = 60;
            int MidLegA = world.AddPointMass(new PointMass(new Vector_2d(loc_floor.X + LegWidth - LegTaper, loc_floor.Y + (LegHeight / 2))));
            int MidLegB = world.AddPointMass(new PointMass(new Vector_2d(loc_floor.X - LegWidth + LegTaper, loc_floor.Y + (LegHeight / 2))));
            float HipSize = 15;
            int HigherLegA = world.AddPointMass(new PointMass(new Vector_2d(loc_floor.X + HipSize, loc_floor.Y + LegHeight)));
            int HigherLegB = world.AddPointMass(new PointMass(new Vector_2d(loc_floor.X - HipSize, loc_floor.Y + LegHeight)));
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
            //world.AddConnection(new ConnectionPushClose(world, LowerLegA, HigherLegA, 90, LowerLegACon, HigherLegACon));
            //world.AddConnection(new ConnectionPushClose(world, LowerLegB, HigherLegB, 90, LowerLegBCon, HigherLegBCon));
            //world.AddConnection(new ConnectionPushClose(world, LowerBody, MidLegA, 90, HipConA, HigherLegACon));
            //world.AddConnection(new ConnectionPushClose(world, LowerBody, MidLegB, 90, HipConB, HigherLegBCon));
            //Body
            float BodyHeight = 30;
            int MidBody = world.AddPointMass(new PointMass(new Vector_2d(loc_floor.X, loc_floor.Y + LegHeight + HipSize + (BodyHeight / 2))));
            int HigherBody = world.AddPointMass(new PointMass(new Vector_2d(loc_floor.X, loc_floor.Y + LegHeight + HipSize + BodyHeight)));
            int ShoulderWidth = 15;
            int ShoulderHeight = 10;
            int ShoulderA = world.AddPointMass(new PointMass(new Vector_2d(loc_floor.X + ShoulderWidth, loc_floor.Y + LegHeight + HipSize + BodyHeight + ShoulderHeight)));
            int ShoulderB = world.AddPointMass(new PointMass(new Vector_2d(loc_floor.X - ShoulderWidth, loc_floor.Y + LegHeight + HipSize + BodyHeight + ShoulderHeight)));
            float NeckHeight = 30;
            int LowerNeck = world.AddPointMass(new PointMass(new Vector_2d(loc_floor.X, loc_floor.Y + LegHeight + HipSize + BodyHeight + ShoulderHeight + (NeckHeight / 2))));
            int HigherNeck = world.AddPointMass(new PointMass(new Vector_2d(loc_floor.X, loc_floor.Y + LegHeight + HipSize + BodyHeight + ShoulderHeight + (NeckHeight))));
            int HeadSize = 20;
            int TopHead = world.AddPointMass(new PointMass(new Vector_2d(loc_floor.X, loc_floor.Y + LegHeight + HipSize + BodyHeight + ShoulderHeight + NeckHeight + HeadSize)));
            int HeadA = world.AddPointMass(new PointMass(new Vector_2d(loc_floor.X + HeadSize, loc_floor.Y + LegHeight + HipSize + BodyHeight + ShoulderHeight + NeckHeight + (HeadSize / 2))));
            int HeadB = world.AddPointMass(new PointMass(new Vector_2d(loc_floor.X - HeadSize, loc_floor.Y + LegHeight + HipSize + BodyHeight + ShoulderHeight + NeckHeight + (HeadSize / 2))));

            int ArmTaper = 30;
            int UpperArmDrop = 10;
            int LowerArmDrop = 20;
            int MidArmA = world.AddPointMass(new PointMass(new Vector_2d(loc_floor.X + ShoulderWidth + (ArmTaper / 2), loc_floor.Y + LegHeight + HipSize + BodyHeight + ShoulderHeight - UpperArmDrop)));
            int MidArmB = world.AddPointMass(new PointMass(new Vector_2d(loc_floor.X - (ShoulderWidth + (ArmTaper / 2)), loc_floor.Y + LegHeight + HipSize + BodyHeight + ShoulderHeight - UpperArmDrop)));
            int LowerArmA = world.AddPointMass(new PointMass(new Vector_2d(loc_floor.X + ShoulderWidth + ArmTaper, loc_floor.Y + LegHeight + HipSize + BodyHeight + ShoulderHeight - (UpperArmDrop + LowerArmDrop))));
            int LowerArmB = world.AddPointMass(new PointMass(new Vector_2d(loc_floor.X - (ShoulderWidth + ArmTaper), loc_floor.Y + LegHeight + HipSize + BodyHeight + ShoulderHeight - (UpperArmDrop + LowerArmDrop))));
            
            int LowerBodyCon = world.AddConnection(new ConnectionStaticDistance(world, LowerBody, MidBody));
            int HigherBodyCon = world.AddConnection(new ConnectionStaticDistance(world, MidBody, HigherBody));
            int ShoulderConA = world.AddConnection(new ConnectionStaticDistance(world, HigherBody, ShoulderA));
            int ShoulderConB = world.AddConnection(new ConnectionStaticDistance(world, HigherBody, ShoulderB));
            int ShoulderConC = world.AddConnection(new ConnectionStaticDistance(world, HigherBody, LowerNeck));
            int ShoulderConE = world.AddConnection(new ConnectionStaticDistance(world, LowerNeck, ShoulderA));
            int ShoulderConF = world.AddConnection(new ConnectionStaticDistance(world, LowerNeck, ShoulderB));
            int ShoulderCon = world.AddConnection(new ConnectionStaticDistance(world, ShoulderA, ShoulderB));
            int NeckConA = world.AddConnection(new ConnectionStaticDistance(world, LowerNeck, HigherNeck));
            int NeckConB = world.AddConnection(new ConnectionStaticDistance(world, HigherNeck, TopHead));
            int HeadMidCon = world.AddConnection(new ConnectionStaticDistance(world, HeadA, HeadB));
            int HeadUpperSideConA = world.AddConnection(new ConnectionStaticDistance(world, HeadA, TopHead));
            int HeadUpperSideConB = world.AddConnection(new ConnectionStaticDistance(world, HeadB, TopHead));
            int HeadLowerSideConA = world.AddConnection(new ConnectionStaticDistance(world, HeadA, HigherNeck));
            int HeadLowerSideConB = world.AddConnection(new ConnectionStaticDistance(world, HeadB, HigherNeck));

            int ArmUpperConA = world.AddConnection(new ConnectionStaticDistance(world, ShoulderA, MidArmA));
            int ArmUpperConB = world.AddConnection(new ConnectionStaticDistance(world, ShoulderB, MidArmB));

            int ArmLowerConA = world.AddConnection(new ConnectionStaticDistance(world, MidArmA, LowerArmA));
            int ArmLowerConB = world.AddConnection(new ConnectionStaticDistance(world, MidArmB, LowerArmB));
            

            if (player != null)
            {
                player.JointsId.Add(new int[] { HigherLegA, MidLegA, LowerLegA });
                player.JointsId.Add(new int[] { HigherLegB, MidLegB, LowerLegB });
                player.JointsId.Add(new int[] { LowerBody, HigherLegA, MidLegA });
                player.JointsId.Add(new int[] { LowerBody, HigherLegB, MidLegB  });
                player.JointsId.Add(new int[] { LowerHip, LowerBody, MidBody });
                player.JointsId.Add(new int[] { LowerBody, MidBody, HigherBody });
                player.JointsId.Add(new int[] { MidBody, HigherBody, LowerNeck });
                player.JointsId.Add(new int[] { HigherBody, LowerNeck, HigherNeck });
                player.JointsId.Add(new int[] { LowerNeck, HigherNeck, TopHead });

                player.JointsId.Add(new int[] { HigherBody, ShoulderA, MidArmA });
                player.JointsId.Add(new int[] { HigherBody, ShoulderB, MidArmB });
                player.JointsId.Add(new int[] { ShoulderA, MidArmA, LowerArmA });
                player.JointsId.Add(new int[] { ShoulderB, MidArmB, LowerArmB });
                player.JointsId.Add(new int[] { MidArmA, LowerArmA,-1 });
                world.PointMassList[LowerArmA].State = 5;
                player.JointsId.Add(new int[] { MidArmB, LowerArmB, -1 });
                world.PointMassList[LowerArmB].State = 5;

                player.PointMasses.Add(LowerLegA);
                player.PointMasses.Add(LowerLegB);
                player.PointMasses.Add(HigherLegA);
                player.PointMasses.Add(HigherLegB);
                player.PointMasses.Add(LowerBody);
                player.PointMasses.Add(LowerHip);
                player.Connctions.Add(LowerLegACon);
                player.Connctions.Add(LowerLegBCon);
                player.Connctions.Add(HigherLegACon);
                player.Connctions.Add(HigherLegBCon);
                player.Connctions.Add(HipConA);
                player.Connctions.Add(HipConB);
                player.Connctions.Add(HipConC);
                player.Connctions.Add(HipConD);
                player.Connctions.Add(HipConE);
                player.Connctions.Add(HipConF);

                //Body/head
                player.PointMasses.Add(MidBody);
                player.PointMasses.Add(HigherBody);
                player.PointMasses.Add(ShoulderA);
                player.PointMasses.Add(ShoulderB);
                player.PointMasses.Add(LowerNeck);
                player.PointMasses.Add(HigherNeck);
                player.PointMasses.Add(TopHead);
                player.PointMasses.Add(HeadA);
                player.PointMasses.Add(HeadB);

                //Arms
                player.PointMasses.Add(MidArmA);
                player.PointMasses.Add(MidArmB);
                player.PointMasses.Add(LowerArmA);
                player.PointMasses.Add(LowerArmB);
                player.Connctions.Add(LowerBodyCon);
                player.Connctions.Add(HigherBodyCon);
                player.Connctions.Add(ShoulderConA);
                player.Connctions.Add(ShoulderConB);
                player.Connctions.Add(ShoulderConC);
                player.Connctions.Add(ShoulderConE);
                player.Connctions.Add(ShoulderConF);
                player.Connctions.Add(ShoulderCon);
                player.Connctions.Add(NeckConA);
                player.Connctions.Add(NeckConB);
                player.Connctions.Add(HeadMidCon);
                player.Connctions.Add(HeadUpperSideConA);
                player.Connctions.Add(HeadUpperSideConB);
                player.Connctions.Add(HeadLowerSideConA);
                player.Connctions.Add(HeadLowerSideConB);
                player.Connctions.Add(ArmUpperConA);
                player.Connctions.Add(ArmUpperConB);
                player.Connctions.Add(ArmLowerConA);
                player.Connctions.Add(ArmLowerConB);
            }
        }
    }
}