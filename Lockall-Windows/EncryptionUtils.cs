using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Lockall_Windows
{
    public static class EncryptionUtils
    {
        public static byte[] Produce256bitFromComponents(byte[] comp1, byte[] comp2)
        {
            var concat = new List<byte>();
            concat.AddRange(comp1);
            concat.AddRange(comp2);
            return new SHA256Managed().ComputeHash(concat.ToArray());
        }

        public static string DecryptDataWithAes256(byte[] content, byte[] aes256Key, byte[] iv) // todo: check
        {
            using (var aesDecryptor = new RijndaelManaged
            {
                KeySize = 256,
                BlockSize = 128,
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            })
            {
                var decryptor = aesDecryptor.CreateDecryptor(aes256Key, iv);

                using (var msDecrypt = new MemoryStream())
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Write))
                    {
                        using (var swEcrypt = new StreamWriter(csDecrypt))
                        {
                            swEcrypt.Write(content);
                        }
                        return Encoding.UTF8.GetString(msDecrypt.ToArray());
                    }
                }
            }
        }
    }
}