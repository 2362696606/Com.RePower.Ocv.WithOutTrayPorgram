namespace Com.RePower.Device.Plc.Impl
{
    public class MelsecImpl : PlcNetAbstract
    {
        public MelsecImpl()
        {
            this.NetWorkDeviceBase = new HslCommunication.Profinet.Melsec.MelsecMcNet();
            base.OnDeviceCreated();
        }
    }
}