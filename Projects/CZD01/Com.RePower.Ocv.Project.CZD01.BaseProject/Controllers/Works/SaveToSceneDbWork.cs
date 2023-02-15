using Com.RePower.Ocv.Model.Dto;
using Com.RePower.Ocv.Model.Helper;
using Com.RePower.WpfBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.CZD01.BaseProject.Controllers.Works
{
    public partial class MainWork
    {
        private OperateResult SaveToSceneDb()
        {
            LogHelper.UiLog.Info("保存到现场服务器数据库");

            {
                var dto = Mapper.Map<TrayDto>(Tray);

                var trays = SceneContext?.Trays.Where(x => x.TrayCode == Tray.TrayCode).Include(x => x.NgInfos).ThenInclude(x => x.Battery).ToList();
                if (trays == null || trays.Count() <= 0)
                {
                    SceneContext?.Trays.Add(dto);
                }
                else if (trays.Count() == 1)
                {
                    var temp = trays.First();
                    foreach (var item in dto.NgInfos)
                    {
                        temp.NgInfos.Add(item);
                    }
                    SceneContext?.Update(temp);
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
                        SceneContext?.Remove(trays[i]);
                    }
                    foreach (var item in dto.NgInfos)
                    {
                        temp.NgInfos.Add(item);
                    }
                    SceneContext?.Update(temp);
                }
                SceneContext?.SaveChanges();
            }
            return OperateResult.CreateSuccessResult();
        }
    }
}
