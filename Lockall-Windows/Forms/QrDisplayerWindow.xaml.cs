using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace Lockall_Windows.Forms
{
    /// <summary>
    /// Логика взаимодействия для PairingWindow.xaml
    /// </summary>
    public partial class QrDisplayerWindow : Window
    {
        public QrDisplayerWindow()
        {
            InitializeComponent();
        }

        public async Task<byte[]> ShowQrForAResult(string prefix, byte[] qrUserContent, bool attachFirstComponent = false)
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
                userData.AddRange(qrUserContent);

                var encryptedUserData = EncryptionUtils.EncryptDataWithAes256(userData.ToArray(), key, iv);

                qrBody.AddRange(BitConverter.GetBytes(encryptedUserData.Length));
                qrBody.AddRange(encryptedUserData);
                
                ImageQr.Dispatcher.Invoke(() =>
                {
                    ImageQr.Source = QrBuilder.CreateQrFromBytes(prefix, qrBody.ToArray());
                });

                return await comm.ReadAndDecryptClientMessage(secondComponent);
            }
        }
    }
}
