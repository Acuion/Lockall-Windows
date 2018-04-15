using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Lockall_Windows
{
    public static class EncryptionUtils
    {
        public static byte[] Produce256BitFromComponents(byte[] comp1, byte[] comp2)
        {
            var concat = new List<byte>();
            concat.AddRange(comp1);
            concat.AddRange(comp2);
            return new SHA256Managed().ComputeHash(concat.ToArray());
        }

        public static byte[] Generate128BitIv()
        {
            var iv = new byte[16];
            new RNGCryptoServiceProvider().GetBytes(iv);
            return iv;
        }

        public static byte[] EncryptDataWithAes256(byte[] content, byte[] aes256Key, byte[] iv)
        {
            using (var aesEncryptor = new RijndaelManaged
            {
                KeySize = 256,
                BlockSize = 128,
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            })
            {
                using (var encryptor = aesEncryptor.CreateEncryptor(aes256Key, iv))
                {
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (var swEncrypt = new BinaryWriter(csEncrypt))
                            {
                                swEncrypt.Write(content);
                            }
                            return msEncrypt.ToArray();
                        }
                    }
                }
            }
        }

        public static byte[] DecryptDataWithAes256(byte[] content, byte[] aes256Key, byte[] iv)
        {
            using (var aesDecryptor = new RijndaelManaged
            {
                KeySize = 256,
                BlockSize = 128,
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            })
            {
                using (var decryptor = aesDecryptor.CreateDecryptor(aes256Key, iv))
                {
                    using (var msDecrypt = new MemoryStream())
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Write))
                        {
                            using (var swEcrypt = new BinaryWriter(csDecrypt))
                            {
                                swEcrypt.Write(content);
                            }
                            return msDecrypt.ToArray();
                        }
                    }
                }
            }
        }
    }
}