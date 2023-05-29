# TCP Demo App (Server & Client)

This is a simple TCP demo application consisting of a server and a client, developed using C# .NET WinForms. 
The solution contains two projects: **TCP-Client** and **TCP-Server**. The server and client communicate with each 
other over a TCP/IP connection, allowing them to exchange messages.

![screenshot tcp demo server and clients](https://imgur.com/lGQr5Jq.png)

## Getting Started

Clone the repository to your local machine and open the solution in Visual Studio/Rider.

Then, run both Server and Client app.

![run app](https://imgur.com/5VIao5v.png)

### Connecting Client - Server in the same machine

<picture>
  <source media="(prefers-color-scheme: dark)" srcset="https://imgur.com/HJzf7NX.png">
  <img alt="Localhost diagram" src="https://imgur.com/UcQpTxf.png">
</picture>

The client and server are configured to connect to each other using the loopback address.

![screenshot localhost setup](https://imgur.com/viwIKcw.png)

In TCP Server app, click `Start Server` button to start the server. In TCP Client app, click `Connect` button to connect to the server.
Now, you can start sending messages between the client and server.

#### Connecting Client - Server in different machines

<picture>
  <source media="(prefers-color-scheme: dark)" srcset="https://imgur.com/v0lkClj.png">
  <img alt="Local network diagram" src="https://imgur.com/fOL8DMC.png">
</picture>

In this section, I will be using Android app since I don't have extra laptop to test this out. Feel free to use any other TCP Client
on any devices.

Download [TCP Client](https://play.google.com/store/apps/details?id=com.hardcodedjoy.tcpclient) app from the Google Play Store.

Start the server, note the IP address of the server. Open the TCP Client app and enter the
information about the server. Then, click on Connect.

![tcp client setting](https://imgur.com/osAySn0.png)

Now, you can start sending messages between the client and server.

![tcp client connected](https://imgur.com/tttJfmw.png)
