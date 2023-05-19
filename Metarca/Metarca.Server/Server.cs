using Metarca.Shared;
using System.Net;
using System.Threading.Channels;

namespace Metarca.Server;

public record class Server(ChannelReader<(Packet packet, EndPoint remoteEndPoint)> ReceiverChannel, ChannelWriter<(Packet packet, EndPoint remoteEndPoint)> SenderChannel)
{
    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
		try
		{
            await foreach ((Packet packet, EndPoint remoteEndPoint) in ReceiverChannel.ReadAllAsync(cancellationToken))
            {
                await OnReceivedPacketAsync(packet, remoteEndPoint, cancellationToken);
            }
        }
		catch (OperationCanceledException) { }
    }

    private async Task OnReceivedPacketAsync(Packet receivedPacket, EndPoint remoteEndPoint, CancellationToken cancellationToken = default)
    {
        switch (receivedPacket)
        {
            case TestPacket packet:
                await SenderChannel.WriteAsync((packet, remoteEndPoint), cancellationToken);
                break;
        }
    }
}