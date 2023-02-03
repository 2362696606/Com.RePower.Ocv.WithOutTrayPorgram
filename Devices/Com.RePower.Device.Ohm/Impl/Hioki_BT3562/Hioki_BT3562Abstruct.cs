using Com.RePower.WpfBase;
using Com.RePower.WpfBase.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Device.Ohm.Impl.Hioki_BT3562
{
    public abstract class Hioki_BT3562Abstruct : OhmBase
    {
        /// <summary>
        /// 结果单位
        /// </summary>
        public ResultUnit ResultUnit { get; set; } = ResultUnit.mΩ;

        /// <summary>
        /// 保留小数位
        /// </summary>
        public int Digits { get; set; } = 3;
        public override OperateResult<double> ReadRes()
        {
            byte[] cmd = Encoding.ASCII.GetBytes(":READ?" + "\r\n");
            var result = SendCmd(cmd.ToArray());
            if (result.IsFailed)
            {
                return OperateResult.CreateFailedResult<double>($"读取内阻失败:{result.Message ?? "未知原因"}");
            }
            var bytes = result.Content;
            if (bytes == null)
            {
                return OperateResult.CreateFailedResult<double>($"发送串口数据成功，但未收到回包，发送{cmd}");
            }
            else
            {
                string value = Encoding.ASCII.GetString(bytes);
                if (string.IsNullOrEmpty(value))
                {
                    return OperateResult.CreateFailedResult<double>("读取到串口回包转化成的字符串为空");
                }
                else
                {
                    string[] values = value.Split(',');
                    //转换为毫安
                    //double parseDoubleVal = (double)decimal.Parse(values[0],
                    //    System.Globalization.NumberStyles.Float) * 1000;
                    //var parseDoubleVal = double.Parse(values[0]) * 1000;
                    var parseDoubleVal = TranslateToDouble(values[0].Replace(" ", ""));
                    return OperateResult.CreateSuccessResult(parseDoubleVal);
                }
            }
        }
        protected virtual double TranslateToDouble(string valueSource)
        {
            //var value = (double)Decimal.Parse(Encoding.ASCII.GetString(bytes), System.Globalization.NumberStyles.Float);
            var value = double.Parse(valueSource);
            switch(ResultUnit)
            {
                default:
                case ResultUnit.mΩ:
                    value = value * 1000;
                    value = Math.Round(value, Digits);
                    break;
                case ResultUnit.Ω:
                    value = Math.Round(value, Digits);
                    break;
            }
            return value;
        }
    }
}
