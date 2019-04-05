using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lockall_Windows.Comm
{
    public abstract class ClientListener : IDisposable
    {
        public abstract List<byte> ComputeHeader();
        public abstract Task<string> ReadAndDecryptClientMessage(CngKey privateKey);
        public abstract void Dispose();

        protected string DecryptMessage(CngKey pcPrivate, Stream message)
        {
            using (var inputStream = new BinaryReader(message))
            {
                var iv = inputStream.ReadBytes(16);
                var msgLen = inputStream.ReadInt32();

                using (var ecdh = new ECDiffieHellmanCng(pcPrivate))
                {
                    ecdh.HashAlgorithm = CngAlgorithm.Sha256;
                    ecdh.KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash;

                    using (var aesManaged = new RijndaelManaged
                    {
                        KeySize = 256,
                        BlockSize = 128,
                        Mode = CipherMode.CBC,
                        Padding = PaddingMode.PKCS7
                    })
                    {
                        using (var mobilePublic = CngKey.Import(todo, CngKeyBlobFormat.EccPublicBlob))
                        {
                            using (var encryptor = aesManaged.CreateDecryptor(ecdh.DeriveKeyMaterial(mobilePublic), iv))
                            {
                                using (var msEncrypt = new MemoryStream())
                                {
                                    using (var csEncrypt =
                                        new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                                    {
                                        using (var swEncrypt = new BinaryWriter(csEncrypt))
                                        {
                                            swEncrypt.Write(inputStream.ReadBytes(msgLen));
                                        }

                                        return Encoding.UTF8.GetString(msEncrypt.ToArray());
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}