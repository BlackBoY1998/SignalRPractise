using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalR_New_Demo
{
    public class myHub : Hub
    {
        public void Send(string name, string message)
        {
            Clients.All.AddMessageToPage(name, message);
        }
    }
}