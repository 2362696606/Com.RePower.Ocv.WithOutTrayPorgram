using Com.RePower.WpfBase;
using Com.RePower.WpfBase.Extensions;

namespace Com.RePower.Device.SwitchBoard.Impl.FourLinesSwitchBoard
{
    public abstract class FourLinesSwitchBoardAbstract : SwitcbBoardBase
    {
        public abstract int ReadDelay { get; set; }

        public override OperateResult CloseAllChannels(int boardIndex)
        {
            if (boardIndex > 2 || boardIndex < 1)
            {
                return OperateResult.CreateFailedResult($"箱号{boardIndex}不合规");
            }
            var baseCmd = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, Convert.ToByte(boardIndex), 0x29, 0x05, 0x00, 0x00, 0x00, 0x00 };
            byte xor = baseCmd.GetCheckXor();
            List<byte> cmd = baseCmd.ToList();
            cmd.Add(xor);
            var result = SendCmd(cmd.ToArray());
            if (result.IsFailed)
            {
                return OperateResult.CreateFailedResult($"关闭所有通道失败:{result.Message ?? "未知原因"}");
            }
            var bytes = result.Content;
            if (bytes == null)
            {
                return OperateResult.CreateFailedResult($"发送串口数据成功，但未收到回包，发送{cmd}");
            }
            return OperateResult.CreateSuccessResult();
        }

        private byte[] GetCmd(int boardIndex, int channel1 = 0, bool open1 = true)
        {
            byte[] baseCmd = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, Convert.ToByte(boardIndex), 0x28, 0x05, Convert.ToByte(channel1), Convert.ToByte(open1), 0x00, 0x00 };
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
            var cmd = GetCmd(boardIndex, channel, false);
            var result = SendCmd(cmd);
            if (result.IsFailed)
            {
                return OperateResult.CreateFailedResult($"关闭通道{boardIndex}失败:{result.Message ?? "未知原因"}");
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
            foreach (var item in channels)
            {
                var result = CloseChannel(boardIndex, item);
                if (result.IsFailed)
                {
                    return result;
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
                return OperateResult.CreateFailedResult($"打开通道{boardIndex}失败:{result.Message ?? "未知原因"}");
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
            foreach (var item in channels)
            {
                var result = OpenChannel(boardIndex, item);
                if (result.IsFailed)
                {
                    return result;
                }
            }
            return OperateResult.CreateSuccessResult();
        }
    }
}