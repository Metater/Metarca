using MessagePack;
using Metarca.Shared;
using System.Net;
using System.Net.Sockets;
using System.Threading.Channels;

namespace Metarca.Server;

public record Receiver(Socket Socket, ChannelWriter<(Packet packet, EndPoint remoteEndPoint)> ReceiverChannel)
{
    private readonly Memory<byte> memory = GC.AllocateArray<byte>(length: 508, pinned: true).AsMemory();
    private readonly EndPoint blankEndPoint = new IPEndPoint(IPAddress.Any, 0);

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                var result = await Socket.ReceiveFromAsync(memory, SocketFlags.None, blankEndPoint, cancellationToken);
                var buffer = memory[..result.ReceivedBytes];
                var remoteEndPoint = result.RemoteEndPoint;
                var packet = MessagePackSerializer.Deserialize<Packet>(buffer, out int bytesRead, cancellationToken);
                Console.WriteLine(bytesRead);
                await ReceiverChannel.WriteAsync((packet, remoteEndPoint), cancellationToken);
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (MessagePackSerializationException)
            {
                continue;
            }
        }
    }
}