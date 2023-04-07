namespace Com.RePower.DeviceBase.BaseDevice
{
    public interface ISerialPortDeviceBase : ISerialPortDevice, ISendCmd
    {
        public int ReadDelay { get; set; }
    }
}