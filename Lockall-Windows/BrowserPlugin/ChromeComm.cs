using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lockall_Windows.Comm;
using Lockall_Windows.Forms;
using Lockall_Windows.Messages;
using Lockall_Windows.Messages.Password;
using Newtonsoft.Json;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Lockall_Windows.BrowserPlugin
{
    /*class ChromeComm : WebSocketBehavior
    {
        public static ChromeComm ChromeConnection;

        public async Task<T> ShowQrInBrowserForAJsonResult<T>(string prefix, string qrUserContentJson)
        {
            using (var comm = new TcpClientListener())
            {
                var secondComponent = ComponentsManager.ComputeRandomizedSecondComponent();

                var qrBody = QrBuilder.BuildQrBody(comm, qrUserContentJson, secondComponent);
                string qrBodyAsBase64 = Convert.ToBase64String(qrBody);
                await Send($"SHOW:{prefix}:{qrBodyAsBase64}");
                var result = await comm.ReadClientMessage(secondComponent); // todo: trycatches everywhere
                await Send("CLOSE");
                System.Threading.Thread.Sleep(1000);
                return JsonConvert.DeserializeObject<T>(result);
            }
        }

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
    }*/
}
