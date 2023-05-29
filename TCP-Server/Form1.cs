using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace TCP_Server
{
    public partial class Form1 : Form
    {
        private MyTcpServer _server;
        private bool serverConnected = false;

        public Form1()
        {
            InitializeComponent();
            
            _server = new MyTcpServer(IPAddress.Any.ToString());
            _server.MessageReceived += Server_MessageReceived;
        }
        
        private void Server_MessageReceived(object sender, string message)
        {
            // Update the TextBox control with the received message
            logTextBox.Invoke((MethodInvoker)(() =>
            {
                logTextBox.AppendText(message + Environment.NewLine);
            }));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!serverConnected)
            {
                try
                {
                    _server.Start();
                    localNetworkLabel.Text = $@"127.0.0.1:{_server.Port}";
                    lanNetworkLabel.Text = $@"{GetDeviceLocalIp()}:{_server.Port}";
                    button1.Text = "Stop server";
                    serverConnected = true;
                }
                catch (Exception exception)
                {
                    logTextBox.AppendText(exception.Message + Environment.NewLine);
                }
            }
            else
            {
                _server.Stop();
                serverConnected = false;
                localNetworkLabel.Text = @"N/A";
                lanNetworkLabel.Text = @"N/A";
                button1.Text = "Start server";
                logTextBox.Clear();
            }
        }

        static string GetDeviceLocalIp()
        {
            // Credit https://stackoverflow.com/a/57248926/13617136
            IPHostEntry entry = Dns.GetHostEntry(Dns.GetHostName());

            // find IPv4 address
            List<string> ips = (from ip in entry.AddressList where ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork select ip.ToString()).ToList();

            return ips[0];
        }
        
    }
}