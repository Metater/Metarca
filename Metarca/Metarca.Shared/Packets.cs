using LiteNetLib.Utils;
using System;

#nullable disable
namespace Metarca.Shared
{
    public class TimePacket
    {
        public static int SizeOf = 8;

        public double Time { get; set; }
    }

    public class InputPacket
    {
        public Data Input { get; set; }
        
        public struct Data : INetSerializable
        {
            private byte data;
            private WASD wasd;

            public WASD WASD
            {
                get
                {
                    return (WASD)(data & 0xF); // Extract and cast wasd data
                }
                set
                {
                    data &= 0xF0; // Keep only non-wasd data
                    data |= (byte)(((int)value) & 0xF); // Slide in wasd data, ensuring it is only the 4 wasd bits
                }
            }

            public void Serialize(NetDataWriter writer)
            {
                writer.Put(data);
            }

            public void Deserialize(NetDataReader reader)
            {
                data = reader.GetByte();
            }
        }

        [Flags]
        public enum WASD : byte
        {
            None = 0,
            W = 1 << 0,
            A = 1 << 1,
            S = 1 << 2,
            D = 1 << 3
        }
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