using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Lockall_Windows.BrowserPlugin
{
    class WebsocketComm : WebSocketBehavior
    {
        protected override Task OnMessage(MessageEventArgs e)
        {
            MessageBox.Show("Rcvd: " + e.Text.ReadToEnd());
            return base.OnMessage(e);
        }
    }
}
