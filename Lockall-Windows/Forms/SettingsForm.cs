using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lockall_Windows.Comm;

namespace Lockall_Windows.Forms
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            if (BluetoothClientListener.GetBtMacAddress() == null)
            {
                bluetoothRB.Enabled = false;
            }
            switch (SettingsHolder.ConnectionType)
            {
                case ConnectionType.Wifi:
                    wifiRB.Checked = true;
                    break;
                case ConnectionType.Bluetooth:
                    bluetoothRB.Checked = true;
                    break;
            }
        }

        private void wifiRB_CheckedChanged(object sender, EventArgs e)
        {
            if (wifiRB.Checked)
            {
                SettingsHolder.ConnectionType = ConnectionType.Wifi;
            }
        }

        private void bluetoothRB_CheckedChanged(object sender, EventArgs e)
        {
            if (bluetoothRB.Checked)
            {
                SettingsHolder.ConnectionType = ConnectionType.Bluetooth;
            }
        }
    }
}
