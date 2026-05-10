using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCP_Server
{
    public class MyTcpServer
    {
        private TcpListener _server;
        private readonly string _ipAddress;
        private readonly int _port;
        private readonly System.Collections.Concurrent.ConcurrentDictionary<System.Net.EndPoint, string> _nicknames = new System.Collections.Concurrent.ConcurrentDictionary<System.Net.EndPoint, string>();

        public event EventHandler<string> MessageReceived;

        public int Port => _port;

        /// <summary>
        /// TCP Server helper class
        /// </summary>
        /// <param name="ipAddress">Default at loopback IP (Cannot be accessed from the outside world)</param>
        /// <param name="port"></param>
        public MyTcpServer(string ipAddress = "127.0.0.1", int port = 8083)
        {
            _ipAddress = ipAddress;
            _port = port;
        }

        public async Task Start()
        {
            // Abort operation if server is already running
            if (_server != null)
            {
                throw new InvalidOperationException("Server is already running!");
            }

            IPAddress localAddr = IPAddress.Parse(_ipAddress);
            _server = new TcpListener(localAddr, _port);
            _server.Start();

            OnMessageReceived("Server started. Waiting for a connection...");

            while (true)
            {
                // Handle new client connection
                TcpClient client = await _server.AcceptTcpClientAsync();
                OnMessageReceived($"Connected! Client IP: {client.Client.RemoteEndPoint}");
                _ = HandleClientAsync(client);
            }
        }

        public void Stop()
        {
            _server?.Stop();
            _server = null;
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            NetworkStream stream = client.GetStream();

            byte[] buffer = new byte[1024];
            StringBuilder messageBuilder = new StringBuilder();

            // Receive data in a loop until the client disconnects
            while (true)
            {
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

                if (bytesRead == 0)
                {
                    OnMessageReceived($"Client {client.Client.RemoteEndPoint} disconnected.");
                    break; // Client disconnected
                }

                string data = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                messageBuilder.Append(data);

                if (data.Length > 0)
                {
                    string receivedMessage = messageBuilder.ToString().TrimEnd();

                    if (receivedMessage.StartsWith("NICK:"))
                    {
                        string nick = receivedMessage.Substring(5);
                        _nicknames[client.Client.RemoteEndPoint] = nick;
                        OnMessageReceived($"{client.Client.RemoteEndPoint} identified as: {nick}");
                        messageBuilder.Clear();
                        continue;
                    }

                    string displayName = _nicknames.TryGetValue(client.Client.RemoteEndPoint, out var name) ? name : client.Client.RemoteEndPoint.ToString();

                    // Raise the MessageReceived event
                    OnMessageReceived($"{displayName}: {receivedMessage}");

                    byte[] response = Encoding.UTF8.GetBytes(receivedMessage + Environment.NewLine);
                    await stream.WriteAsync(response, 0, response.Length);

                    messageBuilder.Clear();
                }
            }
        }

        /// <summary>
        /// Send message to the UI thread
        /// </summary>
        /// <param name="message"></param>
        protected virtual void OnMessageReceived(string message)
        {
            MessageReceived?.Invoke(this, message);
        }
    }
}