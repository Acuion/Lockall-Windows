﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Lockall_Windows
{
    class ClientListener : IDisposable
    {
        TcpListener _listener;

        public int ListensAtPort => ((IPEndPoint)_listener.LocalEndpoint).Port;
        public byte[] ListensAtIp { get; }

        public ClientListener()
        {
            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530); // throws
                IPEndPoint endPoint = (IPEndPoint)socket.LocalEndPoint;
                ListensAtIp = endPoint.Address.GetAddressBytes();
            }

            _listener = new TcpListener(IPAddress.Any, 0);
            _listener.Start();
        }

        public async Task<byte[]> ReadAndDecryptClientMessage(byte[] secondComponent)
        {
            var client = await _listener.AcceptTcpClientAsync();
            var inputStream = new BinaryReader(client.GetStream());
            var msgLen = inputStream.ReadInt32();
            var iv = inputStream.ReadBytes(16);
            var msg = inputStream.ReadBytes(msgLen - 16);

            var key = EncryptionUtils.Produce256BitFromComponents(ComponentsManager.ComputeDeterminedFirstComponent(),
                secondComponent);
            return EncryptionUtils.DecryptDataWithAes256(msg, key, iv);
        }

        public void Dispose()
        {
            _listener.Stop();
        }
    }
}