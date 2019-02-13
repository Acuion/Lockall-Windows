using System;
using System.Collections.Generic;
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
            _bluetoothGuid = new Guid();

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
            result.AddRange(GetBtMacAddress().ToByteArray());
            result.AddRange(_bluetoothGuid.ToByteArray());
            return result;
        }

        public override Task<string> ReadAndDecryptClientMessage(byte[] secondComponent)
        {
            return new Task<string>(() =>
            {
                var client = _listener.AcceptBluetoothClient();
                return DecryptClientMessage(client.GetStream(), secondComponent);
            });
        }
    }
}