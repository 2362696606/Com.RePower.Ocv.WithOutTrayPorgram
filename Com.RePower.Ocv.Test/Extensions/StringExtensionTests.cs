using HslCommunication;
using Xunit.Abstractions;

namespace Com.RePower.Ocv.WpfBase.Extensions.Tests
{
    public class StringExtensionTests
    {
        public StringExtensionTests(ITestOutputHelper outputHelper)
        {
            OutputHelper = outputHelper;
        }

        public ITestOutputHelper OutputHelper { get; }

        [Fact()]
        public void ToHexBytesTest()
        {
            string str = "FF FF FF FF 01 28 05 01 01 00 00";
            byte[] bytes = str.ToHexBytes();
            OutputHelper.WriteLine(bytes.ToHexString(','));
        }
    }
}