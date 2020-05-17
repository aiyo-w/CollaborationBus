using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace CollaborationBus
{
    /// <summary>
    /// 连接两台电脑的类
    /// </summary>
    public class Conector
    {
        private TcpClient _tcpClient;
        private NetworkStream _networkStream;
        private byte[] _dataBuffer;

        public Conector(string address, int port)
        {
            _dataBuffer = new byte[2048];
            _tcpClient = new TcpClient(new IPEndPoint(IPAddress.Parse(address), port));
            _networkStream = _tcpClient.GetStream();
        }

        // TODO:建立Soket连接
        public void SendMessage(string message)
        {
            // TODO:发送消息
            //_tcpClient.Client.Send(Encoding.UTF8.GetBytes(message));
            _networkStream.Write(Encoding.UTF8.GetBytes(message));
        }

        public byte[] ReciveMessage()
        {
            int dataLength = _networkStream.Read(_dataBuffer, 0, 2048);

            byte[] dataRead = new byte[dataLength];
            Buffer.BlockCopy(_dataBuffer, 0, dataRead, 0, dataLength);

            return dataRead;
        }


        // TODO:发送“Hello”消息
    }
}
