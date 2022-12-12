using Xunit;
using Com.RePower.Device.Plc;
using Com.RePower.Device.Plc.Impl;
using Com.RePower.DeviceBase.Plc;
using Xunit.Abstractions;

namespace Com.RePower.Device.Plc.Tests
{
    public class PlcTest
    {
        public ITestOutputHelper OutPutHelper { get; set; }
        public PlcTest(ITestOutputHelper outputHelper)
        {
            this.OutPutHelper = outputHelper;
        }
        [Fact(DisplayName = "��������")]
        public void ConnectTest()
        {
            IPlc plc = new InovanceTcpNetPlcImpl();
            IPlcNet plcNet = (plc as IPlcNet) ?? throw new Exception("�޷�ת��ΪIPlcNet");
            var result = plcNet.Connect("192.168.3.88", 502);
            Assert.True(result.IsSuccess);
        }

        [Fact(DisplayName = "��������")]
        public void ConnectTest1()
        {
            IPlc plc = new InovanceTcpNetPlcImpl();
            IPlcNet plcNet = (plc as IPlcNet) ?? throw new Exception("�޷�ת��ΪIPlcNet");
            plcNet.IpAddress = "192.168.3.88";
            plcNet.Port = 502;
            var result = plcNet.Connect();
            Assert.True(result.IsSuccess);
        }

        [Fact(DisplayName = "�����첽����")]
        public void ConnectAsyncTest()
        {
            IPlc plc = new InovanceTcpNetPlcImpl();
            IPlcNet plcNet = (plc as IPlcNet) ?? throw new Exception("�޷�ת��ΪIPlcNet");
            Assert.False(plcNet.IsConnected);
            DoConnectAsync(plcNet);
            OutPutHelper.WriteLine("�ѵ�������");
            //Assert.True(plcNet.IsConnected);
            async void DoConnectAsync(IPlcNet plcNet)
            {
                var result = await plcNet.ConnectAsync("192.168.3.88", 502);
                Thread.Sleep(1000);
                OutPutHelper.WriteLine("�ɹ�����");
            }
        }

        [Fact(DisplayName = "����д��bool")]
        public void WriteTest()
        {
            IPlc plc = new InovanceTcpNetPlcImpl();
            IPlcNet plcNet = (plc as IPlcNet) ?? throw new Exception("�޷�ת��ΪIPlcNet");
            plcNet.IpAddress = "192.168.3.88";
            plcNet.Port = 502;
            plcNet.Connect();
            Assert.True(plcNet.IsConnected);
            var result = plcNet.Write("MX100.0", false);
            Assert.True(result.IsSuccess);
            var readResult = plcNet.ReadBool("MX100.0");
            Assert.False(readResult.Content);
        }

        [Fact(DisplayName = "���Զ�ȡbool")]
        public void ReadBoolTest()
        {
            IPlc plc = new InovanceTcpNetPlcImpl();
            IPlcNet plcNet = (plc as IPlcNet) ?? throw new Exception("�޷�ת��ΪIPlcNet");
            plcNet.IpAddress = "192.168.3.88";
            plcNet.Port = 502;
            plcNet.Connect();
            Assert.True(plcNet.IsConnected);
            var readResult = plcNet.ReadBool("MX100.0");
            if(readResult.IsFailed)
            {
                OutPutHelper.WriteLine("ErrorCode:" + readResult.ErrorCode);
                OutPutHelper.WriteLine("Message:" + readResult.Message);
            }
            Assert.True(readResult.IsSuccess);
        }
    }
}