using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace Lockall_Windows
{
    public static class BroadcastSender
    {
        private static readonly UdpClient Multicaster;
        private static readonly IPEndPoint MulticastEndpoint;

        static BroadcastSender()
        {
            var multicastAddress = IPAddress.Parse("224.0.24.42");
            Multicaster = new UdpClient(AddressFamily.InterNetwork);
            Multicaster.JoinMulticastGroup(multicastAddress, 8);
            MulticastEndpoint = new IPEndPoint(multicastAddress, 52594);
        }
        /*
         * При первоначальном паринге сервером выступает комп. Он показывает QR с ключом, именем машины и своим local ip адресом (выбор из нескольких?)
         * Телефон присоединяется к нему и передаёт свой mac, связка завершена
         * 
         * При получении пароля комп устанавливает TCP с телефоном (телефон - сервер). Дальше они общаются шифруя сообщения ключом. 
         */
        public static void SendEncryptedBroadcast(string content, byte[] aes256Key)
        {
            using (var multicastMessageStream = new MemoryStream())
            {
                // header + SHA-1 of src data + encrypted data
                using (var writer = new BinaryWriter(multicastMessageStream))
                {
                    writer.Write(Encoding.UTF8.GetBytes("LOCKALL"));
                    using (var hasher = new SHA1Managed())
                    {
                        writer.Write(hasher.ComputeHash(Encoding.UTF8.GetBytes(content)));
                    }
                    writer.Write(EncryptionUtils.EncryptDataWithAes256(content, aes256Key));
                    writer.Flush();

                    Multicaster.Send(multicastMessageStream.ToArray(), (int)multicastMessageStream.Length, MulticastEndpoint);
                }
            }
        }
    }
}