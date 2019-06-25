using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFinalProject.Service
{
    [HubName("ChatHub")]
    public class ChatHub : Hub
    {
        //[HubMethodName("SendMessage")]
        //public void SendMessage(Dictionary<string, object> message)
        //{
        //    Clients.All.ReceiveMessage(message);
        //}
        [HubMethodName("SendMessage")]
        public void SendMessage(string message)
        {
            Clients.All.ReceiveMessage(message);
        }
    }
}
