using MessagePack;

#if UNITY_64
#nullable enable
#endif

namespace Metarca.Shared
{
    [Union(0, typeof(TestPacket))]
    [MessagePackObject]
    public abstract class Packet
    {

    }

    [MessagePackObject]
    public class TestPacket : Packet
    {
        [Key(0)]
        public string? Test { get; set; }
    }
}