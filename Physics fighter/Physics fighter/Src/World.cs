using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Physics_fighter.Src
{
    public class World
    {
        public int FrameStep = 10;
        public int MaxFrames = 100;
        public int DeltaSteps = 50;
        public float DeltaTime;
        public int ContraintSteps = 10;
        public float DeltaConstraint;
        public float BufferSize = 10;
        public Vector_2d Size = new Vector_2d(500,500);
        public int Frame = 0;
        int CurrentSpawnLoc = 0;
        public List<Vector_2d> SpawnLocations = new List<Vector_2d>();
        public List<Player> PlayerList = new List<Player>();
        public List<String> PlayerNameList = new List<String>();
        public List<String> ReadyList = new List<String>();
        public PointMass[] PointMassList;
        public Connection[] ConnectionList;
        public World(int PointMassCount, int ConnectionCount)
        {
            PointMassList = new PointMass[PointMassCount];
            for (int i = 0; i < PointMassList.Length; ++i)
            {
                PointMassList[i] = null;
            }
            ConnectionList = new Connection[ConnectionCount];
            for (int i = 0; i < ConnectionList.Length; ++i)
            {
                ConnectionList[i] = null;
            }
            SpawnLocations.Add(new Vector_2d(100, 50));
            SpawnLocations.Add(new Vector_2d(210, 50));
            DeltaTime = 1.0F / DeltaSteps;
            DeltaConstraint = 1.0F / ContraintSteps;
        }
        public void InitPlayers()
        {
            for (int i = 0; i < PlayerNameList.Count;++i)
            {
                Player player = new Player();
                player.Name = PlayerNameList[i];
                player.Team = i;
                PlayerList.Add(player);
                if(CurrentSpawnLoc < SpawnLocations.Count)
                {
                    player.Spawn(this,SpawnLocations[CurrentSpawnLoc++]);
                }
            }
        }
        public int AddPointMass(PointMass point)
        {
            for (int i = 0; i < PointMassList.Length; ++i)
            {
                if (PointMassList[i] == null)
                {
                    point.Id = i;
                    PointMassList[i] = point;
                    return i;
                }
            }
            return -1;
        }
        public int AddConnection(Connection connection)
        {
            for (int i = 0; i < ConnectionList.Length; ++i)
            {
                if (ConnectionList[i] == null)
                {
                    connection.Id = i;
                    ConnectionList[i] = connection;
                    PointMassList[connection.PointA].Connected.Add(i);
                    PointMassList[connection.PointB].Connected.Add(i);
                    return i;
                }
            }
            return -1;
        }
        public void Update()
        {
            for (int step = 0; step < DeltaSteps; ++step)
            {
                for (int i = 0; i < PointMassList.Length; ++i)
                {
                    if (PointMassList[i] != null)
                    {
                        PointMassList[i].Update(this);
                    }
                }
                Collision();
                for (int j = 0; j < ContraintSteps; ++j)
                {
                    for (int i = 0; i < ConnectionList.Length; ++i)
                    {
                        if (ConnectionList[i] != null)
                        {
                            ConnectionList[i].Update(this);
                        }
                    }
                    for (int i = 0; i < PointMassList.Length; ++i)
                    {
                        if (PointMassList[i] != null)
                        {
                            PointMassList[i].CheckBounds(this);
                        }
                    }
                }
            }
            ++Frame;
        }
        public void Collision()
        {
            for (int i = 0; i < ConnectionList.Length - 1; ++i)
            {
                if (ConnectionList[i] != null)
                {
                    for (int j = i + 1; j < ConnectionList.Length; ++j)
                    {
                        if (ConnectionList[j] != null)
                        {
                            //Do collision
                            if (get_line_intersection(PointMassList[ConnectionList[i].PointA].Pos,PointMassList[ConnectionList[i].PointB].Pos,PointMassList[ConnectionList[j].PointA].Pos,PointMassList[ConnectionList[j].PointB].Pos) != null)
                            {
                                PointMass[] Points = new PointMass[4];
                                Points[0] = PointMassList[ConnectionList[i].PointA];
                                Points[1] = PointMassList[ConnectionList[i].PointB];
                                Points[2] = PointMassList[ConnectionList[j].PointA];
                                Points[3] = PointMassList[ConnectionList[j].PointB];
                                VelocityColl(Points[0], ConnectionList[j]);
                                VelocityColl(Points[1], ConnectionList[j]);
                                VelocityColl(Points[2], ConnectionList[i]);
                                VelocityColl(Points[3], ConnectionList[i]);
                                /*if (ConnectedAtoB(Points[0], PointMassList[2]) && ConnectedAtoB(Points[2], PointMassList[0]))
                                {
                                    if (ConnectedAtoB(Points[1], PointMassList[3]) && ConnectedAtoB(Points[3], PointMassList[1]))
                                    {
                                        Coll(Points[0], Points[2]);
                                        Coll(Points[0], Points[3]);
                                        Coll(Points[1], Points[2]);
                                        Coll(Points[1], Points[3]);

                                    }
                                }*/
                            }
                        }
                    }
                }
            }
        }
        public bool ConnectedAtoB(PointMass a,PointMass b)
        {
            foreach(int k in a.Connected)
            {
                if (ConnectionList[k].PointA == b.Id || ConnectionList[k].PointB == b.Id)
                {
                    return true;
                }
            }
            return false;
        }
        public void Coll(PointMass a,PointMass b)
        {
            Vector_2d diff = a.Pos.Sub(b.Pos);
            float InputForce = 1.0F/(float)Math.Sqrt(diff.Dot(diff));
            diff.Div(InputForce);
            diff.Mult(0.5F);
            a.Accerate(diff,this);
            b.Accerate(diff.Inverted(),this);
        }
        public void VelocityColl(PointMass A,Connection B)
        {
            Vector_2d pos = get_line_intersection(A.Pos, A.OldPos,  PointMassList[B.PointA].Pos, PointMassList[B.PointB].Pos);
            if (pos != null)
            {
                float COR = 0.98F;//Coefficent of restetution
                Vector_2d ConB = PointMassList[B.PointA].Pos.Sub(PointMassList[B.PointB].Pos);
                Vector_2d ConBPerpNormal = ConB.Perpendicular().Div((float)Math.Sqrt(ConB.Dot(ConB)));
                Vector_2d Velocity = A.OldPos.Sub(A.Pos);
                Vector_2d newVelocity = PointMassList[B.PointA].OldPos.Sub(PointMassList[B.PointA].Pos);
                newVelocity = newVelocity.Add(PointMassList[B.PointB].OldPos.Sub(PointMassList[B.PointB].Pos));
                //newVelocity = newVelocity.Sub(Velocity);
                float Distribution = (float)Math.Sqrt(pos.Sub(PointMassList[B.PointA].Pos).Dot(pos.Sub(PointMassList[B.PointA].Pos))) / B.UsedDistance;

                A.Pos = A.OldPos.Add(newVelocity.Mult(COR));
                PointMassList[B.PointA].Pos = PointMassList[B.PointA].OldPos.Add(Velocity.Mult(1 - Distribution));
                PointMassList[B.PointB].Pos = PointMassList[B.PointB].OldPos.Add(Velocity.Mult(Distribution));
            }
        }
        public Vector_2d get_line_intersection(Vector_2d p0, Vector_2d p1, Vector_2d p2, Vector_2d p3)
        {
            Vector_2d s1 = new Vector_2d();
            Vector_2d s2 = new Vector_2d();
            s1.X = p1.X - p0.X; s1.Y = p1.Y - p0.Y;
            s2.X = p3.X - p2.X; s2.Y = p3.Y - p2.Y;

            float s, t;
            s = (-s1.Y * (p0.X - p2.X) + s1.X * (p0.Y - p2.Y)) / (-s2.X * s1.Y + s1.X * s2.Y);
            t = (s2.X * (p0.Y - p2.Y) - s2.Y * (p0.X - p2.X)) / (-s2.X * s1.Y + s1.X * s2.Y);

            if (s > 0 && s < 1 && t > 0 && t < 1)
            {
                return new Vector_2d(p0.X + (t * s1.X), p0.Y + (t * s1.Y));
            }

            return null; // No collision
        }
    }
}