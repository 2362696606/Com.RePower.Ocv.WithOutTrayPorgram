using Com.RePower.WpfBase;
using Com.RePower.WpfBase.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Device.Ohm.Impl.Hioki_BT3562
{
    public abstract class Hioki_BT3562Abstruct : OhmBase
    {
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
                    double parseDoubleVal = (double)decimal.Parse(values[0],
                        System.Globalization.NumberStyles.Float) * 1000;
                    return OperateResult.CreateSuccessResult(parseDoubleVal);
                }
            }
        }
    }
}
