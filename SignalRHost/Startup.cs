using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using System;
using System.Threading.Tasks;
using System.Web.Cors;

[assembly: OwinStartup(typeof(SignalRHost.Startup))]

namespace SignalRHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.Map("/signalr", map =>
            //{

            //    var hubConfiguration = new HubConfiguration
            //    {
            //        // You can enable JSONP by uncommenting line below.
            //        // JSONP requests are insecure but some older browsers (and some
            //        // versions of IE) require JSONP to work cross domain
            //        EnableJSONP = true,
            //        EnableJavaScriptProxies = true,
            //        EnableDetailedErrors = true
            //    };
            //    // Run the SignalR pipeline. We're not using MapSignalR
            //    // since this branch already runs under the "/signalr"
            //    // path.
            //    map.RunSignalR(hubConfiguration);
            //});
            //app.MapSignalR();
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            //var policy = new CorsPolicy()
            //{
            //    AllowAnyHeader = true,
            //    AllowAnyMethod = true,
            //    SupportsCredentials = true
            //};

            //policy.Origins.Add("http://172.16.2.72:8500"); //be sure to include the port:
            //                              //example: "http://localhost:8081"

            //app.UseCors(new CorsOptions
            //{
            //    PolicyProvider = new CorsPolicyProvider
            //    {
            //        PolicyResolver = context => Task.FromResult(policy)
            //    }
            //});

            //app.MapSignalR();

            app.UseCors(CorsOptions.AllowAll);
            app.Map("/signalr", map =>
            {
                HubConfiguration hcf = new HubConfiguration();
                map.RunSignalR();
            });
        }
    }
}
