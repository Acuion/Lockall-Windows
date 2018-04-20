using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
