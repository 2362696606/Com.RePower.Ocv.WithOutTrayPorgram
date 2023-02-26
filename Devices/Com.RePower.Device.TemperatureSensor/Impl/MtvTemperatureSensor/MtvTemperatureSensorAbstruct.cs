using Com.RePower.DeviceBase.BaseDevice;
using Com.RePower.DeviceBase.TemperatureSensor;
using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Device.TemperatureSensor.Impl.MtvTemperatureSensor
{
    public abstract class MtvTemperatureSensorAbstruct : TemperatureSensorBase, ITemperatureSensorNet
    {
        public short BoxIndex { get; set; } = 1;

        public abstract int ReadDelay { get; set; }

        public override OperateResult<double[]> ReadTemp()
        {
            byte[] cmdBase = new byte[] { 0x43, 0x54, 0x4f, 0x44, 0x20, 0x00, 0x01, 0x00, 0x02, 0x00, 0x04, 0x00, 0x01, 0x00, 0xb3, 0x68 };
            byte[] boxIndexByteArray = BitConverter.GetBytes(BoxIndex);
            cmdBase[6] = boxIndexByteArray[0];
            cmdBase[7] = boxIndexByteArray[1];
            byte[] crc = Crc(cmdBase.Take(cmdBase.Length - 2).ToArray());
            cmdBase[15] = crc[0];
            cmdBase[14] = crc[1];
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
                    if (recoveryBytes.Length == 182)
                    {
                        var data = recoveryBytes.Skip(20).Take(160).ToArray();
                        List<byte[]> tempData = new List<byte[]>();
                        for (int index = 0; index < 40; index++)
                        {
                            var temp = data.Skip(index * 4 + 2).Take(2).ToArray();
                            tempData.Add(temp);
                        }
                        List<double> tempDoubleDatas = new List<double>();
                        foreach (var item in tempData)
                        {
                            short tempShortVal = BitConverter.ToInt16(item);
                            double tempDoubleVal = Math.Abs(Math.Round(tempShortVal * 0.1, 2));
                            tempDoubleDatas.Add(tempDoubleVal);
                        }
                        return OperateResult.CreateSuccessResult(tempDoubleDatas.ToArray());
                    }
                }
            }
            return OperateResult.CreateFailedResult<double[]>("读取温度失败");
        }

        /// **********************************************************************
        /// Name: CRC-16/MODBUS    x16+x15+x2+1
        /// Poly: 0x8005
        /// Init: 0xFFFF
        /// Refin: true
        /// Refout: true
        /// Xorout: 0x0000    
        ///*************************************************************************
        public static byte[] Crc(byte[] buffer, int start = 0, int len = 0)
        {
            if (buffer == null || buffer.Length == 0) return new byte[0];
            if (start < 0) return new byte[0];
            if (len == 0) len = buffer.Length - start;
            int length = start + len;
            if (length > buffer.Length) return new byte[0];
            ushort crc = 0xFFFF;// Initial value
            for (int i = start; i < length; i++)
            {
                crc ^= buffer[i];
                for (int j = 0; j < 8; j++)
                {
                    if ((crc & 1) > 0)
                        crc = (ushort)((crc >> 1) ^ 0xA001);// 0xA001 = reverse 0x8005 
                    else
                        crc = (ushort)(crc >> 1);
                }
            }
            byte[] ret = BitConverter.GetBytes(crc);
            Array.Reverse(ret);
            return ret;
        }


        public abstract string IpAddress { get; set; }
        public abstract int Port { get; set; }

        public abstract OperateResult Connect(string ipAddress, int port);
        public virtual async Task<OperateResult> ConnectAsync(string ipAddress, int port)
        {
            return await Task.Run(() => Connect(ipAddress, port));
        }
    }
}
