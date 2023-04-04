using Autofac.Features.AttributeFilters;
using Com.RePower.DeviceBase.DMM;
using Com.RePower.DeviceBase.Ohm;
using Com.RePower.DeviceBase.Plc;
using Com.RePower.DeviceBase.SwitchBoard;
using Com.RePower.DeviceBase.TemperatureSensor;
using Com.RePower.Ocv.Model.DataBaseContext;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Byd.CB15.Controllers
{
    public class DevicesController
    {
        private IDmm? _dmm;
        private IOhm? _ohm;
        private ISwitchBoard? _switchBoard;
        private ITemperatureSensor? _ptemperatureSensor;
        private ITemperatureSensor? _ntemperatureSensor;
        public DevicesController(IPlc localPlc
            , IDmm? dMm = null
            , IOhm? ohm = null
            , ISwitchBoard? switchBoard = null
            , [KeyFilter("PTempSensor")]ITemperatureSensor? ptemperatureSensor = null
            , [KeyFilter("NTempSensor")]ITemperatureSensor? ntemperatureSensor = null)
        {
            Plc = localPlc;
            _dmm = dMm;
            _ohm = ohm;
            _switchBoard = switchBoard;
            _ptemperatureSensor = ptemperatureSensor;
            _ntemperatureSensor = ntemperatureSensor;
        }

        public IPlc Plc { get; }

        public IDmm? Dmm
        {
            get
            {
                //if (_dmm == null)
                //{
                //    LogHelper.UiLog.Error("万用表为null");
                //}
                return _dmm;
            }
        }

        public IOhm? Ohm
        {
            get
            {
                //if (_ohm == null)
                //{
                //    LogHelper.UiLog.Error("万用表为null");
                //}
                return _ohm;
            }
        }

        public ISwitchBoard? SwitchBoard
        {
            get
            {
                //if(_switchBoard == null)
                //{
                //    LogHelper.UiLog.Error("万用表为null");
                //}
                return _switchBoard;
            }
        }

        public ITemperatureSensor? PTemperatureSensor
        {
            get { return _ptemperatureSensor; }
        }
        public ITemperatureSensor? NTemperatureSensor
        {
            get { return _ntemperatureSensor; }
        }

    }
}
