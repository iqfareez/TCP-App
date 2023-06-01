# TCP Demo App (Server & Client)

This is a simple TCP demo application consisting of a server and a client, developed using C# .NET WinForms. 
The solution contains two projects: **TCP-Client** and **TCP-Server**. The server and client communicate with each 
other over a TCP/IP connection, allowing them to exchange messages.

![screenshot tcp demo server and clients](https://imgur.com/lGQr5Jq.png)

## Getting Started

Clone the repository to your local machine and open the solution in Visual Studio/Rider.

Then, run both Server and Client app.

<picture>
  <source media="(prefers-color-scheme: dark)" srcset="https://imgur.com/DvSv2Om.png">
  <img alt="Localhost diagram" src="https://imgur.com/5VIao5v.png">
</picture>

### Connecting Client - Server in the same machine

<picture>
  <source media="(prefers-color-scheme: dark)" srcset="https://imgur.com/HJzf7NX.png">
  <img alt="Localhost diagram" src="https://imgur.com/UcQpTxf.png">
</picture>

The client and server are configured to connect to each other using the loopback address.

![screenshot localhost setup](https://imgur.com/viwIKcw.png)

> **Note** `localhost` can also be referenced by the loopback IP address `127.0.0.1`. This IP is fixed.

In TCP Server app, click `Start Server` button to start the server. In TCP Client app, click `Connect` button to connect to the server.
Now, you can start sending messages between the client and server.

#### Connecting Client - Server in different machines

<picture>
  <source media="(prefers-color-scheme: dark)" srcset="https://imgur.com/v0lkClj.png">
  <img alt="Local network diagram" src="https://imgur.com/fOL8DMC.png">
</picture>

In this section, I will be using Android since I don't have extra laptop to test this out. Feel free to use any other TCP Client
on any devices.

Download [TCP Client](https://play.google.com/store/apps/details?id=com.hardcodedjoy.tcpclient) app from the Google Play Store.

Please ensure that both devices are connected **to the same network**.

![wifi same network](https://imgur.com/b5ghPyY.png)

From the TCP Server app, start the server. Notice there is an host:port information in the "On your network" section.

![tcps erver app](https://imgur.com/lYayO93.png)

> **Note** Your PC local IP address can also be found by running `ipconfig` in the command prompt.

On your Android device, open the TCP Client app and enter the host and port information from the server app.

![tcp client setting](https://imgur.com/osAySn0.png)

Now, you can start sending messages between the client and server.

![tcp client connected](https://imgur.com/tttJfmw.png)

Demo:

https://github.com/iqfareez/TCP-App/assets/60868965/738f869e-bdec-4e00-b0a3-39a1017b8ace

### Bonus: LED Demo

This is a bonus demo to show how the client can control the virtual 'LED' on the server.

[led tcp demo.webm](https://github.com/iqfareez/TCP-App/assets/60868965/ed0739b3-36ef-4126-9ba5-6fd7529f4a41)

## Releases

Don't want to build it yourself? Find the release artifact [here](https://github.com/iqfareez/TCP-App/actions).

In case the artifacts has expired, just fork this repo and run the workflow by yourself.

## Check this out

- CLI version of TCP Server and Client https://github.com/iqfareez/MCTE-4327-Software-Engineering/tree/main/ConsoleApp2-TCP
- For Software Engineering notes, go to https://github.com/iqfareez/MCTE-4327-Software-Engineering/
- Chat App C# Project from previous semester student https://github.com/zulhafiz-zulkifli/Chat-Application (their chat UI is beautiful)

## References

- [TCP Listener](https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.tcplistener)
- [TCP Client](https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.tcpclient)
- ChatGPT

