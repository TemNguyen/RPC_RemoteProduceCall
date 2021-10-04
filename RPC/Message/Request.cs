using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPC.Message
{
    [Serializable]
    public class Request
    {
        public string Class { get; set; }
        public string Method { get; set; }
        public Dictionary<string, object> Parameter { get; set; }
    }
}
