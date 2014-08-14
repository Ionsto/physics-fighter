using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Physics_fighter.Src.Hubs
{
    public class GameHub : Hub
    {
        //This updates the physic frames on all of the clients
        public void SetObjectFrame(int id,int frame, float posx, float posy,string colour)
        {
            Clients.All.SetObjectFrame(id,frame,posx,posy,colour);
        }
        //this function is called by the clients and applys the forces to the user bodys allong with readying them
        public void ReadyState()
        {

        }
    }
}