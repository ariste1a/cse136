using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace web136.Hubs
{
    public class chatHub : Hub
    {
        public void login(string user)
        {
            Clients.All.getMessage("User " + user + " has logged in");
        }
        public void postMessage(string message)
        {
            Clients.All.getMessage(message);
        }
    }
}