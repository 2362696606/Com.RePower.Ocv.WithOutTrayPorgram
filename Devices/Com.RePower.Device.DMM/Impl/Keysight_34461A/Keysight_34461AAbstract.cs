using Com.RePower.DeviceBase;
using Com.RePower.DeviceBase.BaseDevice;
using Com.RePower.DeviceBase.DMM;
using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Device.DMM.Impl.Keysight_34461A
{
    public abstract class Keysight_34461AAbstract : DMMBase,IDMMNet
    {
        /// <summary>
        /// 当前测试模式
        /// </summary>
        private TestModel model = TestModel.Unknow;

        /// <summary>
        /// 直流电压量程
        /// </summary>
        public string DcRange { get; set; } = "auto on";

        /// <summary>
        /// 交流电压量程
        /// </summary>
        public string AcRange { get; set; } = "auto on";

        /// <summary>
        /// 电阻量程
        /// </summary>
        public string ResRange { get; set; } = "auto on";

        /// <summary>
        /// 直流电压NPLC
        /// </summary>
        public string DcNplc { get; set; } = "1";

        /// <summary>
        /// 电阻NPLC
        /// </summary>
        public string ResNplc { get; set; } = "1";

        /// <summary>
        /// 读取延迟
        /// </summary>
        public abstract int ReadDelay { get; set; }

        /// <summary>
        /// 测量结果单位
        /// </summary>
        public ResultUnit ResultUnit { get; set; } = ResultUnit.mV;

        /// <summary>
        /// 保留小数位
        /// </summary>
        public int Digits { get; set; } = 3;
        public abstract string IpAddress { get; set; }
        public abstract int Port { get; set; }

        protected virtual OperateResult<double> ReadValue(byte[] cmd)
        {
            var result = SendCmd(cmd);
            if (result.IsSuccess)
            {
                var byteValue = result.Content;
                if (byteValue == null || byteValue.Length <= 0)
                {
                    return OperateResult.CreateFailedResult<double>("设备返回byte[]为空");
                }
                else
                {
                    var value = TranslateToDouble(byteValue);
                    return OperateResult.CreateSuccessResult(value);
                }
            }
            else
            {
                return OperateResult.CreateFailedResult<double>(result.Message ?? "读取失败", result.ErrorCode);
            }
        }
        private string GetCmd(string name)
        {
            string cmdStr = string.Empty;
            switch (name)
            {
                case "SetDcFunc":
                    cmdStr = ":sens:func \"volt:dc\"";
                    break;
                case "SetAcFunc":
                    cmdStr = ":sens:func \"volt:ac\"";
                    break;
                case "SetResFunc":
                    cmdStr = ":sens:func \"res\"";
                    break;
                case "SetDcRange":
                    cmdStr = $":sens:volt:dc:range:{DcRange}";
                    break;
                case "SetAcRange":
                    cmdStr = $":sens:volt:ac:range:{AcRange}";
                    break;
                case "SetResRange":
                    cmdStr = $":sens:res:range:{ResRange}";
                    break;
                case "SetDcNplc":
                    cmdStr = $":sens:volt:dc:nplc:{DcNplc}";
                    break;
                case "SetResNplc":
                    cmdStr = $":sens:res:nplc:{ResNplc}";
                    break;
                case "Read":
                    cmdStr = "READ?";
                    break;
            }
            return cmdStr;
        }

        protected virtual double TranslateToDouble(byte[] bytes)
        {
            var value = (double)Decimal.Parse(Encoding.ASCII.GetString(bytes), System.Globalization.NumberStyles.Float);
            switch(ResultUnit)
            {
                default:
                case ResultUnit.mV:
                    value = value * 1000;
                    value = Math.Round(value, Digits);
                    break;
                case ResultUnit.V:
                    value = Math.Round(value, Digits);
                    break;
            }
            return value;
        }
        public override OperateResult<double> ReadAc()
        {
            List<string> cmdlist = new List<string>();
            if (this.model != TestModel.AC)
            {
                this.model = TestModel.AC;
                cmdlist.Add(GetCmd("SetAcFunc"));
                cmdlist.Add(Environment.NewLine);
                cmdlist.Add(GetCmd("SetAcRange"));
                cmdlist.Add(Environment.NewLine);
            }
            cmdlist.Add(GetCmd("Read"));
            cmdlist.Add(Environment.NewLine);
            string s = string.Join("", cmdlist);
            byte[] command = ASCIIEncoding.ASCII.GetBytes(s);
            return ReadValue(command);
        }
        public override OperateResult<double> ReadDc()
        {
            List<string> cmdlist = new List<string>();
            if (this.model != TestModel.DC)
            {
                this.model = TestModel.DC;
                cmdlist.Add(GetCmd("SetDcFunc"));
                cmdlist.Add(Environment.NewLine);
                cmdlist.Add(GetCmd("SetDcRange"));
                cmdlist.Add(Environment.NewLine);
            }
            cmdlist.Add(GetCmd("Read"));
            cmdlist.Add(Environment.NewLine);
            string s = string.Join("", cmdlist);
            byte[] command = ASCIIEncoding.ASCII.GetBytes(s);
            return ReadValue(command);
        }
        public override OperateResult<double> ReadRes()
        {
            List<string> cmdlist = new List<string>();
            if (this.model != TestModel.RES)
            {
                this.model = TestModel.RES;
                cmdlist.Add(GetCmd("SetResFunc"));
                cmdlist.Add(Environment.NewLine);
                cmdlist.Add(GetCmd("SetResRange"));
                cmdlist.Add(Environment.NewLine);
            }
            cmdlist.Add(GetCmd("Read"));
            cmdlist.Add(Environment.NewLine);
            string s = string.Join("", cmdlist);
            byte[] command = ASCIIEncoding.ASCII.GetBytes(s);
            return ReadValue(command);
        }

        public abstract OperateResult Connect(string ipAddress, int port);
        public abstract Task<OperateResult> ConnectAsync(string ipAddress, int port);
    }
}
