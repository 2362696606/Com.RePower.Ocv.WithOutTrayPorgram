using Autofac;
using Com.RePower.Device.DMM.Impl.Keysight_34461A;
using Com.RePower.Device.Ohm.Impl.Hioki_BT3562;
using Com.RePower.Device.Plc.Impl;
using Com.RePower.Device.SwitchBoard.Impl.FourLinesSwitchBoard;
using Com.RePower.DeviceBase.DMM;
using Com.RePower.DeviceBase.Ohm;
using Com.RePower.DeviceBase.Plc;
using Com.RePower.DeviceBase.SwitchBoard;
using Com.RePower.Ocv.Project.Byd.CB09.Settings;
using HslCommunication.Core;

namespace Com.RePower.Ocv.Project.Byd.CB09.Modules;

public class DevicesModule:Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var authenticitySetting = new AuthenticitySetting();
        #region Plc注册
        var plcSetting = new NetPlcSetting();
        IPlc plcInstance;
        if (authenticitySetting.IsRealPlc)
        {
            var trulyPlc = new InovanceTcpNetPlcImpl();
            trulyPlc.DeviceName = plcSetting.DeviceName;
            trulyPlc.IpAddress = plcSetting.IpAddress;
            trulyPlc.Port = plcSetting.Port;
            trulyPlc.DataFormat = Enum.Parse<DataFormat>(plcSetting.DataFormat);
            plcInstance = trulyPlc;
        }
        else
        {
            var simPlc = new PlcNetSimulator();
            simPlc.DeviceName = plcSetting.DeviceName;
            simPlc.IpAddress = plcSetting.IpAddress;
            simPlc.Port = plcSetting.Port;
            plcInstance = simPlc;
        }

        builder.RegisterInstance(plcInstance).As<IPlc>();
        #endregion

        #region 注册万用表
        var dmmSetting = new NetDmmSetting();
        IDmm dmmInstance;
        if (authenticitySetting.IsRealDmm)
        {
            var trulyDmm = new Keysight34461AImpl();
            trulyDmm.DeviceName = dmmSetting.DeviceName;
            trulyDmm.IpAddress = dmmSetting.IpAddress;
            trulyDmm.Port = dmmSetting.Port;
            trulyDmm.ReadDelay = dmmSetting.ReadDelay;
            dmmInstance = trulyDmm;
        }
        else
        {
            var simDmm = new Keysight34461ASimulator();
            simDmm.DeviceName = dmmSetting.DeviceName;
            simDmm.IpAddress = dmmSetting.IpAddress;
            simDmm.Port = dmmSetting.Port;
            simDmm.ReadDelay = dmmSetting.ReadDelay;
            dmmInstance = simDmm;
        }

        builder.RegisterInstance(dmmInstance).As<IDmm>();
        #endregion

        #region 注册内阻仪
        var ohmSetting = new SerialPortOhmSetting();
        IOhm ohmInstance;
        if (authenticitySetting.IsRealOhm)
        {
            var trulyOhm = new HiokiBt3562Impl();
            trulyOhm.DeviceName = ohmSetting.DeviceName;
            trulyOhm.PortName = ohmSetting.PortName;
            trulyOhm.BaudRate = ohmSetting.BaudRate;
            trulyOhm.ReadDelay = ohmSetting.ReadDelay;
            ohmInstance = trulyOhm;
        }
        else
        {
            var simOhm = new HiokiBt3562Simulator();
            simOhm.DeviceName = ohmSetting.DeviceName;
            simOhm.PortName = ohmSetting.PortName;
            simOhm.BaudRate = ohmSetting.BaudRate;
            simOhm.ReadDelay = ohmSetting.ReadDelay;
            ohmInstance = simOhm;
        }

        builder.RegisterInstance(ohmInstance).As<IOhm>();
        #endregion

        #region 注册切换版
        var switchBoardSetting = new SerialPortSwitchBoardSetting();
        ISwitchBoard switchBoardInstance;
        if (authenticitySetting.IsRealSwitchBoard)
        {
            var trulySw = new FourLinesSwitchBoardImpl();
            trulySw.DeviceName = switchBoardSetting.DeviceName;
            trulySw.PortName = switchBoardSetting.PortName;
            trulySw.BaudRate = switchBoardSetting.BaudRate;
            trulySw.ReadDelay = switchBoardSetting.ReadDelay;
            switchBoardInstance = trulySw;
        }
        else
        {
            var simSw = new FourLinesSwitchBoardSimulator();
            simSw.DeviceName = switchBoardSetting.DeviceName;
            simSw.PortName = switchBoardSetting.PortName;
            simSw.BaudRate = switchBoardSetting.BaudRate;
            simSw.ReadDelay = switchBoardSetting.ReadDelay;
            switchBoardInstance = simSw;
        }

        builder.RegisterInstance(switchBoardInstance).As<ISwitchBoard>(); 
        #endregion
    }
}