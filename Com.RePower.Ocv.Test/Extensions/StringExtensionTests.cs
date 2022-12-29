using Xunit;
using Com.RePower.Ocv.WpfBase.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using HslCommunication;

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