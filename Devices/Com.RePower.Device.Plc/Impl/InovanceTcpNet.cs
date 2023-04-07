using HslCommunication;
using HslCommunication.Core;
using HslCommunication.ModBus;
using HslCommunication.Profinet.Inovance;

namespace Com.RePower.Device.Plc.Impl
{
    public class InovanceTcpNet : HslCommunication.Profinet.Inovance.InovanceTcpNet
    {
        //public InovanceTcpNet()
        //{
        //    this.netWorkDeviceBase = new HslCommunication.Profinet.Inovance.InovanceTcpNet();
        //    this.netWorkDeviceBase.LogNet = new LogNetDateTime(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs/PlcLogs"), GenerateMode.ByEveryDay, 30);
        //    this.netWorkDeviceBase.SetPersistentConnection();
        //}
        public override OperateResult<string> TranslateToModbusAddress(string address, byte modbusCode)
        {
            return MyInovanceHelper.PraseInovanceAddress(Series, address, modbusCode);
        }
    }

    //
    // 摘要:
    //     汇川PLC的辅助类，提供一些地址解析的方法
    //     Auxiliary class of Yaskawa robot, providing some methods of address resolution
    public class MyInovanceHelper
    {
        private static int CalculateStartAddress(string address)
        {
            if (address.IndexOf('.') < 0)
            {
                return int.Parse(address);
            }

            string[] array = address.Split(new char[1] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            return int.Parse(array[0]) * 8 + int.Parse(array[1]);
        }

        //
        // 摘要:
        //     按照字节读取汇川M地址的数据，地址示例： MB100，MB101，需要注意的是，MB100 及 MB101 的地址是 MW50 的数据。
        //     Read the data of Inovance M address according to the byte, address example: MB100,
        //     MB101, it should be noted that the addresses of MB100 and MB101 are the data
        //     of MW50.
        //
        // 参数:
        //   modbus:
        //     汇川的PLC对象
        //
        //   address:
        //     地址信息
        //
        // 返回结果:
        //     读取的结果数据
        public static OperateResult<byte> ReadByte(IModbus modbus, string address)
        {
            int num = 0;
            if (address.StartsWith("MB") || address.StartsWith("mb"))
            {
                num = Convert.ToInt32(address.Substring(2));
            }
            else if (address.StartsWith("M") || address.StartsWith("m"))
            {
                num = Convert.ToInt32(address.Substring(1));
            }
            else
            {
                new OperateResult<string>(StringResources.Language.NotSupportedDataType);
            }

            OperateResult<byte[]> operateResult = modbus.Read("MW" + num / 2, 1);
            if (!operateResult.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte>(operateResult);
            }

            return OperateResult.CreateSuccessResult((num % 2 == 0) ? operateResult.Content[1] : operateResult.Content[0]);
        }

        public static async Task<OperateResult<byte>> ReadByteAsync(IModbus modbus, string address)
        {
            int offset = 0;
            if (address.StartsWith("MB") || address.StartsWith("mb"))
            {
                offset = Convert.ToInt32(address.Substring(2));
            }
            else if (address.StartsWith("M") || address.StartsWith("m"))
            {
                offset = Convert.ToInt32(address.Substring(1));
            }
            else
            {
                new OperateResult<string>(StringResources.Language.NotSupportedDataType);
            }

            OperateResult<byte[]> read = await modbus.ReadAsync("MW" + offset / 2, 1);
            if (!read.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte>(read);
            }

            return OperateResult.CreateSuccessResult((offset % 2 == 0) ? read.Content[1] : read.Content[0]);
        }

        //
        // 摘要:
        //     根据汇川PLC的地址，解析出转换后的modbus协议信息，适用AM,H3U,H5U系列的PLC
        //     According to the address of Inovance PLC, analyze the converted modbus protocol
        //     information, which is suitable for AM, H3U, H5U series PLC
        //
        // 参数:
        //   series:
        //     PLC的系列
        //
        //   address:
        //     汇川plc的地址信息
        //
        //   modbusCode:
        //     原始的对应的modbus信息
        //
        // 返回结果:
        //     Modbus格式的地址
        public static OperateResult<string> PraseInovanceAddress(InovanceSeries series, string address, byte modbusCode)
        {
            return series switch
            {
                InovanceSeries.AM => PraseInovanceAmAddress(address, modbusCode),
                InovanceSeries.H3U => PraseInovanceH3UAddress(address, modbusCode),
                InovanceSeries.H5U => PraseInovanceH5UAddress(address, modbusCode),
                _ => new OperateResult<string>($"[{series}] Not supported series of plc"),
            };
        }

        public static OperateResult<string> PraseInovanceAmAddress(string address, byte modbusCode)
        {
            try
            {
                string text = string.Empty;
                OperateResult<int> operateResult = HslHelper.ExtractParameter(ref address, "s");
                if (operateResult.IsSuccess)
                {
                    text = $"s={operateResult.Content};";
                }

                if (address.StartsWith("QX") || address.StartsWith("qx"))
                {
                    return OperateResult.CreateSuccessResult(text + CalculateStartAddress(address.Substring(2)));
                }

                if (address.StartsWith("Q") || address.StartsWith("q"))
                {
                    return OperateResult.CreateSuccessResult(text + CalculateStartAddress(address.Substring(1)));
                }

                if (address.StartsWith("IX") || address.StartsWith("ix"))
                {
                    return OperateResult.CreateSuccessResult(text + "x=2;" + CalculateStartAddress(address.Substring(2)));
                }

                if (address.StartsWith("I") || address.StartsWith("i"))
                {
                    return OperateResult.CreateSuccessResult(text + "x=2;" + CalculateStartAddress(address.Substring(1)));
                }

                if (address.StartsWith("MW") || address.StartsWith("mw"))
                {
                    return OperateResult.CreateSuccessResult(text + address.Substring(2));
                }

                if (address.StartsWith("MX") || address.StartsWith("mx"))
                {
                    if (modbusCode == 1 || modbusCode == 15 || modbusCode == 5)
                    {
                        if (address.IndexOf('.') > 0)
                        {
                            string[] array = address.Substring(2).Split(new char[1] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                            int num = Convert.ToInt32(array[0]);
                            int num2 = Convert.ToInt32(array[1]);
                            return OperateResult.CreateSuccessResult(text + num / 2 + "." + (num % 2 * 8 + num2));
                        }

                        int num3 = Convert.ToInt32(address.Substring(2));
                        return OperateResult.CreateSuccessResult(text + num3 / 2 + ".0");
                    }

                    int num4 = Convert.ToInt32(address.Substring(2));
                    return OperateResult.CreateSuccessResult(text + num4 / 2);
                }

                if (address.StartsWith("M") || address.StartsWith("m"))
                {
                    return OperateResult.CreateSuccessResult(text + address.Substring(1));
                }

                if (modbusCode == 1 || modbusCode == 15 || modbusCode == 5)
                {
                    if (address.StartsWith("SMX") || address.StartsWith("smx"))
                    {
                        return OperateResult.CreateSuccessResult(text + $"x={modbusCode + 48};" + CalculateStartAddress(address.Substring(3)));
                    }

                    if (address.StartsWith("SM") || address.StartsWith("sm"))
                    {
                        return OperateResult.CreateSuccessResult(text + $"x={modbusCode + 48};" + CalculateStartAddress(address.Substring(2)));
                    }
                }
                else
                {
                    if (address.StartsWith("SDW") || address.StartsWith("sdw"))
                    {
                        return OperateResult.CreateSuccessResult(text + $"x={modbusCode + 48};" + address.Substring(3));
                    }

                    if (address.StartsWith("SD") || address.StartsWith("sd"))
                    {
                        return OperateResult.CreateSuccessResult(text + $"x={modbusCode + 48};" + address.Substring(2));
                    }
                }

                return new OperateResult<string>(StringResources.Language.NotSupportedDataType);
            }
            catch (Exception ex)
            {
                return new OperateResult<string>(ex.Message);
            }
        }

        private static int CalculateH3UStartAddress(string address)
        {
            if (address.IndexOf('.') < 0)
            {
                return Convert.ToInt32(address, 8);
            }

            string[] array = address.Split(new char[1] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            return Convert.ToInt32(array[0], 8) * 8 + int.Parse(array[1]);
        }

        public static OperateResult<string> PraseInovanceH3UAddress(string address, byte modbusCode)
        {
            try
            {
                string text = string.Empty;
                OperateResult<int> operateResult = HslHelper.ExtractParameter(ref address, "s");
                if (operateResult.IsSuccess)
                {
                    text = $"s={operateResult.Content};";
                }

                if (modbusCode == 1 || modbusCode == 15 || modbusCode == 5)
                {
                    if (address.StartsWith("X") || address.StartsWith("x"))
                    {
                        return OperateResult.CreateSuccessResult(text + (CalculateH3UStartAddress(address.Substring(1)) + 63488));
                    }

                    if (address.StartsWith("Y") || address.StartsWith("y"))
                    {
                        return OperateResult.CreateSuccessResult(text + (CalculateH3UStartAddress(address.Substring(1)) + 64512));
                    }

                    if (address.StartsWith("SM") || address.StartsWith("sm"))
                    {
                        return OperateResult.CreateSuccessResult(text + (Convert.ToInt32(address.Substring(2)) + 9216));
                    }

                    if (address.StartsWith("S") || address.StartsWith("s"))
                    {
                        return OperateResult.CreateSuccessResult(text + (Convert.ToInt32(address.Substring(1)) + 57344));
                    }

                    if (address.StartsWith("T") || address.StartsWith("t"))
                    {
                        return OperateResult.CreateSuccessResult(text + (Convert.ToInt32(address.Substring(1)) + 61440));
                    }

                    if (address.StartsWith("C") || address.StartsWith("c"))
                    {
                        return OperateResult.CreateSuccessResult(text + (Convert.ToInt32(address.Substring(1)) + 62464));
                    }

                    if (address.StartsWith("M") || address.StartsWith("m"))
                    {
                        int num = Convert.ToInt32(address.Substring(1));
                        if (num >= 8000)
                        {
                            return OperateResult.CreateSuccessResult(text + (num - 8000 + 8000));
                        }

                        return OperateResult.CreateSuccessResult(text + num);
                    }
                }
                else
                {
                    if (address.StartsWith("D") || address.StartsWith("d"))
                    {
                        return OperateResult.CreateSuccessResult(text + Convert.ToInt32(address.Substring(1)));
                    }

                    if (address.StartsWith("SD") || address.StartsWith("sd"))
                    {
                        return OperateResult.CreateSuccessResult(text + (Convert.ToInt32(address.Substring(2)) + 9216));
                    }

                    if (address.StartsWith("R") || address.StartsWith("r"))
                    {
                        return OperateResult.CreateSuccessResult(text + (Convert.ToInt32(address.Substring(1)) + 12288));
                    }

                    if (address.StartsWith("T") || address.StartsWith("t"))
                    {
                        return OperateResult.CreateSuccessResult(text + (Convert.ToInt32(address.Substring(1)) + 61440));
                    }

                    if (address.StartsWith("C") || address.StartsWith("c"))
                    {
                        int num2 = Convert.ToInt32(address.Substring(1));
                        if (num2 >= 200)
                        {
                            return OperateResult.CreateSuccessResult(text + ((num2 - 200) * 2 + 63232));
                        }

                        return OperateResult.CreateSuccessResult(text + (num2 + 62464));
                    }
                }

                return new OperateResult<string>(StringResources.Language.NotSupportedDataType);
            }
            catch (Exception ex)
            {
                return new OperateResult<string>(ex.Message);
            }
        }

        public static OperateResult<string> PraseInovanceH5UAddress(string address, byte modbusCode)
        {
            try
            {
                string text = string.Empty;
                OperateResult<int> operateResult = HslHelper.ExtractParameter(ref address, "s");
                if (operateResult.IsSuccess)
                {
                    text = $"s={operateResult.Content};";
                }

                if (modbusCode == 1 || modbusCode == 15 || modbusCode == 5)
                {
                    if (address.StartsWith("X") || address.StartsWith("x"))
                    {
                        return OperateResult.CreateSuccessResult(text + (CalculateH3UStartAddress(address.Substring(1)) + 63488));
                    }

                    if (address.StartsWith("Y") || address.StartsWith("y"))
                    {
                        return OperateResult.CreateSuccessResult(text + (CalculateH3UStartAddress(address.Substring(1)) + 64512));
                    }

                    if (address.StartsWith("S") || address.StartsWith("s"))
                    {
                        return OperateResult.CreateSuccessResult(text + (Convert.ToInt32(address.Substring(1)) + 57344));
                    }

                    if (address.StartsWith("B") || address.StartsWith("b"))
                    {
                        return OperateResult.CreateSuccessResult(text + (Convert.ToInt32(address.Substring(1)) + 12288));
                    }

                    if (address.StartsWith("M") || address.StartsWith("m"))
                    {
                        return OperateResult.CreateSuccessResult(text + Convert.ToInt32(address.Substring(1)));
                    }
                }
                else
                {
                    if (address.StartsWith("D") || address.StartsWith("d"))
                    {
                        return OperateResult.CreateSuccessResult(text + Convert.ToInt32(address.Substring(1)));
                    }

                    if (address.StartsWith("R") || address.StartsWith("r"))
                    {
                        return OperateResult.CreateSuccessResult(text + (Convert.ToInt32(address.Substring(1)) + 12288));
                    }
                }

                return new OperateResult<string>(StringResources.Language.NotSupportedDataType);
            }
            catch (Exception ex)
            {
                return new OperateResult<string>(ex.Message);
            }
        }
    }
}