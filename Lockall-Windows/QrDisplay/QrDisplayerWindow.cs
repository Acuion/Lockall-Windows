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
using Lockall_Windows.QrDisplay;

namespace Lockall_Windows.Forms
{
    public partial class QrDisplayerWindow : Form, IQrDisplayer
    {
        public QrDisplayerWindow()
        {
            InitializeComponent();
        }

        public async Task ShowQr(string prefix, byte[] bytes)
        {
            Text = prefix;
            Show();
            ImageQr.Image = QrBuilder.CreateQrFromBytes(prefix, bytes);
        }

        public async Task HideQr()
        {
            Close();
        }
    }
}
