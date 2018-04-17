using System;
using System.Collections.Generic;
using System.IO;
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
using Lockall_Windows.Messages.Pairing;
using Newtonsoft.Json;

namespace Lockall_Windows.Forms
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TextBox[] _firstComponents;
        
        public MainWindow()
        {
            InitializeComponent();

            System.Windows.Forms.NotifyIcon ni = new System.Windows.Forms.NotifyIcon();
            ni.Icon = new System.Drawing.Icon("app.ico");
            ni.Visible = true;
            ni.DoubleClick +=
                (object sender, EventArgs args) =>
                {
                    Show();
                    WindowState = WindowState.Normal;
                };

            pairingButton.Click += PairingButtonOnClick;
            _firstComponents = new[] {secw1Text, secw2Text, secw3Text, secw4Text, secw5Text, secw6Text};
            string[] components;
            if (File.Exists(ComponentsManager.FirstComponentFilename))
                components = File.ReadAllText(ComponentsManager.FirstComponentFilename).Split();
            else
                components = new string[_firstComponents.Length];
            for (int i = 0; i < _firstComponents.Length; ++i)
            {
                _firstComponents[i].Text = components[i];
                _firstComponents[i].PreviewKeyDown += FirstCompElementKeyDown;
                _firstComponents[i].LostFocus += FirstCompElementLostFocus;
            }
        }

        private void FirstCompElementLostFocus(object sender, RoutedEventArgs e)
        {
            string current = "";
            for (int i = 0; i < _firstComponents.Length; ++i)
                current += _firstComponents[i].Text + " ";
            File.WriteAllText(ComponentsManager.FirstComponentFilename, current);
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
                this.Hide();

            base.OnStateChanged(e);
        }

        private void FirstCompElementKeyDown(object sender, KeyEventArgs e)
        {
            int ix = 0;
            for (int i = 0; i < _firstComponents.Length; ++i)
                if (sender == _firstComponents[i])
                {
                    ix = i;
                    break;
                }
            ix = (ix + 1) % _firstComponents.Length;
            if (e.Key == Key.Space)
            {
                FocusManager.SetFocusedElement(mainGrid, _firstComponents[ix]);
                e.Handled = true;
                return;
            }
        }

        private void PairingButtonOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            var name = Environment.MachineName + "/" + Environment.UserName;

            var pair = new QrDisplayerWindow();
            pair.Show();
            pair.ShowQrForAJsonResult<MessageWithName>("PAIRING",
                JsonConvert.SerializeObject(
                    new MessageWithName(name)), true).ContinueWith(result =>
            {
                if (name == result.Result.name)
                {
                    MessageBox.Show("Pairing complete");
                }
            });
        }
    }
}
