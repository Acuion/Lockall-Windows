using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Lockall_Windows.Comm;
using Newtonsoft.Json;

namespace Lockall_Windows.QrDisplay
{
    public class MobileConnector
    {
        private readonly IQrDisplayer _qrDisplayer;

        public MobileConnector(IQrDisplayer qrDisplayer)
        {
            _qrDisplayer = qrDisplayer;
        }

        private ClientListener NewClientListener()
        {
            if (SettingsHolder.ConnectionType == ConnectionType.Wifi)
            {
                return new TcpClientListener();
            }
            return new BluetoothClientListener();
        }

        public async Task<T> ShowQrForAJsonResult<T>(string prefix, Task<string> qrUserContentJson) where T : class
        {
            using (var pcKey = CngKey.Create(CngAlgorithm.ECDiffieHellmanP384))
            {
                using (var comm = NewClientListener())
                {
                    var qrBody = QrBuilder.BuildQrBody(comm, EncryptionUtils.EccPublicToPem(pcKey));

                    await _qrDisplayer.ShowQr(prefix, qrBody.ToArray());
                    using (var commStream = await comm.GetStream())
                    {
                        await _qrDisplayer.HideQr();

                        var aes256Key = ClientListener.CompleteEcdhFromStream(pcKey, commStream);

                        qrUserContentJson.Start();
                        var content = await qrUserContentJson;
                        if (content == null)
                        {
                            return null;
                        }
                        ClientListener.SendEncrypted(Encoding.UTF8.GetBytes(content), aes256Key, commStream);

                        return JsonConvert.DeserializeObject<T>(ClientListener.DecryptMessage(aes256Key, commStream));
                    }
                }
            }
        }
    }
}