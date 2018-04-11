using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using QRCoder;

namespace Lockall_Windows
{
    internal static class PairingManager
    {
        public static byte[] MakeDataForPairing(int listeningAtPort)
        {
            byte[] localIp;
            try
            {
                using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
                {
                    socket.Connect("8.8.8.8", 65530); // todo: local
                    IPEndPoint endPoint = (IPEndPoint)socket.LocalEndPoint;
                    localIp = endPoint.Address.GetAddressBytes();
                }
            }
            catch
            {
                //todo: failed
                return null;
            }

            var firstComponent = ComponentsManager.ComputeDeterminedFirstComponent();

            var result = new List<byte>();
            result.AddRange(BitConverter.GetBytes(firstComponent.Length));
            result.AddRange(firstComponent);
            result.AddRange(localIp);
            result.AddRange(BitConverter.GetBytes(listeningAtPort));
            result.AddRange(Encoding.UTF8.GetBytes(Environment.UserName));

            return result.ToArray();
        }
    }
}