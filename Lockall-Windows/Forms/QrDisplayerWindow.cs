using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Lockall_Windows.Forms
{
    public partial class QrDisplayerWindow : Form
    {
        public QrDisplayerWindow(string mode)
        {
            InitializeComponent();

            Text = mode;
        }

        protected override bool ShowWithoutActivation => true;

        public async Task<T> ShowQrForAJsonResult<T>(string prefix, string qrUserContentJson, bool attachFirstComponent = false)
        {
            using (var comm = new ClientListener())
            {
                var firstComponent = ComponentsManager.ComputeDeterminedFirstComponent();
                var secondComponent = ComponentsManager.ComputeRandomizedSecondComponent();

                var qrBody = new List<byte>();
                if (attachFirstComponent)
                {
                    qrBody.Add(1);
                    qrBody.AddRange(BitConverter.GetBytes(firstComponent.Length));
                    qrBody.AddRange(firstComponent);
                }
                else
                {
                    qrBody.Add(0);
                }
                qrBody.AddRange(BitConverter.GetBytes(secondComponent.Length));
                qrBody.AddRange(secondComponent);

                var iv = EncryptionUtils.Generate128BitIv();
                var key = EncryptionUtils.Produce256BitFromComponents(firstComponent, secondComponent);

                qrBody.AddRange(iv);

                var userData = new List<byte>();
                userData.AddRange(comm.ListensAtIp);
                userData.AddRange(BitConverter.GetBytes(comm.ListensAtPort));
                userData.AddRange(Encoding.UTF8.GetBytes(qrUserContentJson));

                var encryptedUserData = EncryptionUtils.EncryptDataWithAes256(userData.ToArray(), key, iv);

                qrBody.AddRange(BitConverter.GetBytes(encryptedUserData.Length));
                qrBody.AddRange(encryptedUserData);

                ImageQr.Invoke(new Action(() =>
                {
                    ImageQr.Image = QrBuilder.CreateQrFromBytes(prefix, qrBody.ToArray());
                }));
                var result = await comm.ReadAndDecryptClientMessage(secondComponent);
                Close();
                return JsonConvert.DeserializeObject<T>(result);
            }
        }
    }
}
