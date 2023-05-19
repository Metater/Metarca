using MessagePack;
using Nerdbank.Streams;
using Metarca.Shared;
using System.Net;
using System.Threading.Channels;
using System.Net.Sockets;

namespace Metarca.Server;

public record Sender(Socket Socket, ChannelReader<(Packet packet, EndPoint remoteEndPoint)> SenderChannel)
{
    private readonly Sequence<byte> writer = new();

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await foreach ((Packet packet, EndPoint remoteEndPoint) in SenderChannel.ReadAllAsync(cancellationToken))
            {
                MessagePackSerializer.Serialize(writer, packet, null, cancellationToken);
                var buffer = writer.AsReadOnlySequence.First;
                await SendToAsync(buffer, remoteEndPoint);
                writer.Reset();
            }
        }
        catch (OperationCanceledException) { }
    }

    private async ValueTask<int> SendToAsync(ReadOnlyMemory<byte> buffer, EndPoint remoteEndPoint)
    {
        return await Socket.SendToAsync(buffer, remoteEndPoint);
    }
}