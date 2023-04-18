using Com.RePower.Ocv.Model.Dto;
using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Dto;
using Com.RePower.WpfBase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Mes
{
    public class MesSimulator : MesImpl
    {
        //public MesSimulator(Tray tray)
        //{
        //    Tray = tray;
        //}

        //public Tray Tray { get; }

        //public OperateResult<string> BatteryDismantlingDiskDataUploadToMes()
        //{
        //    throw new NotImplementedException();
        //}

        //public OperateResult<string> CapacitySortingNg()
        //{
        //    throw new NotImplementedException();
        //}

        //public OperateResult<string> FormingNg()
        //{
        //    throw new NotImplementedException();
        //}

        //public OperateResult<string> GetShopOrderList()
        //{
        //    var result = new MesGetShopOrderListResultDto();
        //    result.status = true;
        //    List<MesGetShopOrderListItem> resultList = new List<MesGetShopOrderListItem>();
        //    for (int i = 0; i < 3; i++) 
        //    {
        //        var resultItem = new MesGetShopOrderListItem();
        //        string orderRandom = string.Format("{0:D5}", new Random().Next(10000));
        //        resultItem.value = "OrderValue_" + orderRandom;
        //        resultItem.text = "OrderText_" + orderRandom;
        //        resultList.Add(resultItem);
        //    }
        //    result.result = resultList;
        //    string jStr = JsonConvert.SerializeObject(result);
        //    return OperateResult.CreateSuccessResult<string>(jStr);
        //}

        //public OperateResult<string> UploadHistoricalResult(List<NgInfoDto> ngInfos)
        //{
        //    throw new NotImplementedException();
        //}

        //public OperateResult<string> UploadingDeviceStatus(int status, bool isShutdown, string message = "")
        //{
        //    MesBatteryResultReturnDto mesBatteryResultReturnDto = new MesBatteryResultReturnDto
        //    {
        //        status = true,
        //    };
        //    string str = JsonConvert.SerializeObject(mesBatteryResultReturnDto);
        //    return OperateResult.CreateSuccessResult<string>(str);
        //}

        //public OperateResult<string> UploadResult()
        //{
        //    //MesBatteryResultReturnDto mesBatteryResultReturnDto = new MesBatteryResultReturnDto
        //    //{
        //    //    status = true,
        //    //};
        //    //string str = JsonConvert.SerializeObject(mesBatteryResultReturnDto);

        //    //List<MesBatteryRecovertDot> temp = new List<MesBatteryRecovertDot>();
        //    //temp.Add(new MesBatteryRecovertDot { sfcNO = Tray.NgInfos[5].Battery.BarCode, errMsg = "测试错误", result = "false" });
        //    //temp.Add(new MesBatteryRecovertDot { sfcNO = Tray.NgInfos[7].Battery.BarCode, errMsg = "测试错误", result = "false" });
        //    //var messageStr = JsonConvert.SerializeObject(temp);
        //    //MesBatteryResultReturnDto mesBatteryResultReturnDto = new MesBatteryResultReturnDto
        //    //{
        //    //    status = false,
        //    //    message = messageStr
        //    //};
        //    //string str = JsonConvert.SerializeObject(mesBatteryResultReturnDto);
        //    string str = @"{""status"":false,""message"":""[{\""sfcNo\"":\""\"",\""errMsg\"":\""电芯条码不能为空\"",\""result\"":\""pick\""}]"",""errorCode"":""warn""}";
        //    //string str = @"{""status"":false,""message"":""[{""sfcNo"":""02KCBB411280AGD361003544"",""trayNo"":""LZ1AP0002239"",""seq"":""5"",""errMsg"":""条码【02KCBB411280AGD361003544】被标记隔离【23-03-20001：负极片划痕】，请【排出后联系品质廖春文15277102428】！""}]"",""errorCode"":""warn""}";
        //    return OperateResult.CreateSuccessResult<string>(str);
        //}

        //public OperateResult<string> UserAuthentication(string userName, string password)
        //{
        //    throw new NotImplementedException();
        //}

        //public OperateResult<string> VerifyDataOcv1Ocv2TestCabinet()
        //{
        //    throw new NotImplementedException();
        //}
        public MesSimulator(IHttpClientFactory httpClientFactory, Tray tray) : base(httpClientFactory, tray)
        {
        }

        protected override OperateResult<string> Post(object obj, string url, string optionName)
        {
            if (url == MesSetting?.SfcTrayOnceUnbindUrl)
            {
                return OperateResult.CreateSuccessResult<string>("成功");
            }
            else if (url == MesSetting?.UploadMachineStatusUrl)
            {
                MesBatteryResultReturnDto mesBatteryResultReturnDto = new MesBatteryResultReturnDto
                {
                    Status = true,
                };
                string str = JsonConvert.SerializeObject(mesBatteryResultReturnDto);
                return OperateResult.CreateSuccessResult<string>(str);
            }
            else if (url == MesSetting?.SaveDataAutoUrl)
            {
                MesBatteryResultReturnDto mesBatteryResultReturnDto = new MesBatteryResultReturnDto
                {
                    Status = true,
                };
                string str = JsonConvert.SerializeObject(mesBatteryResultReturnDto);

                //List<MesBatteryRecovertDot> temp = new List<MesBatteryRecovertDot>();
                //temp.Add(new MesBatteryRecovertDot { sfcNO = Tray.NgInfos[5].Battery.BarCode, errMsg = "测试错误", result = "false" });
                //temp.Add(new MesBatteryRecovertDot { sfcNO = Tray.NgInfos[7].Battery.BarCode, errMsg = "测试错误", result = "false" });
                //var messageStr = JsonConvert.SerializeObject(temp);
                //MesBatteryResultReturnDto mesBatteryResultReturnDto = new MesBatteryResultReturnDto
                //{
                //    status = false,
                //    message = messageStr
                //};
                //string str = JsonConvert.SerializeObject(mesBatteryResultReturnDto);
                //string str = @"{""status"":false,""message"":""[{\""sfcNo\"":\""\"",\""errMsg\"":\""电芯条码不能为空\"",\""result\"":\""pick\""}]"",""errorCode"":""warn""}";
                //string str = @"{""status"":false,""message"":""[{""sfcNo"":""02KCBB411280AGD361003544"",""trayNo"":""LZ1AP0002239"",""seq"":""5"",""errMsg"":""条码【02KCBB411280AGD361003544】被标记隔离【23-03-20001：负极片划痕】，请【排出后联系品质廖春文15277102428】！""}]"",""errorCode"":""warn""}";
                return OperateResult.CreateSuccessResult<string>(str);
            }
            else if (url == MesSetting?.LoadShopOrderListUrl)
            {
                var result = new MesGetShopOrderListResultDto();
                result.status = true;
                List<MesGetShopOrderListItem> resultList = new List<MesGetShopOrderListItem>();
                for (int i = 0; i < 3; i++)
                {
                    var resultItem = new MesGetShopOrderListItem();
                    string orderRandom = string.Format("{0:D5}", new Random().Next(10000));
                    resultItem.value = "OrderValue_" + orderRandom;
                    resultItem.text = "OrderText_" + orderRandom;
                    resultList.Add(resultItem);
                }
                result.result = resultList;
                string jStr = JsonConvert.SerializeObject(result);
                return OperateResult.CreateSuccessResult<string>(jStr);
            }
            else
            {
                return OperateResult.CreateFailedResult<string>("失败");
            }
        }
    }
}
