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

            ApplyTheme(this, true);
        }

        private void ApplyTheme(Control control, bool isDarkTheme)
        {
            control.BackColor = isDarkTheme ? System.Drawing.Color.FromArgb(45, 45, 48) : System.Drawing.SystemColors.Control;
            control.ForeColor = isDarkTheme ? System.Drawing.Color.White : System.Drawing.SystemColors.ControlText;

            if (control is TextBox || control is RichTextBox)
            {
                control.BackColor = isDarkTheme ? System.Drawing.Color.FromArgb(30, 30, 30) : System.Drawing.SystemColors.Window;
                control.ForeColor = isDarkTheme ? System.Drawing.Color.White : System.Drawing.SystemColors.WindowText;
                if (control is TextBoxBase textBoxBase)
                {
                    textBoxBase.BorderStyle = BorderStyle.FixedSingle;
                }
            }
            
            if (control is Button button)
            {
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderColor = isDarkTheme ? System.Drawing.Color.FromArgb(100, 100, 100) : System.Drawing.SystemColors.ControlDark;
                button.BackColor = isDarkTheme ? System.Drawing.Color.FromArgb(60, 60, 60) : System.Drawing.SystemColors.Control;
            }

            foreach (Control child in control.Controls)
            {
                ApplyTheme(child, isDarkTheme);
            }
        }
        
        private void themeToggleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ApplyTheme(this, themeToggleCheckBox.Checked);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var ipAddress = hostTextBox.Text;
            var port = int.Parse(portTextBox.Text);
            _client.Nickname = nicknameTextBox.Text;
            try
            {
                _client.Connect(ipAddress, port);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            statusLabel.Text = $@"Connected to {ipAddress}:{port}";
            hostTextBox.Enabled = false;
            portTextBox.Enabled = false;
            nicknameTextBox.Enabled = false;
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
            statusLabel.Text = @"Disconnected";
            hostTextBox.Enabled = true;
            portTextBox.Enabled = true;
            nicknameTextBox.Enabled = true;
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