using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Lockall_Windows.Comm
{
    public abstract class ClientListener : IDisposable
    {
        public abstract List<byte> ComputeHeader();
        public abstract Task<Stream> GetStream();
        public abstract void Dispose();

        public static byte[] CompleteEcdhFromStream(CngKey pcPrivate, Stream message)
        {
            using (var inputStream = new BinaryReader(message, Encoding.UTF8, true))
            {
                var ecdhKeyLen = inputStream.ReadInt32();
                var ecdhKeyBytes = inputStream.ReadBytes(ecdhKeyLen);

                using (var ecdh = new ECDiffieHellmanCng(pcPrivate))
                {
                    ecdh.HashAlgorithm = CngAlgorithm.Sha256;
                    ecdh.KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash;

                    using (var mobilePublic = CngKey.Import(EncryptionUtils.PemToEccBlob(ecdhKeyBytes),
                        CngKeyBlobFormat.EccPublicBlob))
                    {
                        return ecdh.DeriveKeyMaterial(mobilePublic);
                    }
                }
            }
        }

        public static string DecryptMessage(byte[] aes256Key, Stream message)
        {
            using (var inputStream = new BinaryReader(message, Encoding.UTF8, true))
            {
                var iv = inputStream.ReadBytes(16); // todo: wait for a time
                var msgLen = inputStream.ReadInt32();

                return Encoding.UTF8.GetString(EncryptionUtils.DecryptDataWithAes256(inputStream.ReadBytes(msgLen), aes256Key, iv));
            }
        }

        public static void SendEncrypted(byte[] message, byte[] aes256Key, Stream to)
        {
            using (var outputStream = new BinaryWriter(to, Encoding.UTF8, true))
            {
                var iv = EncryptionUtils.Generate128BitIv();
                outputStream.Write(iv);
                
                var encrypted = EncryptionUtils.EncryptDataWithAes256(message, aes256Key, iv);
                outputStream.Write(encrypted.Length);
                outputStream.Write(encrypted);
            }
        }
    }
}