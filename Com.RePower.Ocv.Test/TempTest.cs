using HslCommunication;
using HslCommunication.Profinet.Inovance;
using System.Text;
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
        [Fact]
        public void DoubleParseTest()
        {
            string byteStr = "35393831452D332C2D302E3137303531452B300D0A2020302E";
            byte[] bytes = byteStr.ToHexBytes();
            string value = Encoding.ASCII.GetString(bytes);
            OutputHelper.WriteLine(value);
            //string value = "0.6859E-3";
            //var value1 = double.Parse(value);
        }
    }
}
