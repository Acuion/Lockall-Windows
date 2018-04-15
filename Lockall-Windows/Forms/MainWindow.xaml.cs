using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lockall_Windows.Forms
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var pair = new QrDisplayerWindow();
            pair.Show();
            pair.ShowQrForAResult("PAIRING", QrContentBuilder.MakeDataForPairing(), true).ContinueWith((result) =>
            {
                MessageBox.Show(Encoding.UTF8.GetString(result.Result));
            });
            
        }
    }
}
