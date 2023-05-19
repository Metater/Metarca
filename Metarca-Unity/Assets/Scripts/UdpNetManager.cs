using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;

public class UdpNetManager
{
    private readonly Socket socket = new(AddressFamily.InterNetworkV6, SocketType.Dgram, ProtocolType.Udp)
    {
        DualMode = true,
        SendBufferSize = 8192,
        ReceiveBufferSize = 8192,
    };
    private readonly byte[] memory = new byte[508];
    private EndPoint blankEndPoint = new IPEndPoint(IPAddress.Any, 0);
    private readonly Thread receiveThread;

    public UdpNetManager(int port)
    {
        socket.Connect(IPAddress.IPv6Loopback, port);

        receiveThread = new(Receive);

        receiveThread.Start();
    }

    public void Send()
    {
        // socket.Send(buffer);
    }

    public void Stop()
    {
        receiveThread.Abort();
    }

    private void Receive()
    {
        while (true)
        {
            int receivedBytes = socket.Receive(memory);
            byte[] packet = memory[..receivedBytes];
            byte packetId = packet[0];
        }
    }
}
