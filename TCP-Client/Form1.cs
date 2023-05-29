using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCP_Client
{
    public partial class Form1 : Form
    {
        MyTcpClient _client;
        public Form1()
        {
            InitializeComponent();
            
            _client = new MyTcpClient();
            _client.MessageReceived += Server_MessageReceived;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var ipAddress = hostTextBox.Text;
            var port = int.Parse(portTextBox.Text);
            try
            {
                _client.Connect(ipAddress, port);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            statusBar1.Text = $@"Connected to {ipAddress}:{port}";
            hostTextBox.Enabled = false;
            portTextBox.Enabled = false;
            disconnectButton.Enabled = true;
            connectButton.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _client.SendMessage(sendTextBox.Text);
        }
        
        private void Server_MessageReceived(object sender, string message)
        {
            // Update the TextBox control with the received message
            messageTextBox.Invoke((MethodInvoker)(() =>
            {
                messageTextBox.AppendText("Server: " + message);
            }));
        }

        private void disconnectButton_Click(object sender, EventArgs e)
        {
            _client.Disconnect();
            statusBar1.Text = @"Disconnected";
            hostTextBox.Enabled = true;
            portTextBox.Enabled = true;
            connectButton.Enabled = true;
            disconnectButton.Enabled = false;
        }
    }
}