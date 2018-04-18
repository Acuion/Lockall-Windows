using System;
using System.Drawing;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using Lockall_Windows.Messages;
using Lockall_Windows.Messages.Pairing;
using Lockall_Windows.Messages.Password;
using Lockall_Windows.WinUtils;
using Newtonsoft.Json;
using Application = System.Windows.Application;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MessageBox = System.Windows.MessageBox;
using ModifierKeys = System.Windows.Input.ModifierKeys;
using TextBox = System.Windows.Controls.TextBox;

namespace Lockall_Windows.Forms
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly TextBox[] _firstComponents;
        private KeyboardHook _passCreate, _passLoad;

        public MainWindow()
        {
            InitializeComponent();

            Closed += (sender, args) => { Application.Current.Shutdown(0); };

            _passCreate = new KeyboardHook();
            _passCreate.RegisterHotKey(WinUtils.ModifierKeys.Alt | WinUtils.ModifierKeys.Shift, Keys.Insert);
            _passCreate.KeyPressed += _passCreate_KeyPressed;

            _passLoad = new KeyboardHook();
            _passLoad.RegisterHotKey(WinUtils.ModifierKeys.Alt, Keys.Insert);
            _passLoad.KeyPressed += _passLoad_KeyPressed;

            NotifyIcon ni = new NotifyIcon();
            ni.Icon = new Icon("app.ico");
            ni.Visible = true;
            ni.DoubleClick +=
                (sender, args) =>
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

        private void _passLoad_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            var load = new QrDisplayerWindow();
            load.ShowQrForAJsonResult<MessageWithPassword>("LOAD",
                JsonConvert.SerializeObject(
                    new MessageWithResourceid(TitleGetter.GetActiveWindowTitle())), true).ContinueWith(result =>
            {
                    SendKeys.SendWait(result.Result.password);
            });
            load.Show();
        }

        private void _passCreate_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            var create = new QrDisplayerWindow();
            create.ShowQrForAJsonResult<MessageStatus>("STORE",
                JsonConvert.SerializeObject(
                    new MessageWithPassword(TitleGetter.GetActiveWindowTitle(), "qwerty")), true).ContinueWith(result =>
            {
                 MessageBox.Show(result.Result.status);
            });
            create.Show();
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
                Hide();

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
            }
        }

        private void PairingButtonOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            var name = Environment.MachineName + "/" + Environment.UserName;

            var pair = new QrDisplayerWindow();
            pair.ShowQrForAJsonResult<MessageWithName>("PAIRING",
                JsonConvert.SerializeObject(
                    new MessageWithName(name)), true).ContinueWith(result =>
            {
                if (name == result.Result.name)
                {
                    MessageBox.Show("Pairing complete");
                }
            });
            pair.Show();
        }
    }
}
