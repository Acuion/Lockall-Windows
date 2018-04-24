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
                var secondComponent = ComponentsManager.ComputeRandomizedSecondComponent();

                var qrBody = QrBuilder.BuildQrBody(comm, qrUserContentJson, secondComponent, attachFirstComponent);

                ImageQr.Image = QrBuilder.CreateQrFromBytes(prefix, qrBody.ToArray());
                var result = await comm.ReadAndDecryptClientMessage(secondComponent);
                Close();
                return JsonConvert.DeserializeObject<T>(result);
            }
        }
    }
}
