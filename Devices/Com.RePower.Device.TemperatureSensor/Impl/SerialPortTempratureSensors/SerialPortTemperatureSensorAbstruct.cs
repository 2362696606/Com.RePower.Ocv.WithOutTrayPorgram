using Com.RePower.DeviceBase.TemperatureSensor;
using Com.RePower.WpfBase;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Device.TemperatureSensor.Impl.SerialPortTempratureSensors
{
    public abstract class SerialPortTemperatureSensorAbstruct : TemperatureSensorBase, ITemperatureSensorSerialPort
    {
        public abstract string PortName { get; set; }
        public abstract int BaudRate { get; set; }
        public abstract int ReadDelay { get; set; }

        public abstract OperateResult Connect(string portName, int baudRate);

        public async Task<OperateResult> ConnectAsync(string portName, int baudRate)
        {
            return await Task.Run(() => Connect(portName, baudRate));
        }
        public override OperateResult<double[]> ReadTemp()
        {
            byte[] cmdBase = new byte[] { 0x01, 0x03, 0x00, 0x08, 0x00, 0x08, 0xC5, 0xCE };
            OperateResult<byte[]> result = SendCmd(cmdBase);
            int i = 3;
            while (i > 0)
            {
                if (result.IsFailed)
                {
                    result = SendCmd(cmdBase);
                }
                if (result.IsSuccess)
                {
                    byte[] recoveryBytes = result.Content!;
                    if (recoveryBytes.Length < 11)
                        return OperateResult.CreateFailedResult<double[]>("读取到的串口回包过短");
                    else
                    {
                        //var data = recoveryBytes.Skip(20).Take(160).ToArray();
                        //List<byte[]> tempData = new List<byte[]>();
                        //for (int index = 0; index < 40; index++)
                        //{
                        //    var temp = data.Skip(index * 4 + 2).Take(2).ToArray();
                        //    tempData.Add(temp);
                        //}
                        //List<double> tempDoubleDatas = new List<double>();
                        //foreach (var item in tempData)
                        //{
                        //    short tempShortVal = BitConverter.ToInt16(item);
                        //    double tempDoubleVal = Math.Abs(Math.Round(tempShortVal * 0.01, 2));
                        //    tempDoubleDatas.Add(tempDoubleVal);
                        //}
                        //return OperateResult.CreateSuccessResult(tempDoubleDatas.ToArray());
                        List<double> resultList = new List<double>();
                        for (int t = 3; t < 10; t += 2)
                        {
                            byte[] currentBytes = recoveryBytes.Skip(i).Take(2).Reverse().ToArray();
                            var realValue = BitConverter.ToInt16(currentBytes);
                            decimal decValue = (decimal)realValue * (decimal)0.1;
                            resultList.Add((double)decValue);
                        }
                        return OperateResult.CreateSuccessResult(resultList.ToArray());
                    }
                }
            }
            return OperateResult.CreateFailedResult<double[]>("读取温度失败");
        }

    }
}
