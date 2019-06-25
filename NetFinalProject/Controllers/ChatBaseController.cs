﻿using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace NetFinalProject.Controllers
{
    [EnableCors("*", "*", "*")]
    public class ChatBaseController<T> : ApiController where T : Hub
    {
        protected IHubConnectionContext<dynamic> Clients { get; private set; }
        protected IGroupManager Groups { get; private set; }
        protected ChatBaseController()
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<T>();
            Clients = context.Clients;
            Groups = context.Groups;
        }
    }
}
