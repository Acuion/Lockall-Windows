using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lockall_Windows.Comm;

namespace Lockall_Windows
{
    static class QrBuilder
    {
        public static Bitmap CreateQrFromBytes(string prefix, byte[] source)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                var data = "LOCKALL:" + prefix + ":" + Convert.ToBase64String(source);
                using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q))
                using (QRCode qrCode = new QRCode(qrCodeData))
                {
                    return qrCode.GetGraphic(20);
                }
            }
        }

        public static byte[] BuildQrBody(ClientListener responseTo, string qrUserContentJson,
            byte[] pcPublicEcdh, bool attachFirstComponent = false)
        {
            var qrBody = new List<byte>();
            qrBody.AddRange(BitConverter.GetBytes(pcPublicEcdh.Length));
            qrBody.AddRange(pcPublicEcdh);

            var userData = new List<byte>();
            userData.AddRange(responseTo.ComputeHeader());
            userData.AddRange(Encoding.UTF8.GetBytes(qrUserContentJson));

            qrBody.AddRange(BitConverter.GetBytes(userData.Count));
            qrBody.AddRange(userData);

            return qrBody.ToArray();
        }
    }
}
