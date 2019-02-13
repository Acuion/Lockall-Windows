using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Lockall_Windows.Comm
{
    public abstract class ClientListener : IDisposable
    {
        public abstract List<byte> ComputeHeader();
        public abstract Task<string> ReadAndDecryptClientMessage(byte[] secondComponent);
        public abstract void Dispose();

        protected string DecryptClientMessage(Stream stream, byte[] secondComponent)
        {
            var inputStream = new BinaryReader(stream);

            var iv = inputStream.ReadBytes(16);
            var msgLen = inputStream.ReadInt32();
            var msg = inputStream.ReadBytes(msgLen);

            var key = EncryptionUtils.Produce256BitFromComponents(ComponentsManager.ComputeDeterminedFirstComponent(),
                secondComponent);
            return Encoding.UTF8.GetString(EncryptionUtils.DecryptDataWithAes256(msg, key, iv));
        }
    }
}