using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(JavascriptClientSummary.Startup))]

namespace JavascriptClientSummary
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/messageConnection", map => 
            {
                map.RunSignalR<MessageConnection>();
            });

            app.Map("/messageHub", map => 
            {
                map.RunSignalR(new Microsoft.AspNet.SignalR.HubConfiguration { EnableJavaScriptProxies = true });
            });
        }
    }
}
