using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRClient
{
    public partial class Form1 : Form
    {
        private HubConnection connection;
        string urlSignalR = "";

        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private async void btSend_Click(object sender, EventArgs e)
        {
            try
            {
                await connection.InvokeAsync("SendMessage",
                    txtFrom.Text, txtTo.Text,txtMessage.Text);
                messagesList.Items.Add($"{txtFrom.Text}:{txtMessage.Text}");
            }
            catch (Exception ex)
            {
                messagesList.Items.Add(ex.Message);
            }
        }

        private async void btConnect_Click(object sender, EventArgs e)
        {
            urlSignalR = txtSignalRServer.Text;

            connection = new HubConnectionBuilder()
                .WithUrl(urlSignalR)
                .Build();

            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };

            connection.On<string, string, string>("ReceiveMessage", (user, to, message) =>
            {
                this.Invoke((Action)(() =>
                {
                    if (to == txtFrom.Text)
                    {
                        var newMessage = $"{user}: {message}";
                        messagesList.Items.Add(newMessage);
                    }

                }));
            });

            try
            {
                await connection.StartAsync();
                messagesList.Items.Add("Connection started");
                //connectButton.IsEnabled = false;
                //btSend.IsEnabled = true;
            }
            catch (Exception ex)
            {
                messagesList.Items.Add(ex.Message);
            }
        }
    }
}
