using Com.RePower.Ocv.Project.Byd.CB15.Services.Mes.Dtos;
using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Byd.CB15.Controllers.Works
{
    public partial class MainWork
    {
        private OperateResult SaveTestResultToCsv()
        {
            var time = DateTime.Now;
            string dayStr = time.ToString("yyyy_MM_dd");
            string timeStr = time.ToString("HH_mm");
            string dir = @$"./测试记录/{SettingManager.CurrentOcvType}/{dayStr}";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            string path = @$"./测试记录/{dayStr}/{Tray.TrayCode}_{SettingManager.CurrentOcvType.ToString()}_{timeStr}.csv";
            switch (SettingManager.CurrentOcvType)
            {
                case Enums.OcvTypeEnmu.OCV1:
                    return SaveTestDateToCsv<Ocv1InfoDto>(path);
                case Enums.OcvTypeEnmu.OCV2:
                    return SaveTestDateToCsv<Ocv2InfoDto>(path);
                case Enums.OcvTypeEnmu.OCV3:
                    return SaveTestDateToCsv<Ocv3InfoDto>(path);
                case Enums.OcvTypeEnmu.OCV4:
                    return SaveTestDateToCsv<Ocv4InfoDto>(path);
                default:
                    return SaveTestDateToCsv<Ocv1InfoDto>(path);
            }
        }
    }
}
