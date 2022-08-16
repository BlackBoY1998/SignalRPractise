using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SignalRClient
{
    public partial class WebForm1 : System.Web.UI.Page
    {

        public HubConnection hubconnection = null;
        public IHubProxy HubProxy = null;
        string SignalRCon = ConfigurationManager.AppSettings["SignalRHub"];
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            hubconnection = new HubConnection(SignalRCon);

            HubProxy = hubconnection.CreateHubProxy("MyHub");
            hubconnection.Start();
            hubconnection.Start().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Console.WriteLine("There was an error opening the connection:{0}",
                                      task.Exception.GetBaseException());

                    return;
                }
                else
                {
                    Console.WriteLine("Connected to Server.The ConnectionID is:" + hubconnection.ConnectionId);

                }

            }).Wait();

            var p = HubProxy.Invoke<string>("GetRequest", TextBox1.Text).Result;
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + p + "');", true);

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            hubconnection = new HubConnection(SignalRCon);
            HubProxy = hubconnection.CreateHubProxy("MyHub");
            hubconnection.Start();
            hubconnection.Start().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Console.WriteLine("There was an error opening the connection:{0}",
                                      task.Exception.GetBaseException());

                    return;
                }
                else
                {
                    Console.WriteLine("Connected to Server.The ConnectionID is:" + hubconnection.ConnectionId);

                }

            }).Wait();

            var data = HubProxy.Invoke<string>("GetData", TextBox1.Text,TextBox2.Text).Result;
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + data + "');", true);
        }
    }
}