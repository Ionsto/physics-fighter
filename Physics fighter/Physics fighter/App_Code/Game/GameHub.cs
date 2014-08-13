﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Physics_fighter
{
    public class GameHub : Hub
    {
        public void SetObjectFrame(int id,int frame, float posx, float posy,string colour)
        {
            Clients.All.SetObjectFrame(id,frame,posx,posy,colour);
        }
    }
}