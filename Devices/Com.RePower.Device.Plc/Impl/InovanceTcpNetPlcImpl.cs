using HslCommunication.Core;
using HslCommunication.LogNet;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Device.Plc.Impl
{
    public class InovanceTcpNetPlcImpl:PlcNetAbstract
    {
        private DataFormat _dataFormat;

        [JsonConverter(typeof(StringEnumConverter),true)]
        public DataFormat DataFormat
        {
            get
            {
                return (netWorkDeviceBase as HslCommunication.Profinet.Inovance.InovanceTcpNet)?.DataFormat?? _dataFormat;
            }
            set
            {
                if (netWorkDeviceBase is HslCommunication.Profinet.Inovance.InovanceTcpNet device) device.DataFormat = value;
                else _dataFormat = value;
            }
        }
        public InovanceTcpNetPlcImpl()
        {
            var device = new HslCommunication.Profinet.Inovance.InovanceTcpNet();

            device.Read<InovanceTcpNet>();

            device.IsStringReverse = true;
            netWorkDeviceBase = device;
            OnDeviceCreated();
        }
    }
}
