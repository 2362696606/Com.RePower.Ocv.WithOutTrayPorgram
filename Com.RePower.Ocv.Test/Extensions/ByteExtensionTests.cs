using Xunit.Abstractions;
using Com.RePower.WpfBase.Extensions;

namespace Com.RePower.DeviceBase.Extensions.Tests
{
    public class ByteExtensionTests
    {
        public ByteExtensionTests(ITestOutputHelper outputHelper)
        {
            OutputHelper = outputHelper;
        }

        public ITestOutputHelper OutputHelper { get; }

        [Fact()]
        public void GetCheckXorTest()
        {
            int boardIndex = 1;
            var baseCmd = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, Convert.ToByte(boardIndex), 0x29, 0x05, 0x00, 0x00, 0x00, 0x00 };
            byte xor = baseCmd.GetCheckXor();
            List<byte> cmd = baseCmd.ToList();
            cmd.Add(xor);
            OutputHelper.WriteLine(cmd.ToArray().ToHexString(','));
            //Assert.True(false, "This test needs an implementation");
        }
    }
}