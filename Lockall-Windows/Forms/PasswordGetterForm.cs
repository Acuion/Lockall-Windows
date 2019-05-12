using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lockall_Windows.Forms
{
    public partial class PasswordGetterForm : Form
    {
        internal string PasswordResult;

        protected override bool ShowWithoutActivation => true;

        public PasswordGetterForm(string title)
        {
            InitializeComponent();

            passwordBox.KeyDown += PasswordBox_KeyDown;
            Closing += (sender, args) =>
            {
                if (DialogResult == DialogResult.None)
                    DialogResult = DialogResult.Cancel;
            };
            Text = "Pass for " + title;

            TopLevel = true;
        }

        private void Finish()
        {
            if (string.Empty == passwordBox.Text.Trim())
            {
                return;
            }
            DialogResult = DialogResult.OK;
            PasswordResult = passwordBox.Text;
        }

        private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Finish();
                e.Handled = true;
            }
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            var upper = new List<string>
            {
                "A", "E", "I", "O", "U", "Y", "B", "C", "D", "F", "G", "H",
                "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "V", "W", "X", "Y", "Z"
            };
            var lower = new List<string>
            {
                "a", "e", "i", "o", "u", "y", "b", "c", "d", "f", "g", "h",
                "j", "k", "l", "m", "n", "p", "q", "r", "s","t", "v", "w", "x", "y", "z"
            };
            var digits = new List<string>
            {
                "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"
            };
            var symbols = new List<string>
            {
                "!", "\"", "#", "$", "%", "&", "'", "(", ")", "*", "+", ",", "-", ".", "/", ":", ";",
                "<", "=", ">", "?", "@", "[", "\\", "]", "^", "_", "`", "{", "|", "}", "~"
            };

            var dict = new List<string>();
            if (checkBoxUppercase.Checked)
            {
                dict.AddRange(upper);
            }
            if (checkBoxLowercase.Checked)
            {
                dict.AddRange(lower);
            }
            if (checkBoxDigits.Checked)
            {
                dict.AddRange(digits);
            }
            if (checkBoxSymbols.Checked)
            {
                dict.AddRange(symbols);
            }

            using (var rng = RandomNumberGenerator.Create())
            {
                var buffer = new byte[4];
                rng.GetBytes(buffer);
                var rnd = new Random((buffer[0]) + (buffer[1] << 8) + (buffer[2] << 16) + (buffer[3] << 24));
                passwordBox.Clear();
                for (int i = 0; i < trackBarPasslen.Value; ++i)
                {
                    passwordBox.Text += dict[rnd.Next(dict.Count)];
                }
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void buttonVis_Click(object sender, EventArgs e)
        {
            passwordBox.PasswordChar = '\0' == passwordBox.PasswordChar ? '•' : '\0';
        }

        private void trackBarPasslen_Scroll(object sender, EventArgs e)
        {
            labelPasslen.Text = trackBarPasslen.Value.ToString();
        }
    }
}
