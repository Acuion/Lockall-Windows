﻿using System.IO;
using System.Security.Cryptography;

namespace Lockall_Windows
{
    public static class EncryptionUtils
    {
        public static byte[] EncryptDataWithAes256(string content, byte[] aes256Key)
        {
            using (var aesEncryptor = new RijndaelManaged
            {
                KeySize = 256,
                BlockSize = 128,
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            })
            {
                aesEncryptor.GenerateIV();

                var encryptor = aesEncryptor.CreateEncryptor(aes256Key, aesEncryptor.IV);

                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEcrypt = new StreamWriter(csEncrypt))
                        {
                            swEcrypt.Write(content);
                        }
                        return msEncrypt.ToArray();
                    }
                }
            }
        }
    }
}