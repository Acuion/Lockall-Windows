using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lockall_Windows.Forms
{
    public partial class PasswordGetterForm : Form
    {
        internal string PasswordResult;

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
        }

        private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DialogResult = DialogResult.OK;
                PasswordResult = passwordBox.Text;
                e.Handled = true;
            }
        }
    }
}
