using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.DeviceBase.BaseDevice
{
    public interface ISimulatorRecovery
    {
        public Func<byte[], byte[]>? RecoveryMethod { get; set; }
    }
}
