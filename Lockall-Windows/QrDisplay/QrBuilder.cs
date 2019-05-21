using System;
using System.Collections.Generic;
using System.Drawing;
using Lockall_Windows.Comm;
using QRCoder;

namespace Lockall_Windows.QrDisplay
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

        public static byte[] BuildQrBody(ClientListener responseTo,
            byte[] pcPublicEcdh)
        {
            var qrBody = new List<byte>();
            qrBody.AddRange(BitConverter.GetBytes(pcPublicEcdh.Length));
            qrBody.AddRange(pcPublicEcdh);

            var userData = new List<byte>();
            userData.AddRange(responseTo.ComputeHeader());

            qrBody.AddRange(BitConverter.GetBytes(userData.Count));
            qrBody.AddRange(userData);

            return qrBody.ToArray();
        }
    }
}
