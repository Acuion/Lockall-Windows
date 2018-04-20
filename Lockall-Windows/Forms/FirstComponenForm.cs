using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lockall_Windows.Forms;
using Lockall_Windows.Messages;
using Lockall_Windows.Messages.Pairing;
using Lockall_Windows.Messages.Password;
using Lockall_Windows.WinUtils;
using Newtonsoft.Json;

namespace Lockall_Windows
{
    public partial class Form1 : Form
    {
        private readonly TextBox[] _firstComponents;
        private KeyboardHook _passCreate, _passLoad;

        public Form1()
        {
            InitializeComponent();

            _passCreate = new KeyboardHook();
            _passCreate.RegisterHotKey(WinUtils.ModifierKeys.Alt | WinUtils.ModifierKeys.Shift, Keys.Insert);
            _passCreate.KeyPressed += _passCreate_KeyPressed;

            _passLoad = new KeyboardHook();
            _passLoad.RegisterHotKey(WinUtils.ModifierKeys.Alt, Keys.Insert);
            _passLoad.KeyPressed += _passLoad_KeyPressed;

            pairingButton.Click += PairingButton_Click;

            _firstComponents = new[] { secw1Text, secw2Text, secw3Text, secw4Text, secw5Text, secw6Text };
            string[] components;
            if (File.Exists(ComponentsManager.FirstComponentFilename))
                components = File.ReadAllText(ComponentsManager.FirstComponentFilename).Split();
            else
                components = new string[_firstComponents.Length];
            for (int i = 0; i < _firstComponents.Length; ++i)
            {
                _firstComponents[i].Text = components[i];
                _firstComponents[i].KeyDown += FirstCompElementKeyDown;
                _firstComponents[i].LostFocus += FirstComponentLostFocus;
            }
        }

        private void PairingButton_Click(object sender, EventArgs e)
        {
            var name = Environment.MachineName + "/" + Environment.UserName;

            var pair = new QrDisplayerWindow("PAIR");
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

        private void FirstComponentLostFocus(object sender, EventArgs e)
        {
            string current = "";
            for (int i = 0; i < _firstComponents.Length; ++i)
                current += _firstComponents[i].Text + " ";
            File.WriteAllText(ComponentsManager.FirstComponentFilename, current);
        }

        private void _passLoad_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            var load = new QrDisplayerWindow("PULL");
            load.Show();
            load.ShowQrForAJsonResult<MessageWithPassword>("PULL",
                JsonConvert.SerializeObject(
                    new MessageWithResourceid(TitleGetter.GetActiveWindowTitle())), true).ContinueWith(result =>
                    {
                        SendKeys.SendWait(result.Result.password);
                    });
        }

        private void _passCreate_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            var winHeader = TitleGetter.GetActiveWindowTitle();

            var passAsker = new PasswordGetterForm(winHeader);
            var res = passAsker.ShowDialog();
            if (res == DialogResult.OK)
            {
                var create = new QrDisplayerWindow("STORE");
                create.Show();
                create.ShowQrForAJsonResult<MessageStatus>("STORE",
                    JsonConvert.SerializeObject(
                        new MessageWithPassword(winHeader, passAsker.PasswordResult)), true).ContinueWith(result =>
                        {
                        });
            }
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
            if (e.KeyCode == Keys.Space)
            {
                _firstComponents[ix].Focus();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }
    }
}
