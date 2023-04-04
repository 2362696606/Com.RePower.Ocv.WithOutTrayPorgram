using Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Dto;
using Com.RePower.WpfBase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Controllers.Works
{
    public partial class MainWork
    {
        private OperateResult GetShopOrderList()
        {
            var getResult = MesService.GetShopOrderList();
            if(getResult.IsFailed) return getResult;
            if(!string.IsNullOrEmpty(getResult.Content))
            {
                MesGetShopOrderListResultDto? resultDto = JsonConvert.DeserializeObject<MesGetShopOrderListResultDto>(getResult.Content);
                if(resultDto is { })
                {
                    if(!resultDto.Status)
                    {
                        return OperateResult.CreateFailedResult($"获取工单列表Mes返回false,原因{resultDto.Message ?? "未知原因"}");
                    }
                    else
                    {
                        Dictionary<string,string> resultDic = new Dictionary<string,string>();
                        foreach(var item in resultDto.Result)
                        {
                            resultDic.Add(item.Value, item.Value);
                        }
                        SettingManager.OrderList = resultDic;
                    }
                }
                else
                {
                    return OperateResult.CreateFailedResult("获取工单列表时Json解析失败");
                }
            }
            else
            {
                return OperateResult.CreateFailedResult("Mes返回结果为null");
            }
            return OperateResult.CreateSuccessResult();
        }
    }
}
