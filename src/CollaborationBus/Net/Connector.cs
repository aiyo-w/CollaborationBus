using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CollaborationBus
{
    /// <summary>
    /// 连接两台电脑的类
    /// </summary>
    public class Connector
    {
        private UdpClient _udpClient;
        private NetworkStream _networkStream;
        private byte[] _dataBuffer;
        private IPEndPoint _multiCast;

        private string _address;
        private int _port;

        public event EventHandler<string> RecevieMessage;

        public Connector(string address, int port)
        {
            _dataBuffer = new byte[2048];
            _address = address;
            _port = port;
            _multiCast = new IPEndPoint(IPAddress.Parse("239.0.0.1"), 9898);

            _udpClient = new UdpClient(new IPEndPoint(IPAddress.Any,9898));
            _udpClient.JoinMulticastGroup(_multiCast.Address);

            Thread thread = new Thread(ReciveMessage);
            thread.Start();
        }

        public void SendMessage(string message)
        {
            byte[] messageBuffer = Encoding.UTF8.GetBytes(message);
            _udpClient.Send(messageBuffer, messageBuffer.Length, _multiCast);
            Debug.WriteLine("send {0}", message);
        }

        public void ReciveMessage()
        {
            while (true)
            {
                IPEndPoint iep = new IPEndPoint(IPAddress.Any,0);
                byte[] messageBuffer = _udpClient.Receive(ref iep);

                string recevieMessage = Encoding.UTF8.GetString(messageBuffer);

                RecevieMessage?.Invoke(this,recevieMessage);
            }
        }

        //发送“Hello”消息
    }
}
