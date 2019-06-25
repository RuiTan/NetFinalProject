using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace JavascriptClientSummary
{
    public class MessageConnection : PersistentConnection
    {
        protected override Task OnConnected(IRequest request, string connectionId)
        {
            return Connection.Send(connectionId, FormatMessage(request, "Welcome!"));
        }

        protected override Task OnReceived(IRequest request, string connectionId, string data)
        {
            return Connection.Broadcast(FormatMessage(request, data));
        }

        private string FormatMessage(IRequest request, string message)
        {
            var key = "username";
            var cookieUserName = request.Cookies[key] == null ? string.Empty : request.Cookies[key].Value;
            var qsUserName = request.QueryString[key];
            message = string.Format("cookieUserName:{0},qsUserName:{1},message:{2}", cookieUserName, qsUserName, message);
            return message;
        }
    }
}