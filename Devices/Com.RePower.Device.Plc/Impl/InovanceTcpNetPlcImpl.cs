using HslCommunication.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Com.RePower.Device.Plc.Impl
{
    public class InovanceTcpNetPlcImpl : PlcNetAbstract
    {
        private DataFormat _dataFormat;

        [JsonConverter(typeof(StringEnumConverter), true)]
        public DataFormat DataFormat
        {
            get
            {
                return (NetWorkDeviceBase as HslCommunication.Profinet.Inovance.InovanceTcpNet)?.DataFormat ?? _dataFormat;
            }
            set
            {
                if (NetWorkDeviceBase is HslCommunication.Profinet.Inovance.InovanceTcpNet device) device.DataFormat = value;
                else _dataFormat = value;
            }
        }

        public InovanceTcpNetPlcImpl()
        {
            var device = new HslCommunication.Profinet.Inovance.InovanceTcpNet();

            device.Read<InovanceTcpNet>();

            device.IsStringReverse = true;
            NetWorkDeviceBase = device;
            OnDeviceCreated();
        }
    }
}