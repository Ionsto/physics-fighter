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
        }
    }
}