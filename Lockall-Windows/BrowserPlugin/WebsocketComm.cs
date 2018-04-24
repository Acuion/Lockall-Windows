using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lockall_Windows.Forms;
using Lockall_Windows.Messages;
using Lockall_Windows.Messages.Password;
using Newtonsoft.Json;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Lockall_Windows.BrowserPlugin
{
    class WebsocketComm : WebSocketBehavior
    {
        protected override Task OnMessage(MessageEventArgs e)
        {
            string request = e.Text.ReadToEnd();

            var secondComponent = ComponentsManager.ComputeRandomizedSecondComponent();

            switch (request)
            {
                case "PULL":
                {
                    using (var comm = new ClientListener())
                    {
                        var data = QrBuilder.BuildQrBody(comm, JsonConvert.SerializeObject(
                            new MessageWithResourceid("")), secondComponent); // port & ip, site address will be added later
                        Send(data).Wait();
                        var result = JsonConvert.DeserializeObject<MessageWithPassword>(
                            comm.ReadAndDecryptClientMessage(secondComponent).Result); // todo: locking ws communication
                        SendKeys.SendWait(result.password);
                        return Send("OK");
                    }
                }
                case "STORE":
                {
                    var passAsker = new PasswordGetterForm("the site");
                    var res = passAsker.ShowDialog();
                    if (res == DialogResult.OK)
                    {
                        using (var comm = new ClientListener())
                        {
                            var data = QrBuilder.BuildQrBody(comm, JsonConvert.SerializeObject(
                                new MessageWithPassword("", passAsker.PasswordResult)), secondComponent); // port & ip & password, site address will be added later
                                Send(data).Wait();
                            var result = JsonConvert.DeserializeObject<MessageStatus>(
                                comm.ReadAndDecryptClientMessage(secondComponent).Result); // todo: locking ws communication
                            return Send("OK");
                        }
                    }
                }
                break;
            }
            return null;
        }
    }
}
