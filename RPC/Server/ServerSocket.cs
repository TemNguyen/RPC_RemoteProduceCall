using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace RPC.Server
{
    public class ServerSocket
    {
        TcpListener _listener;
        TcpClient _client;
        BinaryFormatter _formatter = new BinaryFormatter();
        NetworkStream _stream;

        public TcpClient Client => _client;

        public ServerSocket(int port) => _listener = new TcpListener(IPAddress.Any, port);
        public void Start() => _listener.Start();
        public void Accept()
        {
            _client = _listener.AcceptTcpClient();
            _stream = _client.GetStream();
        }
        public void CloseConnection() => _client.Close();
        public void Send(object message) => _formatter.Serialize(_stream, message);
        public object Receive() => _formatter.Deserialize(_stream);
    }
}
