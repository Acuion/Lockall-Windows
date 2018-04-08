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
        // gen key, get local ip, get machine name
        // show qr
        // listen to connections

        public static void PreparePairingData(out byte[] aes256Key, out string localIp, out string machineName)
        {
            using (var keyGeneratorIsntance = new RijndaelManaged())
            {
                keyGeneratorIsntance.GenerateKey();
                aes256Key = keyGeneratorIsntance.Key;
            }

            try
            {
                using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
                {
                    socket.Connect("8.8.8.8", 65530); // todo: local
                    IPEndPoint endPoint = (IPEndPoint)socket.LocalEndPoint;
                    localIp = endPoint.Address.ToString();
                }
            }
            catch
            {
                localIp = null;
            }

            machineName = System.Environment.UserName;
        }

        public static BitmapImage CreateAuthQr(byte[] aes256Key, string localIp, string machineName)
        {
            List<byte> toBase64 = new List<byte>();
            toBase64.AddRange(aes256Key);
            toBase64.AddRange(Encoding.UTF8.GetBytes(localIp + "~" + machineName));

            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                using (QRCodeData qrCodeData = qrGenerator.CreateQrCode("LOCKALL:" + Convert.ToBase64String(toBase64.ToArray()), QRCodeGenerator.ECCLevel.Q))
                {
                    using (QRCode qrCode = new QRCode(qrCodeData))
                    {
                        using (MemoryStream memory = new MemoryStream())
                        {
                            qrCode.GetGraphic(20).Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                            memory.Position = 0;
                            BitmapImage bitmapimage = new BitmapImage();
                            bitmapimage.BeginInit();
                            bitmapimage.StreamSource = memory;
                            bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                            bitmapimage.EndInit();

                            return bitmapimage;
                        }
                    }
                }
            }
        }

        // todo: move from the pairing manager
        public static void ScanTheNetwork(string localIp)
        {
            // TODO
            var localAddr = IPAddress.Parse(localIp);
            var mask = GetSubnetMask(localAddr).GetAddressBytes();

            var localBytes = localAddr.GetAddressBytes();

            int incrbleMask = 0, subnetMask = 0;

            for (int i = 0; i < 4; ++i)
            {
                subnetMask |= (byte)(mask[i] & localBytes[i]) << ((3 - i) * 8);
                incrbleMask |= (byte)(~mask[i]) << ((3 - i) * 8);
            }

            var tc = new TcpClient();

            int ipInc = 0;
            int ipTo = 0;
            while ((ipTo & incrbleMask) == ipTo)
                ipTo++;
            ipTo--;
            ConcurrentQueue<IPAddress> failed = new ConcurrentQueue<IPAddress>();
            Parallel.For(ipInc, ipTo, (ip, loopState) =>
            {
                int ipToScanAsInt = ip | subnetMask;

                var ipToScan = new IPAddress(new[]
                {
                    (byte) ((ipToScanAsInt & 0xFF000000) >> 24), (byte) ((ipToScanAsInt & 0xFF0000) >> 16),
                    (byte) ((ipToScanAsInt & 0xFF00) >> 8), (byte) (ipToScanAsInt & 0xFF)
                });

                var client = new TcpClient();
                if (client.ConnectAsync(ipToScan, 42424).Wait(1000))
                {
                    loopState.Break();
                }
                else
                {
                    failed.Enqueue(ipToScan);
                }
            });
        }

        private static IPAddress GetSubnetMask(IPAddress address)
        {
            foreach (NetworkInterface adapter in NetworkInterface.GetAllNetworkInterfaces())
            {
                foreach (UnicastIPAddressInformation unicastIPAddressInformation in adapter.GetIPProperties().UnicastAddresses)
                {
                    if (unicastIPAddressInformation.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        if (address.Equals(unicastIPAddressInformation.Address))
                        {
                            return unicastIPAddressInformation.IPv4Mask;
                        }
                    }
                }
            }
            throw new ArgumentException($"Can't find subnetmask for IP address '{address}'"); // todo: catch
        }

    }
}