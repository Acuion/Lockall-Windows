using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Lockall_Windows
{
    class ClientListener
    {
        TcpListener _listener;

        public int ListensAtPort => ((IPEndPoint)_listener.LocalEndpoint).Port;

        public ClientListener()
        {
            _listener = new TcpListener(IPAddress.Any, 0);
            _listener.Start();
        }

        public async Task<string> ReadAndDecryptClientMessageThenCloseListenerAsync(byte[] secondComponent)
        {
            var client = await _listener.AcceptTcpClientAsync();
            _listener.Stop();
            var inputStream = new BinaryReader(client.GetStream());
            var msgLen = inputStream.ReadInt32();
            var iv = inputStream.ReadBytes(16);
            var msg = inputStream.ReadBytes(msgLen - 16);

            var key = EncryptionUtils.Produce256bitFromComponents(ComponentsManager.ComputeDeterminedFirstComponent(),
                secondComponent);
            return EncryptionUtils.DecryptDataWithAes256(msg, key, iv);
        }
    }
}
