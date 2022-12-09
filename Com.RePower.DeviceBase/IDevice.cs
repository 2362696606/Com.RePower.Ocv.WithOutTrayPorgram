using Com.RePower.WpfBase;

namespace Com.RePower.DeviceBase
{
    public interface IDevice
    {
        public bool IsConnect { get; }
        public string DeviceName { get; set; }
        OperateResult Connect();
    }
}