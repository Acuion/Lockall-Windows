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
using System.Security.Cryptography.X509Certificates;

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

        public async Task<T> ShowQrForAJsonResult<T>(string prefix, Task<string> qrUserContentJson) where T: class
        {
            using (var pcKey = CngKey.Create(CngAlgorithm.ECDiffieHellmanP384))
            {
                using (var comm = NewClientListener())
                {
                    var qrBody = QrBuilder.BuildQrBody(comm, EncryptionUtils.EccPublicToPem(pcKey));

                    ImageQr.Image = QrBuilder.CreateQrFromBytes(prefix, qrBody.ToArray());
                    using (var commStream = await comm.GetStream())
                    {
                        Close();

                        var aes256Key = ClientListener.CompleteEcdhFromStream(pcKey, commStream); // todo: refactor

                        qrUserContentJson.Start();
                        var content = await qrUserContentJson;
                        if (content == null)
                        {
                            throw new Exception("No content");
                        }
                        ClientListener.SendEncrypted(Encoding.UTF8.GetBytes(content), aes256Key, commStream);

                        return JsonConvert.DeserializeObject<T>(ClientListener.DecryptMessage(aes256Key, commStream));
                    }
                }
            }
        }
    }
}
