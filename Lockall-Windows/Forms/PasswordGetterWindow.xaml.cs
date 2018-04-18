using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Lockall_Windows.Forms
{
    /// <summary>
    /// Interaction logic for PasswordGetterWindow.xaml
    /// </summary>
    public partial class PasswordGetterWindow : Window
    {
        public PasswordGetterWindow(string title)
        {
            InitializeComponent();

            passwordBox.PreviewKeyDown += PasswordBox_PreviewKeyDown;
            Closing += (sender, args) =>
            {
                if (DialogResult == null)
                    DialogResult = false;
            };
            Title = "Pass for " + title;
        }

        private void PasswordBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                DialogResult = true;
                e.Handled = true;
            }
        }
    }
}
