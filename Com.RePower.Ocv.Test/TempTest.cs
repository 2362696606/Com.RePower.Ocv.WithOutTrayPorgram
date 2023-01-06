using Com.RePower.Ocv.Model;
using Com.RePower.Ocv.Model.Entity;
using HslCommunication;
using HslCommunication.Profinet.Inovance;
using Newtonsoft.Json;
using Npoi.Mapper;
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

        [Fact]
        public void PlcValueCacheTest()
        {
            string jString = "[{\"Name\":\"托盘条码\",\"Address\":\"MW7100\",\"Type\":\"String\",\"Length\":\"20\",\"Description\":\"托盘号\"},{\"Name\":\"测试请求信号\",\"Address\":\"MW7010\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"设备准备到位，设备正常，请求校表(需要在WCS的反馈标志位0时才能发起请求)，收到WCS的反馈后自动复位\"},{\"Name\":\"位置1托盘电池状态\",\"Address\":\"MW7201\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"0:没状态\\r\\n1:OK\\r\\n2:NG\\r\\n3:一次复测\\r\\n4:二次复测\"},{\"Name\":\"位置2托盘电池状态\",\"Address\":\"MW7202\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"0:没状态\\r\\n1:OK\\r\\n2:NG\\r\\n3:一次复测\\r\\n4:二次复测\"},{\"Name\":\"位置3托盘电池状态\",\"Address\":\"MW7203\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"0:没状态\\r\\n1:OK\\r\\n2:NG\\r\\n3:一次复测\\r\\n4:二次复测\"},{\"Name\":\"位置4托盘电池状态\",\"Address\":\"MW7204\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"0:没状态\\r\\n1:OK\\r\\n2:NG\\r\\n3:一次复测\\r\\n4:二次复测\"},{\"Name\":\"位置5托盘电池状态\",\"Address\":\"MW7205\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"0:没状态\\r\\n1:OK\\r\\n2:NG\\r\\n3:一次复测\\r\\n4:二次复测\"},{\"Name\":\"位置6托盘电池状态\",\"Address\":\"MW7206\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"0:没状态\\r\\n1:OK\\r\\n2:NG\\r\\n3:一次复测\\r\\n4:二次复测\"},{\"Name\":\"位置7托盘电池状态\",\"Address\":\"MW7207\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"0:没状态\\r\\n1:OK\\r\\n2:NG\\r\\n3:一次复测\\r\\n4:二次复测\"},{\"Name\":\"位置8托盘电池状态\",\"Address\":\"MW7208\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"0:没状态\\r\\n1:OK\\r\\n2:NG\\r\\n3:一次复测\\r\\n4:二次复测\"},{\"Name\":\"位置9托盘电池状态\",\"Address\":\"MW7209\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"0:没状态\\r\\n1:OK\\r\\n2:NG\\r\\n3:一次复测\\r\\n4:二次复测\"},{\"Name\":\"位置10托盘电池状态\",\"Address\":\"MW7210\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"0:没状态\\r\\n1:OK\\r\\n2:NG\\r\\n3:一次复测\\r\\n4:二次复测\"},{\"Name\":\"位置11托盘电池状态\",\"Address\":\"MW7211\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"0:没状态\\r\\n1:OK\\r\\n2:NG\\r\\n3:一次复测\\r\\n4:二次复测\"},{\"Name\":\"位置12托盘电池状态\",\"Address\":\"MW7212\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"0:没状态\\r\\n1:OK\\r\\n2:NG\\r\\n3:一次复测\\r\\n4:二次复测\"},{\"Name\":\"位置13托盘电池状态\",\"Address\":\"MW7213\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"0:没状态\\r\\n1:OK\\r\\n2:NG\\r\\n3:一次复测\\r\\n4:二次复测\"},{\"Name\":\"位置14托盘电池状态\",\"Address\":\"MW7214\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"0:没状态\\r\\n1:OK\\r\\n2:NG\\r\\n3:一次复测\\r\\n4:二次复测\"},{\"Name\":\"位置15托盘电池状态\",\"Address\":\"MW7215\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"0:没状态\\r\\n1:OK\\r\\n2:NG\\r\\n3:一次复测\\r\\n4:二次复测\"},{\"Name\":\"位置16托盘电池状态\",\"Address\":\"MW7216\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"0:没状态\\r\\n1:OK\\r\\n2:NG\\r\\n3:一次复测\\r\\n4:二次复测\"},{\"Name\":\"位置17托盘电池状态\",\"Address\":\"MW7217\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"0:没状态\\r\\n1:OK\\r\\n2:NG\\r\\n3:一次复测\\r\\n4:二次复测\"},{\"Name\":\"位置18托盘电池状态\",\"Address\":\"MW7218\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"0:没状态\\r\\n1:OK\\r\\n2:NG\\r\\n3:一次复测\\r\\n4:二次复测\"},{\"Name\":\"位置19托盘电池状态\",\"Address\":\"MW7219\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"0:没状态\\r\\n1:OK\\r\\n2:NG\\r\\n3:一次复测\\r\\n4:二次复测\"},{\"Name\":\"位置20托盘电池状态\",\"Address\":\"MW7220\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"0:没状态\\r\\n1:OK\\r\\n2:NG\\r\\n3:一次复测\\r\\n4:二次复测\"},{\"Name\":\"位置21托盘电池状态\",\"Address\":\"MW7221\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"0:没状态\\r\\n1:OK\\r\\n2:NG\\r\\n3:一次复测\\r\\n4:二次复测\"},{\"Name\":\"位置22托盘电池状态\",\"Address\":\"MW7222\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"0:没状态\\r\\n1:OK\\r\\n2:NG\\r\\n3:一次复测\\r\\n4:二次复测\"},{\"Name\":\"位置23托盘电池状态\",\"Address\":\"MW7223\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"0:没状态\\r\\n1:OK\\r\\n2:NG\\r\\n3:一次复测\\r\\n4:二次复测\"},{\"Name\":\"位置24托盘电池状态\",\"Address\":\"MW7224\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"0:没状态\\r\\n1:OK\\r\\n2:NG\\r\\n3:一次复测\\r\\n4:二次复测\"},{\"Name\":\"位置25托盘电池状态\",\"Address\":\"MW7225\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"0:没状态\\r\\n1:OK\\r\\n2:NG\\r\\n3:一次复测\\r\\n4:二次复测\"},{\"Name\":\"位置26托盘电池状态\",\"Address\":\"MW7226\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"0:没状态\\r\\n1:OK\\r\\n2:NG\\r\\n3:一次复测\\r\\n4:二次复测\"},{\"Name\":\"位置27托盘电池状态\",\"Address\":\"MW7227\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"0:没状态\\r\\n1:OK\\r\\n2:NG\\r\\n3:一次复测\\r\\n4:二次复测\"},{\"Name\":\"位置28托盘电池状态\",\"Address\":\"MW7228\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"0:没状态\\r\\n1:OK\\r\\n2:NG\\r\\n3:一次复测\\r\\n4:二次复测\"},{\"Name\":\"位置29托盘电池状态\",\"Address\":\"MW7229\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"0:没状态\\r\\n1:OK\\r\\n2:NG\\r\\n3:一次复测\\r\\n4:二次复测\"},{\"Name\":\"位置30托盘电池状态\",\"Address\":\"MW7230\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"0:没状态\\r\\n1:OK\\r\\n2:NG\\r\\n3:一次复测\\r\\n4:二次复测\"},{\"Name\":\"位置31托盘电池状态\",\"Address\":\"MW7231\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"0:没状态\\r\\n1:OK\\r\\n2:NG\\r\\n3:一次复测\\r\\n4:二次复测\"},{\"Name\":\"位置32托盘电池状态\",\"Address\":\"MW7232\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"0:没状态\\r\\n1:OK\\r\\n2:NG\\r\\n3:一次复测\\r\\n4:二次复测\"},{\"Name\":\"位置33托盘电池状态\",\"Address\":\"MW7233\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"0:没状态\\r\\n1:OK\\r\\n2:NG\\r\\n3:一次复测\\r\\n4:二次复测\"},{\"Name\":\"位置34托盘电池状态\",\"Address\":\"MW7234\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"0:没状态\\r\\n1:OK\\r\\n2:NG\\r\\n3:一次复测\\r\\n4:二次复测\"},{\"Name\":\"位置35托盘电池状态\",\"Address\":\"MW7235\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"0:没状态\\r\\n1:OK\\r\\n2:NG\\r\\n3:一次复测\\r\\n4:二次复测\"},{\"Name\":\"位置36托盘电池状态\",\"Address\":\"MW7236\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"0:没状态\\r\\n1:OK\\r\\n2:NG\\r\\n3:一次复测\\r\\n4:二次复测\"},{\"Name\":\"校表请求信号\",\"Address\":\"MW7004\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"1:请求校表\"},{\"Name\":\"设备故障代码\",\"Address\":\"MW7002\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"故障代码\"},{\"Name\":\"运行状态\",\"Address\":\"MW7000\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"0:没启动\\r\\n1:自动运行\\r\\n2:手动运行\\r\\n3:故障\"},{\"Name\":\"测试状态\",\"Address\":\"MW7012\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"0:机器没任务\\r\\n1:测试1到位\\r\\n2:测试2到位\\r\\n3:测试3到位\\r\\n4:测试4到位\\r\\n5:测试5到位\\r\\n6:测试6到位\\r\\n7:测试7到位\\r\\n8:测试8到位\\r\\n（收到拆盘完成信号后清0）\"},{\"Name\":\"拆盘请求信号\",\"Address\":\"MW7014\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"1:请求拆盘\"},{\"Name\":\"托盘状态\",\"Address\":\"MW7016\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"0:机器没任务\\r\\n1:拆盘中\\r\\n2:拆盘完成（收到拆盘完成信号后清0）\\r\\n4:满盘NG出\"},{\"Name\":\"校表状态\",\"Address\":\"MW7008\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"0:机器没任务\\r\\n1:校表中\\r\\n2:清空中\\r\\n3:计量中（收到拆盘完成信号后清0）\"},{\"Name\":\"清空/计量选择\",\"Address\":\"MW7018\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"0:取消\\r\\n1:清空选择\\r\\n2:计量选择\\r\\n3:清空计量选择\"},{\"Name\":\"校表标志位\",\"Address\":\"MW7020\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"0:初始状态\\r\\n1:是开始校表\\r\\n2:是清空完成\\r\\n3:是计量完成\\r\\n4:无校表任务（收到校表无任务时，WCS自动复位标志）\"},{\"Name\":\"测试标志位\",\"Address\":\"MW7022\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"0:初始状态\\r\\n1:可以开始测试\\r\\n2:测试总完成\\r\\n3:该托盘号不合规\\r\\n4:该托盘号已经存在\\r\\n5:电芯条码不合规格\\r\\n6:电芯条码重复\\r\\n7:其他\\r\\n11:测试1完成\\r\\n12:测试2完成:\\r\\n13:测试3完成\\r\\n14:测试4完成\\r\\n15:测试5完成\\r\\n16:测试6完成\\r\\n17:测试7完成\\r\\n18:测试8完成\\r\\n111:测试1复测\\r\\n112:测试2复测\\r\\n113:测试3复测\\r\\n114:测试4复测\\r\\n115:测试5复测\\r\\n116:测试6复测\\r\\n117:测试7复测\\r\\n118:测试8复测\\r\\n（收到测试无任务时，上位机自动清除次标志）\"},{\"Name\":\"拆盘标志位\",\"Address\":\"MW7024\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"0:是初始状态\\r\\n1:是开始拆盘\\r\\n2:是收到PLC拆盘完成确认（收到机器人无任务时，上位机自动复位标志）\\r\\n3:满盘NG（NG1或NG2）\\r\\n4:收到PLC满盘NG确认（上位机解绑该满盘NG盘，因为异常口无扫码）\"},{\"Name\":\"请求信号\",\"Address\":\"MW7006\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"1:请求清空\\r\\n2:请求计量\"},{\"Name\":\"探针压合次数\",\"Address\":\"MW7999\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"探针压合次数\"},{\"Name\":\"电芯条码\",\"Address\":\"MW15410\",\"Type\":\"String\",\"Length\":\"1\",\"Description\":\"电芯条码(Ocv3)\"},{\"Name\":\"测试状态\",\"Address\":\"MW202\",\"Type\":\"Int16\",\"Length\":\"1\",\"Description\":\"测试状态(Ocv3)\"}]";
            List<PlcCacheValue> plcCacheValues = JsonConvert.DeserializeObject<List<PlcCacheValue>>(jString) ?? new List<PlcCacheValue>();
            //Array.Sort(plcCacheValues.ToArray(), new CustomComparer());
            plcCacheValues.Sort(new CustomComparer());
            var mapper = new Mapper();
            mapper.Save("柳州Plc协议.xlsx", plcCacheValues);
            string resultString = JsonConvert.SerializeObject(plcCacheValues);
            OutputHelper.WriteLine(resultString);
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

    public class CustomComparer : IComparer<PlcCacheValue>
    {
        //public int Compare(object? x, object? y)
        //{
        //    var value1 = x as PlcCacheValue;
        //    var value2 = y as PlcCacheValue;

        //    string s1 = value1?.Address ?? string.Empty;

        //    string s2 = value2?.Address ?? string.Empty;

        //    if (s1.Length > s2.Length) return 1;

        //    if (s1.Length < s2.Length) return -1;

        //    for (int i = 0; i < s1.Length; i++)
        //    {

        //        if (s1[i] > s2[i]) return 1;

        //        if (s1[i] < s2[i]) return -1;

        //    }

        //    return 0;
        //}

        public int Compare(PlcCacheValue? x, PlcCacheValue? y)
        {
            string s1 = x?.Address ?? string.Empty;

            string s2 = y?.Address ?? string.Empty;

            if (s1.Length > s2.Length) return 1;

            if (s1.Length < s2.Length) return -1;

            for (int i = 0; i < s1.Length; i++)
            {

                if (s1[i] > s2[i]) return 1;

                if (s1[i] < s2[i]) return -1;

            }

            return 0;
        }
    }
}
