using LiteNetLib.Utils;
using System;

namespace Metarca.Shared.Packets;

public class InputPacket
{
    public static int SizeOf = 1;

    public Data Input { get; set; }

    public struct Data : INetSerializable
    {
        private byte data;

        public WASD WASD
        {
            get
            {
                // Extract and cast wasd data
                return (WASD)(data & 0xF);
            }
            set
            {
                // Keep only non-wasd data
                data &= 0xF0;
                // Slide in wasd data, ensuring it is only the 4 wasd bits
                data |= (byte)(((int)value) & 0xF);
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