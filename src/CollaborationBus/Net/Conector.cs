using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CollaborationBus
{
    /// <summary>
    /// 连接两台电脑的类
    /// </summary>
    public class Conector
    {
        private TcpClient _tcpClient;
        private TcpListener _tcpListener;
        private NetworkStream _networkStream;
        private byte[] _dataBuffer;

        private string _address;
        private int _port;

        public Conector(string address, int port)
        {
            _dataBuffer = new byte[2048];
            _address = address;
            _port = port;
            //_tcpClient = new TcpClient(address, port);
            _tcpListener = new TcpListener(IPAddress.Parse("0.0.0.0"), port);


            _tcpListener.Start();
            _tcpClient = _tcpListener.AcceptTcpClient();
            _networkStream = _tcpClient.GetStream();

            //Task.Run(() =>
            //{
            //});
        }

        public void Connect()
        {
            try
            {
                var tcpClient = new TcpClient();
                tcpClient.Connect(_address, _port);
                _tcpClient = tcpClient;
                _networkStream = _tcpClient.GetStream();
            }
            catch (Exception ex)
            {

            }
        }


        // TODO:建立Soket连接
        public void SendMessage(string message)
        {
            // TODO:发送消息
            //_tcpClient.Client.Send(Encoding.UTF8.GetBytes(message));
            _networkStream.Write(Encoding.UTF8.GetBytes(message));
            Debug.WriteLine("send {0}", message);
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
