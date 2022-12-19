using Com.RePower.DeviceBase.Extensions;
using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Device.SwitchBoard.Impl
{
    public class SixLinesSwitchBoardImpl : SwitchBoardAbstract
    {
        public override OperateResult CloseAllChannels(int boardIndex)
        {
            if (boardIndex > 2 || boardIndex < 1)
            {
                return OperateResult.CreateFailedResult($"箱号{boardIndex}不合规");
            }
            var cmd = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, Convert.ToByte(boardIndex), 0x29, 0x05, 0x00, 0x00, 0x00, 0x00 };
            var result = SendCmd(cmd);
            if (result.IsFailed)
            {
                return OperateResult.CreateFailedResult(result.Message ?? "打开通道失败");
            }
            var bytes = result.Content;
            if(bytes == null)
            {
                return OperateResult.CreateFailedResult($"发送串口数据成功，但未收到回包，发送{cmd}");
            }
            return OperateResult.CreateSuccessResult();
        }

        private byte[] GetCmd(int boardIndex, int channel1 = 0, bool open1 = true, int channel2 = 0, bool open2 = true, int channel3 = 0, bool open3 = true, int channel4 = 0, bool open4 = true)
        {
            byte[] baseCmd = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF
                , Convert.ToByte(boardIndex)
                , 0x2A, 0x0B
                , channel1 == 0?Convert.ToByte(0xFF):Convert.ToByte(channel1), channel1 == 0?Convert.ToByte(0xFF):Convert.ToByte(open1)
                , channel2 == 0?Convert.ToByte(0xFF):Convert.ToByte(channel2), channel2 == 0?Convert.ToByte(0xFF):Convert.ToByte(open2)
                , channel3 == 0?Convert.ToByte(0xFF):Convert.ToByte(channel3), channel3 == 0?Convert.ToByte(0xFF):Convert.ToByte(open3)
                , channel4 == 0?Convert.ToByte(0xFF):Convert.ToByte(channel4), channel4 == 0?Convert.ToByte(0xFF):Convert.ToByte(open4)
                , 0xFF, 0xFF };
            byte xor = baseCmd.GetCheckXor();
            List<byte> temp = baseCmd.ToList();
            temp.Add(xor);
            return temp.ToArray();
        }

        public override OperateResult CloseChannel(int boardIndex, int channel)
        {
            if (boardIndex > 2 || boardIndex < 1 || channel > 24 || channel < 1)
            {
                return OperateResult.CreateFailedResult($"箱号{boardIndex},通道号{channel}不合规");
            }
            var cmd = GetCmd(boardIndex, channel,false);
            var result = SendCmd(cmd);
            if (result.IsFailed)
            {
                return OperateResult.CreateFailedResult(result.Message ?? "打开通道失败");
            }
            var bytes = result.Content;
            if (bytes == null)
            {
                return OperateResult.CreateFailedResult($"发送串口数据成功，但未收到回包，发送{cmd}");
            }
            return OperateResult.CreateSuccessResult();
        }

        public override OperateResult CloseChannels(int boardIndex, int[] channels)
        {
            if (boardIndex > 2 || boardIndex < 1)
            {
                return OperateResult.CreateFailedResult($"箱号{boardIndex}不合规");
            }
            if (channels.ToList().Any(x => x > 24 || x < 1))
            {
                return OperateResult.CreateFailedResult("通道" + string.Join(", ", channels) + "中包含不合规通道");
            }
            if (channels.Length < 1)
            {
                return OperateResult.CreateFailedResult("未选择通道");
            }
            List<int[]> channelList = channels.SplitAry(4);
            foreach (var item in channelList)
            {
                byte[] cmd;
                switch (item.Length)
                {
                    case 1:
                        cmd = GetCmd(boardIndex, item[0], false);
                        break;
                    case 2:
                        cmd = GetCmd(boardIndex, item[0], false, item[1], false);
                        break;
                    case 3:
                        cmd = GetCmd(boardIndex, item[0], false, item[1], false, item[2], false);
                        break;
                    default:
                        cmd = GetCmd(boardIndex, item[0], false, item[1], false, item[2], false, item[3], false);
                        break;
                }
                var result = SendCmd(cmd);
                if (result.IsFailed)
                {
                    return OperateResult.CreateFailedResult(result.Message ?? "打开通道失败");
                }
                var bytes = result.Content;
                if (bytes == null)
                {
                    return OperateResult.CreateFailedResult($"发送串口数据成功，但未收到回包，发送{cmd}");
                }
            }
            return OperateResult.CreateSuccessResult();
        }

        public override OperateResult OpenChannel(int boardIndex, int channel)
        {
            if (boardIndex > 2 || boardIndex < 1 || channel > 24 || channel < 1) 
            {
                return OperateResult.CreateFailedResult($"箱号{boardIndex},通道号{channel}不合规");
            }
            var cmd = GetCmd(boardIndex, channel);
            var result = SendCmd(cmd);
            if (result.IsFailed)
            {
                return OperateResult.CreateFailedResult(result.Message ?? "打开通道失败");
            }
            var bytes = result.Content;
            if (bytes == null)
            {
                return OperateResult.CreateFailedResult($"发送串口数据成功，但未收到回包，发送{cmd}");
            }
            return OperateResult.CreateSuccessResult();
        }

        public override OperateResult OpenChannels(int boardIndex, int[] channels)
        {
            if(boardIndex>2||boardIndex<1)
            {
                return OperateResult.CreateFailedResult($"箱号{boardIndex}不合规");
            }
            if(channels.ToList().Any(x=>x>24||x<1))
            {
                return OperateResult.CreateFailedResult("通道" + string.Join(", ", channels) + "中包含不合规通道");
            }
            if(channels.Length<1)
            {
                return OperateResult.CreateFailedResult("未选择通道");
            }
            List<int[]> channelList = channels.SplitAry(4);
            foreach(var item in channelList)
            {
                byte[] cmd;
                switch(item.Length)
                {
                    case 1:
                        cmd = GetCmd(boardIndex, item[0], true);
                        break;
                    case 2:
                        cmd = GetCmd(boardIndex, item[0], true, item[1], true);
                        break;
                    case 3:
                        cmd = GetCmd(boardIndex, item[0], true, item[1], true, item[2], true);
                        break;
                    default:
                        cmd = GetCmd(boardIndex, item[0], true, item[1], true, item[2], true, item[3], true);
                        break;
                }
                var result = SendCmd(cmd);
                if (result.IsFailed)
                {
                    return OperateResult.CreateFailedResult(result.Message ?? "打开通道失败");
                }
                var bytes = result.Content;
                if (bytes == null)
                {
                    return OperateResult.CreateFailedResult($"发送串口数据成功，但未收到回包，发送{cmd}");
                }
            }
            return OperateResult.CreateSuccessResult();
        }
    }
}
