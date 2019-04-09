using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    class ChromeComm : WebSocketBehavior
    {
        public static ChromeComm ChromeConnection;

        private ClientListener NewClientListener() // todo: refactor the duplication
        {
            if (SettingsHolder.ConnectionType == ConnectionType.Wifi)
            {
                return new TcpClientListener();
            }
            return new BluetoothClientListener();
        }

        public async Task<T> ShowQrInBrowserForAJsonResult<T>(string prefix, Task<string> qrUserContentJson)
        {
            using (var pcKey = CngKey.Create(CngAlgorithm.ECDiffieHellmanP384))
            {
                using (var comm = NewClientListener())
                {
                    var qrBody = QrBuilder.BuildQrBody(comm, EncryptionUtils.EccPublicToPem(pcKey));

                    await Send($"SHOW:{prefix}:{Convert.ToBase64String(qrBody)}");
                    using (var commStream = await comm.GetStream())
                    {
                        await Send("CLOSE");

                        var aes256Key = ClientListener.CompleteEcdhFromStream(pcKey, commStream);

                        qrUserContentJson.Start();
                        var content = await qrUserContentJson;
                        if (content == null)
                        {
                            throw new Exception("No content"); // todo
                        }
                        ClientListener.SendEncrypted(Encoding.UTF8.GetBytes(content), aes256Key, commStream);

                        return JsonConvert.DeserializeObject<T>(ClientListener.DecryptMessage(aes256Key, commStream));
                    }
                }
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
    }
}
