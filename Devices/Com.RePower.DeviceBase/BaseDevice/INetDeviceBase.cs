namespace Com.RePower.DeviceBase.BaseDevice
{
    public interface INetDeviceBase : INetDevice, ISendCmd
    {
        public int ReadDelay { get; set; }
    }
}