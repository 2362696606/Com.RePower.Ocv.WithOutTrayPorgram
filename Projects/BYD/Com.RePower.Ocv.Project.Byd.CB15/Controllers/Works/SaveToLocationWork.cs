using Com.RePower.Ocv.Model.DataBaseContext;
using Com.RePower.Ocv.Model.Dto;
using Com.RePower.Ocv.Model.Helper;
using Com.RePower.WpfBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Byd.CB15.Controllers.Works
{
    public partial class MainWork
    {

        /// <summary>
        /// 保存到本地数据库
        /// </summary>
        /// <returns></returns>
        private OperateResult SaveToLocation()
        {
            LogHelper.UiLog.Info("保存到本地数据库");
            using (var localContext = new LocalTestResultDbContext())
            {
                var dto = Mapper.Map<TrayDto>(Tray);

                var trays = localContext.Trays.Where(x => x.TrayCode == Tray.TrayCode).Include(x => x.NgInfos).ThenInclude(x => x.Battery).ToList();
                if (trays == null || trays.Count() <= 0)
                {
                    localContext.Trays.Add(dto);
                }
                else if (trays.Count() == 1)
                {
                    var temp = trays.First();
                    foreach (var item in dto.NgInfos)
                    {
                        temp.NgInfos.Add(item);
                    }
                    localContext.Update(temp);
                }
                else
                {
                    var temp = trays.First();
                    int count = trays.Count();
                    for (int i = 1; i < count; i++)
                    {
                        foreach (var item in trays[i].NgInfos)
                        {
                            temp.NgInfos.Add(item);
                        }
                        localContext.Remove(trays[i]);
                    }
                    foreach (var item in dto.NgInfos)
                    {
                        temp.NgInfos.Add(item);
                    }
                    localContext.Update(temp);
                }
                localContext.SaveChanges();
            }
            return OperateResult.CreateSuccessResult();
        }
    }
}
