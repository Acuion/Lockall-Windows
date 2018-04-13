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

                var comm = new ClientListener();
                
                ImageQr.Dispatcher.Invoke(() =>
                {
                    ImageQr.Source = QrBuilder.CreateQrFromBytes("PAIRING",
                        PairingManager.MakeDataForPairing(comm.ListensAtPort,
                        out secondComponent));
                });

                var rcv = comm.ReadAndDecryptClientMessageThenCloseListenerAsync(secondComponent).Result;

                MessageBox.Show(rcv);
            });
        }
    }
}
