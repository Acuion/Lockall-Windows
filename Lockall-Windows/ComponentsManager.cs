using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lockall_Windows
{
    static class ComponentsManager
    {
        public static byte[] ComputeDeterminedFirstComponent()
        {
            return Encoding.UTF8.GetBytes("CakeIsALie"); // todo: rtv from the user
        }

        public static byte[] ComputeRandomizedSecondComponent()
        {
            var result = new byte[32];
            new RNGCryptoServiceProvider().GetBytes(result);
            return result;
        }
    }
}
