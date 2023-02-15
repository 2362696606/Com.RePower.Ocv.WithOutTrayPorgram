using Com.RePower.DeviceBase.DMM;
using Com.RePower.DeviceBase.Ohm;
using Com.RePower.DeviceBase.Plc;
using Com.RePower.DeviceBase.SwitchBoard;
using Com.RePower.DeviceBase.TemperatureSensor;
using log4net.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.CZD01.BaseProject.Controllers
{
    public class DevicesController
    {
        public DevicesController(IPlc? plc = null,ISwitchBoard? switchBoard = null,IOhm? ohm = null,IDMM? dmm = null,ITemperatureSensor? temperatureSensor = null)
        {
            _plc = plc;
            _switchBoard = switchBoard;
            _ohm = ohm;
            _dmm = dmm;
            _temperatureSensor = temperatureSensor;
        }

        private IPlc? _plc;

        public IPlc? Plc
        {
            get { return _plc; }
        }
        private ISwitchBoard? _switchBoard;

        public ISwitchBoard? SwitchBoard
        {
            get { return _switchBoard; }
        }
        private IOhm? _ohm;

        public IOhm? Ohm
        {
            get { return _ohm; }
        }
        private IDMM? _dmm;

        public IDMM? Dmm
        {
            get { return _dmm; }
        }
        private ITemperatureSensor? _temperatureSensor;

        public ITemperatureSensor? TemperatureSensor
        {
            get { return _temperatureSensor; }
        }

    }
}
