using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Lockall_Windows
{
    public static class EncryptionUtils
    {
        public static byte[] Generate128BitIv()
        {
            var iv = new byte[16];
            new RNGCryptoServiceProvider().GetBytes(iv);
            return iv;
        }

        public static byte[] EccPublicToPem(CngKey pcKey)
        {
            var cngBlob = pcKey.Export(CngKeyBlobFormat.EccPublicBlob);

            byte[] prefix = Secp384R1PublicPrefix;
            byte[] derPublicKey = new byte[120];
            Buffer.BlockCopy(prefix, 0, derPublicKey, 0, prefix.Length);

            Debug.Assert(cngBlob.Length == 104);

            Buffer.BlockCopy(cngBlob, 8, derPublicKey, prefix.Length, cngBlob.Length - 8);

            return derPublicKey;
        }

        public static byte[] PemToEccBlob(byte[] pemBytes)
        {
            byte[] cngBlob = new byte[104];
            Buffer.BlockCopy(Msblob384Header, 0, cngBlob, 0, Msblob384Header.Length);
            Buffer.BlockCopy(pemBytes, Secp384R1PublicPrefix.Length, cngBlob, Msblob384Header.Length, pemBytes.Length - Secp384R1PublicPrefix.Length);
            return cngBlob;
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

        private static readonly byte[] Secp384R1PublicPrefix =
        {
            // SEQUENCE (SubjectPublicKeyInfo, 0x76 bytes)
            0x30, 0x76,
            // SEQUENCE (AlgorithmIdentifier, 0x10 bytes)
            0x30, 0x10,
            // OBJECT IDENTIFIER (id-ecPublicKey)
            0x06, 0x07, 0x2A, 0x86, 0x48, 0xCE, 0x3D, 0x02, 0x01,
            // OBJECT IDENTIFIER (secp384r1)
            0x06, 0x05, 0x2B, 0x81, 0x04, 0x00, 0x22,
            // BIT STRING, 0x61 content bytes, 0 unused bits.
            0x03, 0x62, 0x00,
            // Uncompressed EC point
            0x04,
        };

        private static readonly byte[] Msblob384Header =
        {
            0x45, 0x43, 0x4b, 0x33, 0x30, 0x0, 0x0, 0x0
        };
    }
}