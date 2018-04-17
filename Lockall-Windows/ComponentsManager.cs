using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lockall_Windows
{
    static class ComponentsManager
    {
        public const string FirstComponentFilename = "firstComponent.txt";

        public static byte[] ComputeDeterminedFirstComponent()
        {
            return SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(File.ReadAllText(FirstComponentFilename)));
        }

        public static byte[] ComputeRandomizedSecondComponent()
        {
            var result = new byte[32];
            new RNGCryptoServiceProvider().GetBytes(result);
            return result;
        }
    }
}
