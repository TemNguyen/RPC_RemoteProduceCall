using RPC.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RPC.Server
{
    class ServerProxy
    {
        readonly int _port;
        ServerSocket _server;

        public Func<Request, Reponse> Process { get; set; }
        public Action<string> Log { get; set; }
        public ServerProxy(int port)
        {
            _port = port;
            _server = new ServerSocket(port);
        }

        public void Run()
        {
            _server.Start();
            Log?.Invoke($"[{DateTime.Now}] Accepted connection from {_server.Client.Client.RemoteEndPoint}");

            ThreadPool.QueueUserWorkItem((o) =>
           {
               var request = _server.Receive() as Request;
               Log?.Invoke($"[{DateTime.Now}] Received Request: {request.GetString()}");

               var response = Process?.Invoke(request);
               _server.Send(response);
               _server.CloseConnection();
           });
        }
    }

    internal static class Extension
    {
        public static string GetString(this Request request)
        {
            var stringBuilder = new StringBuilder();
            foreach (var o in request.Parameter.Values)
                stringBuilder.Append($" {o} ");

            return $"{request.Class}.{request.Method}({stringBuilder.ToString()})";
        }
    }
}
