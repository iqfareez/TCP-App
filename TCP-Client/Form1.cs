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
            // 'Marshalled' to the UI thread using MethodInvoker
            // NOTE: MethodInvoker can also be replaced with Action (like in slide 8, pg 27)
            // According to SO, MethodInvoker is better use case than Action
            // https://stackoverflow.com/questions/1167771/methodinvoker-vs-action-for-control-begininvoke
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

        private void ledToggleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // Send LED ON/OFF command to the server
            _client.SendMessage(ledToggleCheckBox.Checked ? "LED ON" : "LED OFF");
        }
    }
}