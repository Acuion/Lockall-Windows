using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lockall_Windows.Comm;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace Lockall_Windows.Forms
{
    public partial class QrDisplayerWindow : Form
    {
        public QrDisplayerWindow(string mode)
        {
            InitializeComponent();

            Text = mode;
        }

        private ClientListener NewClientListener()
        {
            if (SettingsHolder.ConnectionType == ConnectionType.Wifi)
            {
                return new TcpClientListener();
            }
            return new BluetoothClientListener();
        }

        public async Task<T> ShowQrForAJsonResult<T>(string prefix, string qrUserContentJson, bool attachFirstComponent = false)
        {
            using (var pcKey = CngKey.Create(CngAlgorithm.ECDiffieHellmanP521))
            {
                    using (var comm = NewClientListener())
                    {
                        // todo: сгенерировать половину ecdh, показать её + запрос, получить вторую половину и ответ, посчитать ключ, расшифровать, вернуть
                        var qrBody = QrBuilder.BuildQrBody(comm, qrUserContentJson, pcKey.Export(CngKeyBlobFormat.EccPublicBlob),
                            attachFirstComponent);

                        ImageQr.Image = QrBuilder.CreateQrFromBytes(prefix, qrBody.ToArray());
                        var result = await comm.ReadAndDecryptClientMessage();
                        Close();
                        return JsonConvert.DeserializeObject<T>(result);
                    }
                }
        }
    }
}
