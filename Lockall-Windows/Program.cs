using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lockall_Windows.Forms;
using Lockall_Windows.Messages;
using Lockall_Windows.Messages.Password;
using Lockall_Windows.Properties;
using Lockall_Windows.QrDisplay;
using Lockall_Windows.WinUtils;
using Newtonsoft.Json;
using WebSocketSharp.Server;

namespace Lockall_Windows
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Initialize();
            Application.Run();
        }

        private static KeyboardHook _passCreate;
        private static KeyboardHook _passLoad;
        private static KeyboardHook _otpLoad;

        private static void Initialize()
        {
            try
            {
                var wsServer = new WebSocketServer(IPAddress.Loopback, 42587);
                wsServer.AddWebSocketService<ChromeQrDisplayer>("/Lockall");
                wsServer.Start();
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot bind to 42587", "Browser plugin cannot be used", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            _passCreate = new KeyboardHook();
            _passCreate.RegisterHotKey(WinUtils.ModifierKeys.Alt | WinUtils.ModifierKeys.Shift, Keys.N);
            _passCreate.KeyPressed += _passCreate_KeyPressed;

            _passLoad = new KeyboardHook();
            _passLoad.RegisterHotKey(WinUtils.ModifierKeys.Alt | WinUtils.ModifierKeys.Shift, Keys.Insert);
            _passLoad.KeyPressed += _passLoad_KeyPressed;

            _otpLoad = new KeyboardHook();
            _otpLoad.RegisterHotKey(WinUtils.ModifierKeys.Alt | WinUtils.ModifierKeys.Shift, Keys.O);
            _otpLoad.KeyPressed += _otpLoad_KeyPressed;

            var trayIcon = new System.Windows.Forms.NotifyIcon {Icon = Properties.Resources.app, Visible = true};
            var contextMenu = new ContextMenu();
            contextMenu.MenuItems.AddRange(new[] { new MenuItem(), new MenuItem() });
            contextMenu.MenuItems[1].Text = "Exit";
            contextMenu.MenuItems[1].Click += (o, s) => { Application.ExitThread(); };
            contextMenu.MenuItems[0].Text = "Settings";
            contextMenu.MenuItems[0].Click += (o, s) => { new SettingsForm().Show(); };
            trayIcon.ContextMenu = contextMenu;
        }

        private static MobileConnector CreateConnector()
        {
            var winHeader = TitleGetter.GetActiveWindowTitle();

            if (winHeader.EndsWith("Google Chrome") && ChromeQrDisplayer.ChromeConnection != null)
            {
                return new MobileConnector(ChromeQrDisplayer.ChromeConnection);
            }
            return new MobileConnector(new QrDisplayerWindow());
        }

        private static void _otpLoad_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            CreateConnector().ShowQrForAJsonResult<MessageWithPassword>("OTP", new Task<string>(() => "{}")).ContinueWith(result =>
            {
                if (result.Result != null)
                {
                    SendKeys.SendWait(result.Result.password);
                }
            });
        }

        private static void _passLoad_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            var winHeader = TitleGetter.GetActiveWindowTitle();

            CreateConnector().ShowQrForAJsonResult<MessageWithPassword>("PULL",
                new Task<string>(() => JsonConvert.SerializeObject(
                    new MessageWithResourceid(winHeader)))).ContinueWith(result =>
                    {
                        if (result.Result != null)
                        {
                            SendKeys.SendWait(result.Result.password);
                        }
                    });
        }

        private static void _passCreate_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            var winHeader = TitleGetter.GetActiveWindowTitle();

            var passAskerTask = new Task<string>(() =>
            {
                var passAsker = new PasswordGetterForm(winHeader);
                var res = passAsker.ShowDialog();
                if (res != DialogResult.OK)
                {
                    return null;
                }

                return JsonConvert.SerializeObject(
                    new MessageWithPassword(winHeader, passAsker.PasswordResult));
            });

            CreateConnector().ShowQrForAJsonResult<MessageStatus>("STORE", passAskerTask).ContinueWith(result => { });
        }
    }
}
