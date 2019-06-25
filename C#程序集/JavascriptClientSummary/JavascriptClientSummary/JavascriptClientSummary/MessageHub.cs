using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace JavascriptClientSummary
{
    [HubName("MessageService")]
    public class MessageHub : Hub
    {
        public override Task OnConnected()
        {
            Clients.All.hello(FormatMessage("Welcome!"));
            return base.OnConnected();
        }

        public void Hello(string message)
        {
            Clients.All.hello(FormatMessage(message));
        }

        private string FormatMessage(string message)
        {
            var key = "username";
            var cookieUserName = Context.RequestCookies[key] == null ? string.Empty : Context.RequestCookies[key].Value;
            var qsUserName = Context.QueryString[key];
            var clientType = Clients.Caller.ClientType;
            message = string.Format("cookieUserName:{0},qsUserName:{1},message:{2},clientType:{3}"
                , cookieUserName, qsUserName, message, clientType);
            return message;
        }
    }
}