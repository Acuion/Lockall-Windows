﻿using System;
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

        public async Task<T> ShowQrForAJsonResult<T>(string prefix, string qrUserContentJson)
        {
            using (var pcKey = CngKey.Create(CngAlgorithm.ECDiffieHellmanP384))
            {
                using (var comm = NewClientListener())
                {
                    // todo: сгенерировать половину ecdh, показать её + запрос, получить вторую половину и ответ, посчитать ключ, расшифровать, вернуть
                    var qrBody = QrBuilder.BuildQrBody(comm, qrUserContentJson, EncryptionUtils.EccPublicToPem(pcKey));

                    ImageQr.Image = QrBuilder.CreateQrFromBytes(prefix, qrBody.ToArray());
                    var result = await comm.ReadAndDecryptClientMessage(pcKey);
                    Close();
                    return JsonConvert.DeserializeObject<T>(result);
                }
            }
        }
    }
}
