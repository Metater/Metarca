using LiteNetLib.Utils;

#nullable disable
namespace Metarca.Shared
{
    public class TimePacket
    {
        public static int SizeOf = 8;

        public double Time { get; set; }
    }

    public class DebugEntityPacket
    {
        public static int SizeOf(int count) => 8 + 2 + (count * DebugEntity.SizeOf);

        public DebugEntity[] Entities { get; set; }
    }

    public struct DebugEntity : INetSerializable
    {
        public static int SizeOf = 1 + 4 + 4;

        public byte Id { get; set; }
        public float X { get; set; }
        public float Y { get; set; }

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(Id);
            writer.Put(X);
            writer.Put(Y);
        }

        public void Deserialize(NetDataReader reader)
        {
            Id = reader.GetByte();
            X = reader.GetFloat();
            Y = reader.GetFloat();
        }
    }
}
#nullable enable