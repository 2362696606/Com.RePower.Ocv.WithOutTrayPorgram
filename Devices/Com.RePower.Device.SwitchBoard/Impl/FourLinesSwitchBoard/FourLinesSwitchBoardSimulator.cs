using Com.RePower.DeviceBase.BaseDevice;

namespace Com.RePower.Device.SwitchBoard.Impl.FourLinesSwitchBoard
{
    public class FourLinesSwitchBoardSimulator : FourLinesSwitchBoardImpl
    {
        public FourLinesSwitchBoardSimulator()
        {
            DeviceBase = new SerialPortDeviceBaseSimulator();
        }
    }
}