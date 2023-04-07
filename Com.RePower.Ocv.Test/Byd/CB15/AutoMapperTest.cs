using AutoMapper;
using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Project.Byd.CB15.Services.Mes;
using Com.RePower.Ocv.Project.Byd.CB15.Services.Mes.Dtos;
using Xunit.Abstractions;

namespace Com.RePower.Ocv.Test.Byd.CB15
{
    public class AutoMapperTest
    {
        public AutoMapperTest(ITestOutputHelper testOutputHelper)
        {
            TestOutputHelper = testOutputHelper;
        }

        public ITestOutputHelper TestOutputHelper { get; }

        [Fact]
        public static void TestMapper()
        {
            var config = new MapperConfiguration(cfg => { cfg.AddProfile<MesDtoProfile>(); });
            var mapper = config.CreateMapper();
            var ngInfo = new NgInfo();
            ngInfo.Battery.VolValue = 100000000000.0000;
            ngInfo.Battery.Temp = 100000000000.0000;
            ngInfo.Battery.Res = 100000000000.0000;
            var mesValue = mapper.Map<Ocv4InfoDto>(ngInfo);
            Assert.Equal(mesValue.AcirRo, 100000);
        }
    }
}