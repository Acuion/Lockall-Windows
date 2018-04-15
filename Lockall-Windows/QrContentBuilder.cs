using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Lockall_Windows
{
   static class QrContentBuilder
    {
        public static byte[] MakeDataForPairing()
        {
            var result = new List<byte>();
            var username = Encoding.UTF8.GetBytes(Environment.MachineName + "/" + Environment.UserName);
            result.AddRange(BitConverter.GetBytes(username.Length));
            result.AddRange(username);
            return result.ToArray();
        }
    }
}
