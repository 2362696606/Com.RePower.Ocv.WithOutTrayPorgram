using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Device.DMM.Impl
{
    public class Keysight_34461A : DMMNetAbstract
    {
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
        /// 当前测试模式
        /// </summary>
        private AvoTestModel model = AvoTestModel.Unknow;

        public override OperateResult Connect()
        {
            this.model = AvoTestModel.Unknow;
            return base.Connect();
        }
        public override OperateResult Connect(string ipAddress, int port)
        {
            this.model = AvoTestModel.Unknow;
            return base.Connect(ipAddress, port);
        }
        public override async Task<OperateResult> ConnectAsync()
        {
            this.model = AvoTestModel.Unknow;
            return await base.ConnectAsync();
        }
        public override async Task<OperateResult> ConnectAsync(string ipAddress, int port)
        {
            this.model = AvoTestModel.Unknow;
            return await base.ConnectAsync(ipAddress, port);
        }

        protected override byte[] GetReadAcCmd()
        {
            List<string> cmdlist = new List<string>();
            if (this.model != AvoTestModel.AC)
            {
                this.model = AvoTestModel.AC;
                cmdlist.Add(GetCmd("SetAcFunc"));
                cmdlist.Add(Environment.NewLine);
                cmdlist.Add(GetCmd("SetAcRange"));
                cmdlist.Add(Environment.NewLine);
            }
            cmdlist.Add(GetCmd("Read"));
            cmdlist.Add(Environment.NewLine);
            string s = string.Join("", cmdlist);
            byte[] command = ASCIIEncoding.ASCII.GetBytes(s);
            return command;
        }

        protected override byte[] GetReadDcCmd()
        {
            List<string> cmdlist = new List<string>();
            if (this.model != AvoTestModel.DC)
            {
                this.model = AvoTestModel.DC;
                cmdlist.Add(GetCmd("SetDcFunc"));
                cmdlist.Add(Environment.NewLine);
                cmdlist.Add(GetCmd("SetDcRange"));
                cmdlist.Add(Environment.NewLine);
            }
            cmdlist.Add(GetCmd("Read"));
            cmdlist.Add(Environment.NewLine);
            string s = string.Join("", cmdlist);
            byte[] command = ASCIIEncoding.ASCII.GetBytes(s);
            return command;
        }

        protected override byte[] GetReadResCmd()
        {
            List<string> cmdlist = new List<string>();
            if (this.model != AvoTestModel.RES)
            {
                this.model = AvoTestModel.RES;
                cmdlist.Add(GetCmd("SetResFunc"));
                cmdlist.Add(Environment.NewLine);
                cmdlist.Add(GetCmd("SetResRange"));
                cmdlist.Add(Environment.NewLine);
            }
            cmdlist.Add(GetCmd("Read"));
            cmdlist.Add(Environment.NewLine);
            string s = string.Join("", cmdlist);
            byte[] command = ASCIIEncoding.ASCII.GetBytes(s);
            return command;
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

        protected override double TranslateToDouble(byte[] bytes)
        {
            return (double)Decimal.Parse(Encoding.ASCII.GetString(bytes), System.Globalization.NumberStyles.Float);
        }
        private enum AvoTestModel
        {
            Unknow,
            AC,
            DC,
            RES
        }
    }
}
