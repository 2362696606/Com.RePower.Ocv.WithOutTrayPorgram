using HslCommunication;
using HslCommunication.Profinet.Inovance;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO.Ports;
using System.Text;
using System.Threading;
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
        [Fact]
        public void WaitTest()
        {
            int readDelay = 5000;
            int timeOut = 2000;
            Stopwatch stopwatch = Stopwatch.StartNew();
            ManualResetEvent manualResetEvent = new ManualResetEvent(false);
            //SerialDataReceivedEventHandler setFlag = (sender, e) => { manualResetEvent.Set()};
            var setFlag = () => { manualResetEvent.Set(); };
            Task.Run(() =>
            {
                int sleepTime = 3000;
                OutputHelper.WriteLine($"开始发送，{sleepTime}ms后收到回复，计时{stopwatch.ElapsedMilliseconds}");
                Thread.Sleep(sleepTime);
                OutputHelper.WriteLine($"收到回复，计时{stopwatch.ElapsedMilliseconds}");
                setFlag.Invoke();
            });
            //serialPort.Write(bytes, 0, bytes.Length);
            //Task t1 = Task.Run(() => 
            //{
            //    OutputHelper.WriteLine($"开始睡眠，计时{stopwatch.ElapsedMilliseconds}");
            //    Thread.Sleep(readDelay);
            //    OutputHelper.WriteLine($"睡眠结束，计时{stopwatch.ElapsedMilliseconds}");
            //});
            Task t1 = Task.Delay(readDelay);
            bool waitResult = false;
            Task t2 = Task.Run(() =>
            {
                OutputHelper.WriteLine($"开始等待，计时{stopwatch.ElapsedMilliseconds}");
                waitResult = manualResetEvent.WaitOne(timeOut);
                OutputHelper.WriteLine($"等待结束，计时{stopwatch.ElapsedMilliseconds},等待结果{waitResult}");
            });
            Task.WaitAll(t1, t2);
            if (!waitResult)
            {
                OutputHelper.WriteLine("超时");
            }
        }
        [Fact]
        public void JsonConvertTest()
        {
            var testEntity = new TestClass();
            string jsonStr = JsonConvert.SerializeObject(testEntity);
            OutputHelper.WriteLine(jsonStr);
            string jsonStr1 = "{\"StringValue\":\"\",\"EnumValue\":\"Ω\",\"IntValue\":0}";
            var obj = JsonConvert.DeserializeObject<TestClass>(jsonStr1);
        }
    }
    public class TestClass
    {
        public string StringValue { get; set; } = string.Empty;
        public TestEnum EnumValue { get; set; } = TestEnum.mΩ;
        public int IntValue { get; set; }
    }
    public enum TestEnum
    {
        mΩ,
        Ω
    }
}
