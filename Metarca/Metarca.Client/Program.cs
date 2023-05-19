using MessagePack;
using Metarca.Shared;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

Console.WriteLine("Hello, World!");

Socket socket = new(AddressFamily.InterNetworkV6, SocketType.Dgram, ProtocolType.Udp)
{
    DualMode = true,
    SendBufferSize = 8192,
    ReceiveBufferSize = 8192,
};

socket.Connect(IPAddress.IPv6Loopback, 7777);

Packet packet = new TestPacket
{
    Test = "hello"
};
var data = MessagePackSerializer.Serialize(packet);

while (!Console.KeyAvailable)
{
    socket.Send(data);
}

Console.ReadLine();
Console.ReadLine();