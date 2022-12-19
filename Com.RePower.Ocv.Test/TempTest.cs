using HslCommunication;
using HslCommunication.Profinet.Inovance;
using Xunit.Abstractions;

namespace Com.RePower.Ocv.Test
{
    public class TempTest
    {
        public ITestOutputHelper OutputHelper { get; set; }
        public TempTest(ITestOutputHelper outputHelper)
        {
            OutputHelper = outputHelper;
        }
        [Fact]
        public void PraseInovanceAMAddressTest()
        {
            int[] a = { 1, 2, 3, };
            OutputHelper.WriteLine(a.ToArrayString());
        }
    }
}
