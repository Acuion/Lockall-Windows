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
                {
                    using (QRCode qrCode = new QRCode(qrCodeData))
                    {
                        using (MemoryStream memory = new MemoryStream())
                        {
                            return qrCode.GetGraphic(20);
                        }
                    }
                }
            }
        }

        public static byte[] BuildQrBody(ClientListener responseTo, string qrUserContentJson,
            byte[] secondComponent, bool attachFirstComponent = false)
        {
            var firstComponent = ComponentsManager.ComputeDeterminedFirstComponent();

            var qrBody = new List<byte>();
            if (attachFirstComponent)
            {
                qrBody.Add(1); // unsafe, with fc
                qrBody.AddRange(BitConverter.GetBytes(firstComponent.Length));
                qrBody.AddRange(firstComponent);
            }
            else
            {
                qrBody.Add(0); // safe
            }
            qrBody.AddRange(BitConverter.GetBytes(secondComponent.Length));
            qrBody.AddRange(secondComponent);

            var iv = EncryptionUtils.Generate128BitIv();
            var key = EncryptionUtils.Produce256BitFromComponents(firstComponent, secondComponent);

            qrBody.AddRange(iv);

            var userData = new List<byte>();
            userData.AddRange(responseTo.ComputeHeader());
            userData.AddRange(Encoding.UTF8.GetBytes(qrUserContentJson));

            var encryptedUserData = EncryptionUtils.EncryptDataWithAes256(userData.ToArray(), key, iv);

            qrBody.AddRange(BitConverter.GetBytes(encryptedUserData.Length));
            qrBody.AddRange(encryptedUserData);

            return qrBody.ToArray();
        }
    }
}
