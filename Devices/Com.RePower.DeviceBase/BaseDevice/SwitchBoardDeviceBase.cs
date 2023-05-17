using Com.RePower.DeviceBase.Helper;
using Com.RePower.WpfBase;

namespace Com.RePower.DeviceBase.BaseDevice;

public class SwitchBoardDeviceBase:SerialPortDeviceBase
{
    public override OperateResult<byte[]> SendCmd(byte[] cmd, int timeout = 10000, bool isNeedRecovery = true)
    {
        try
        {
            if (isNeedRecovery)
            {
                return SerialPort.SendAndRecoveryManual(cmd, timeout, ReadDelay);
            }
            else
            {
                SerialPort.Write(cmd, 0, cmd.Length);
                return OperateResult.CreateSuccessResult<byte[]>(null);
            }
        }
        catch (Exception err)
        {
            return OperateResult.CreateFailedResult<byte[]>(err.Message, err.HResult);
        }
    }
}