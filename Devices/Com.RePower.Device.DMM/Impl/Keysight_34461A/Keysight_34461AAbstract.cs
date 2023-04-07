using Com.RePower.DeviceBase.DMM;
using Com.RePower.WpfBase;
using System.Text;

namespace Com.RePower.Device.DMM.Impl.Keysight_34461A
{
    public abstract class Keysight34461AAbstract : DmmBase, IDmmNet
    {
        /// <summary>
        /// 当前测试模式
        /// </summary>
        private TestModel _model = TestModel.Unknow;

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
        public ResultUnit ResultUnit { get; set; } = ResultUnit.MV;

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
            switch (ResultUnit)
            {
                default:
                case ResultUnit.MV:
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
            if (this._model != TestModel.Ac)
            {
                this._model = TestModel.Ac;
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
            if (this._model != TestModel.Dc)
            {
                this._model = TestModel.Dc;
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
            if (this._model != TestModel.Res)
            {
                this._model = TestModel.Res;
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