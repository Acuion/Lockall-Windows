using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Lockall_Windows.Comm;
using Newtonsoft.Json;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Lockall_Windows.QrDisplay
{
    class ChromeQrDisplayer : WebSocketBehavior, IQrDisplayer
    {
        public static ChromeQrDisplayer ChromeConnection;

        protected override Task OnClose(CloseEventArgs e)
        {
            ChromeConnection = null;
            return base.OnClose(e);
        }

        protected override Task OnOpen()
        {
            ChromeConnection = this;
            return base.OnOpen();
        }

        public async Task ShowQr(string prefix, byte[] bytes)
        {
            await Send($"SHOW:{prefix}:{Convert.ToBase64String(bytes)}");
        }

        public async Task HideQr()
        {
            await Send("CLOSE");
        }
    }
}
