using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;

namespace Lockall_Windows.Comm
{
    public class BluetoothClientListener : ClientListener
    {
        private Guid _bluetoothGuid;
        private BluetoothListener _listener;

        public BluetoothClientListener()
        {
            _bluetoothGuid = Guid.NewGuid();

            _listener = new BluetoothListener(_bluetoothGuid);
            _listener.Start();
        }

        public static BluetoothAddress GetBtMacAddress()
        {
            BluetoothRadio myRadio = BluetoothRadio.PrimaryRadio;
            return myRadio?.LocalAddress;
        }

        public override void Dispose()
        {
            _listener.Stop();
        }

        public override List<byte> ComputeHeader()
        {
            List<byte> result = new List<byte>();
            result.Add(2); // bluetooth
            var mac = Encoding.UTF8.GetBytes(GetBtMacAddress().ToString("C"));
            result.AddRange(mac);
            var uuid = Encoding.UTF8.GetBytes(_bluetoothGuid.ToString());
            result.AddRange(uuid);
            return result;
        }

        public override Task<Stream> GetStream()
        {
            var task = new Task<Stream>(() =>
            {
                var client = _listener.AcceptBluetoothClient();
                return client.GetStream();
            });
            task.Start();
            return task;
        }
    }
}