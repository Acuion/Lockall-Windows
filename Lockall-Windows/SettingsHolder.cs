namespace Lockall_Windows
{
    public enum ConnectionType
    {
        Wifi,
        Bluetooth
    }

    public static class SettingsHolder
    {
        public static ConnectionType ConnectionType = ConnectionType.Wifi;
    }
}