using System;
using System.Collections.Generic;
using System.Text;

namespace CollaborationBus.Entity
{
    internal class GP
    {
        private byte[] _flag;
        private byte[] _version;
        private int _dataLen;
        private byte[] _data;

        public GP(byte[] data)
        {
            _flag = new byte[] { 0x47, 0x50 };
            _version = new byte[] { 0x01, 0x00 };
            _data = data;
            _dataLen = data.Length;
        }

        public byte[] ToBytes()
        {
            byte[] buffer = new byte[8 + _dataLen];

            Buffer.BlockCopy(_flag, 0, buffer, 0, 2);
            Buffer.BlockCopy(_version, 0, buffer, 2, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(_dataLen), 0, buffer, 4, 4);
            if (_data != null)
                Buffer.BlockCopy(_data, 0, buffer, 8, _dataLen);

            return buffer;
        }

        public static bool TryParse(byte[] buffer, out GP gp)
        {
            gp = null;

            if (buffer.Length < 8)
                return false;

            if (buffer[0] != 0x47 || buffer[1] != 0x50)
                return false;

            if (buffer[2] != 0x01 || buffer[3] != 0x00)
                return false;

            int dataLen = BitConverter.ToInt32(buffer, 4);
            if (dataLen <= 0)
                return false;

            if (buffer.Length != dataLen + 8)
                return false;

            byte[] data = new byte[dataLen];
            Buffer.BlockCopy(buffer, 8, data, 0, dataLen);

            gp = new GP(data);

            return true;
        }
    }
}
