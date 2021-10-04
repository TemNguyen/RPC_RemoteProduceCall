using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPC.Message
{
    [Serializable]
    public class Reponse
    {
        public bool Success { get; set; }
        public string Phrase { get; set; }
        public object Result { get; set; }
    }
}
