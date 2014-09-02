using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Physics_fighter.Src;

namespace Physics_fighter.Hubs
{
    public class GameHub : Hub
    {
        //This updates the physic frames on all of the clients
        public void SetObjectFrame(int id,int frame, float posx, float posy,string colour)
        {
            Clients.All.SetObjectFrame(id,frame,posx,posy,colour);
        }
        //this function is called by the clients and applys the forces to the user bodys allong with readying them
        public void ReadyState(string Name,int []A,int [] Mid, int []B,int[] State)
        {
            Game.MakeWorldSafe();
            if (Game.World.Frame < Game.World.MaxFrames-1)
            {
                Game.World.ReadyList.Add(Name);
                for (int i = 0; i < Mid.Length; ++i)
                {
                    //Random rnd = new Random();
                    //State[i] = (int)Math.Floor((rnd.NextDouble() * 5));
                    if (Game.World.PointMassList[Mid[i]].State != State[i])
                    {
                        Game.World.PointMassList[Mid[i]].State = State[i];
                        Player player = Game.World.PlayerList[Game.World.PlayerNameList.IndexOf(Name)];
                        for (int j = 0; j < player.JointActuators.Count;++j)
                        {
                            if(player.JointActuators[j][1] == Mid[i])
                            {
                                if (player.JointActuators[j][0] != -1)
                                {
                                    if (Game.World.ConnectionList[player.JointActuators[j][0]] != null)
                                    {
                                        Game.World.ConnectionList[player.JointActuators[j][0]].Destroy(Game.World);
                                    }
                                }
                            }
                        }
                        if (State[i] == 1)
                        {
                            player.JointActuators.Add(new int[]{Game.World.AddConnection(new ConnectionStaticDistance(Game.World, A[i], B[i])),Mid[i]});
                        }
                        if (State[i] == 2)
                        {
                            float Distance = 1;
                            foreach(int k in Game.World.PointMassList[Mid[i]].Connected)
                            {
                                if (k != -1)
                                {
                                    if (Game.World.ConnectionList[k].PointA == A[i] || Game.World.ConnectionList[k].PointB == A[i])
                                    {
                                        Distance += Game.World.ConnectionList[k].UsedDistance;
                                    }
                                    if (Game.World.ConnectionList[k].PointA == B[i] || Game.World.ConnectionList[k].PointB == B[i])
                                    {
                                        Distance += Game.World.ConnectionList[k].UsedDistance;
                                    }
                                }
                            }
                            player.JointActuators.Add(new int[]{Game.World.AddConnection(new ConnectionStaticDistance(Game.World, A[i], B[i],Distance)),Mid[i]});
                        }
                        if (State[i] == 3)
                        {
                            player.JointActuators.Add(new int[] { Game.World.AddConnection(new ConnectionRotateToDirection(Game.World, A[i], Mid[i], B[i], false)), Mid[i] });
                        }
                        if (State[i] == 4)
                        {
                            player.JointActuators.Add(new int[] { Game.World.AddConnection(new ConnectionRotateToDirection(Game.World, A[i], Mid[i], B[i], true)), Mid[i] });
                        }
                        if(State[i] != 0)
                        {
                            if (player.JointActuators.Last()[0] != -1)
                            {
                                Game.World.ConnectionList[player.JointActuators.Last()[0]].Render = false;
                                //Game.World.ConnectionList[player.JointActuators.Last()[0]].MaxForce = Game.World.PointMassList[Mid[i]].ForceApplied;
                                Game.World.ConnectionList[player.JointActuators.Last()[0]].Force = 1F;// Game.World.DeltaTime;//
                                Game.World.ConnectionList[player.JointActuators.Last()[0]].Stiffness = 0.8F;//

                                //Game.World.ConnectionList[player.JointActuators.Last()[0]].ForceStep = 1F;
                            }
                        }
                    }
                }
                Clients.All.setPlayerList(Game.World.ReadyList);
                if (Game.World.ReadyList.Count == Game.World.PlayerNameList.Count)
                {
                    int Count = Math.Min(Game.World.FrameStep,(Game.World.MaxFrames-1) - Game.World.Frame);
                    for (int i = 0; i < Count; ++i)
                    {
                        Game.World.Update();
                        SendRenderData(Clients.All);
                    }
                    Game.World.ReadyList.Clear();
                    Clients.All.renderFrameSet(Game.World.Frame - Count, Count);
                }
                Clients.All.setPlayerList(Game.World.ReadyList);
            }
            if (Game.World.Frame >= Game.World.MaxFrames-1)
            {
                Clients.All.gameOver();
            }
        }
        public void RequestJoints(string Name)
        {
            Player player = Game.World.PlayerList[Game.World.PlayerNameList.IndexOf(Name)];
            List<int> A = new List<int>();
            List<int> B = new List<int>();
            List<int> Mid = new List<int>();
            List<float> X = new List<float>();
            List<float> Y = new List<float>();
            List<string> Colour = new List<string>();
            List<int> State = new List<int>();
            for (int i = 0; i < player.JointsId.Count;++i )
            {
                bool JointNull = false;
                for(int j = 0;j < 3;++j)
                {
                    if(Game.World.PointMassList[player.JointsId[i][j]] == null)
                    {
                        player.JointsId.RemoveAt(i);
                        JointNull = true;;
                        break;
                    }
                }
                if (!JointNull)
                {
                    // A -- Mid
                    //         \
                    //          B
                    A.Add(player.JointsId[i][0]);
                    Mid.Add(player.JointsId[i][1]);
                    B.Add(player.JointsId[i][2]);
                    X.Add(Game.World.PointMassList[player.JointsId[i][1]].Pos.X);
                    Y.Add(Game.World.PointMassList[player.JointsId[i][1]].Pos.Y);
                    State.Add(Game.World.PointMassList[player.JointsId[i][1]].State);
                }
            }
            Clients.Caller.sendJoints(A.Count,A,B,Mid,X,Y,Colour,State);
        }
        public void RequestSettings()
        {
            Game.MakeWorldSafe();
            int[] Settings = new int[2];
            Settings[0] = Game.World.MaxFrames;
            Settings[1] = Game.World.ConnectionList.Length;
            Clients.Caller.initSettings(Settings);
            //Preliminary
            SendRenderData(Clients.Caller);
            Clients.Caller.renderFrameSet(0,1);
        }
        private void SendRenderData(dynamic to)
        {
            for (int j = 0; j < Game.World.ConnectionList.Length; ++j)
            {
                if (Game.World.ConnectionList[j] != null)
                {
                    if (Game.World.ConnectionList[j].Render)
                    {
                        Vector_2d PosA = Game.World.PointMassList[Game.World.ConnectionList[j].PointA].Pos;
                        Vector_2d PosB = Game.World.PointMassList[Game.World.ConnectionList[j].PointB].Pos;
                        float Color = Game.World.ConnectionList[j].Damadge/100;
                        Color *= 255;
                        int Colour = (int)Color;
                        to.setObjectFrame(j, Game.World.Frame, PosA.X, PosA.Y, PosB.X, PosB.Y, "#FF" + Colour.ToString("X") + Colour.ToString("X"));
                    }
                }
            }
        }
    }
}