using HslCommunication.Profinet.Inovance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var result = InovanceHelper.PraseInovanceAMAddress("MB100.0", 16);
            OutputHelper.WriteLine(result.Content);
        }
    }
}
