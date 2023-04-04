namespace Com.RePower.Device.Plc.Impl
{
    public class SiemensS1500Impl : PlcNetAbstract
    {
        public SiemensS1500Impl()
        {
            this.NetWorkDeviceBase = new HslCommunication.Profinet.Siemens.SiemensS7Net(HslCommunication.Profinet.Siemens.SiemensPLCS.S1500);
            base.OnDeviceCreated();
        }
    }
}