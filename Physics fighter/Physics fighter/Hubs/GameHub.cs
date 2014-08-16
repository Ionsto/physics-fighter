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
        public void ReadyState(string Name,int []Ids,float [] Forces)
        {
            Game.MakeWorldSafe();
            Game.World.ReadyList.Add(Name);
            for (int i = 0; i < Ids.Length;++i )
            {
                //Game.World.ConnectionList[Ids[i]].ApplyForce(Forces[i]);
            }
            Clients.All.setPlayerList(Game.World.ReadyList);
            if (Game.World.ReadyList.Count == Game.World.PlayerList.Count)
            {
                for(int i = 0;i < 30;++i)
                {
                    Game.World.Update();
                    for (int j = 0; j < Game.World.ConnectionList.Length; ++j)
                    {
                        if (Game.World.ConnectionList[j] != null)
                        {
                            Vector_2d PosA = Game.World.PointMassList[Game.World.ConnectionList[j].PointA].Pos;
                            Vector_2d PosB = Game.World.PointMassList[Game.World.ConnectionList[j].PointB].Pos;
                            Clients.All.setObjectFrame(j, Game.World.Frame, PosA.X,PosA.Y,PosB.X,PosB.Y, "#FFFFFF");
                        }
                    }
                }
                Game.World.ReadyList.Clear();
                Clients.All.renderFrameSet(Game.World.Frame - 30,30);
            }
            Clients.All.setPlayerList(Game.World.ReadyList);
        }
        public void RequestSettings()
        {
            Game.MakeWorldSafe();
            int[] Settings = new int[2];
            Settings[0] = 300;
            Settings[1] = Game.World.ConnectionList.Length;
            Clients.Caller.initSettings(Settings);
            //Preliminary
            for (int j = 0; j < Game.World.ConnectionList.Length; ++j)
            {
                if (Game.World.ConnectionList[j] != null)
                {
                    Vector_2d PosA = Game.World.PointMassList[Game.World.ConnectionList[j].PointA].Pos;
                    Vector_2d PosB = Game.World.PointMassList[Game.World.ConnectionList[j].PointB].Pos;
                    Clients.Caller.setObjectFrame(j, Game.World.Frame, PosA.X, PosA.Y, PosB.X, PosB.Y, "#FFFFFF");
                }
            }
            Clients.Caller.renderFrameSet(0,1);
        }
    }
}