using CollaborationBus.Entity;
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

            _udpClient = new UdpClient(new IPEndPoint(IPAddress.Any, 9898));
            _udpClient.JoinMulticastGroup(_multiCast.Address);

            Thread thread = new Thread(ReciveMessage);
            thread.Start();
        }

        public void SendMessage(string message)
        {
            //TODO:使用GP协议打包消息
            byte[] messageBuffer = Encoding.UTF8.GetBytes(message);

            GP gp = new GP(messageBuffer);
            byte[] buffer = gp.ToBytes();

            _udpClient.Send(buffer, buffer.Length, _multiCast);
            Debug.WriteLine("send {0}", buffer);
        }

        public void ReciveMessage()
        {
            while (true)
            {
                IPEndPoint iep = new IPEndPoint(IPAddress.Any, 0);
                byte[] messageBuffer = _udpClient.Receive(ref iep);

                //TODO:判断收到的是否为GP协议的包
                GP gp;
                bool isGp = GP.TryParse(messageBuffer, out gp);

                //TODO:将消息从包中提取出来
                if (isGp) {
                    string recevieMessage = Encoding.UTF8.GetString(gp.data);
                    RecevieMessage?.Invoke(this, recevieMessage);
                }
            }
        }

        //发送“Hello”消息
    }
}
