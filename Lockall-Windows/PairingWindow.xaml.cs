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

namespace Lockall_Windows
{
    /// <summary>
    /// Логика взаимодействия для PairingWindow.xaml
    /// </summary>
    public partial class PairingWindow : Window
    {
        public PairingWindow()
        {
            InitializeComponent();

            Task.Run(() =>
            {
                byte[] secondComponent = null;

                var listener = new TcpListener(IPAddress.Any, 0);
                listener.Start();
                ImageQr.Dispatcher.Invoke(() =>
                {
                    ImageQr.Source = QrBuilder.CreateQrFromBytes("PAIRING",
                        PairingManager.MakeDataForPairing(((IPEndPoint)listener.LocalEndpoint).Port,
                        out secondComponent));
                });
                var client = listener.AcceptTcpClient();
                listener.Stop();
                var inputStream = new BinaryReader(client.GetStream());
                var msgLen = inputStream.ReadInt32();
                var iv = inputStream.ReadBytes(16);
                var msg = inputStream.ReadBytes(msgLen - 16); // todo: to a method

                var key = EncryptionUtils.Produce256bitFromComponents(ComponentsManager.ComputeDeterminedFirstComponent(),
                    secondComponent);
                var decrypted = EncryptionUtils.DecryptDataWithAes256(msg, key, iv);
                MessageBox.Show(decrypted);
            });
        }
    }
}
