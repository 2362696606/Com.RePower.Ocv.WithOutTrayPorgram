using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Model.Helper;
using Com.RePower.Ocv.Project.Byd.CB09.Serivces.DbContexts;
using Com.RePower.Ocv.Project.Byd.CB09.Serivces.Dtos;
using Com.RePower.Ocv.Project.Byd.CB09.Settings;
using Com.RePower.WpfBase;

namespace Com.RePower.Ocv.Project.Byd.CB09.Serivces.Impl;

public class MesService:IMesService
{
    private readonly Tray _tray;

    public MesService(Tray tray)
    {
        _tray = tray;
    }
    public OperateResult<string> UploadTestResult()
    {
        using var dbContext = new OcvDataDbContext();
        OperateResult result = OperateResult.CreateFailedResult();
        try
        {
            List<RnDbOcv> listrnDbs = new List<RnDbOcv>();
            foreach (var item in _tray.NgInfos)
            {
                RnDbOcv rnDbOcv = new RnDbOcv
                {
                    EqpId = "rn_" + SystemSetting.Default.DefaultOcvType + "_6", //设备编码   //EquipmentCode;
                    PcId = SystemSetting.Default.DefaultOcvType + "_6", //设备号+线
                    Operation = SystemSetting.Default.DefaultOcvType.ToString() //设备号
                };
                if (string.IsNullOrWhiteSpace(_tray.TrayCode))
                {
                    LogHelper.UiLog.Error("托盘条码为空！");
                    result.Message = "托盘条码为空!";
                    return (OperateResult<string>)result;
                }
                rnDbOcv.TrayId = _tray.TrayCode;//托盘号
                rnDbOcv.IsTrans = 1;//是否跨线
                rnDbOcv.ModelNo = SystemSetting.Default.DefaultOcvType + "_6";//设备号+线
                rnDbOcv.TotalNgState = _tray.NgInfos.Any(x => x.IsNg) ? "NG" : "OK";
                rnDbOcv.CellId = item.Battery.BarCode;
                rnDbOcv.OcvVoltage = Convert.ToDecimal(item.Battery.VolValue);//电池电压
                rnDbOcv.TestResult = item.IsNg ? "NG" : "OK";
                rnDbOcv.TestResultDesc = item.NgDescription;
                rnDbOcv.BatteryPos = item.Battery.Position;
                rnDbOcv.EndDateTime = item.Battery.TestTime;
                rnDbOcv.InsertTime = DateTime.Now;
                rnDbOcv.TestMode = "自动";
                rnDbOcv.ShellVoltage = Convert.ToDecimal(item.Battery.NVolValue);
                rnDbOcv.SvResult = item.IsNg ? "NG" : "OK";
                switch (item.NgType)
                {
                    case 0:
                        rnDbOcv.SvNgCode = "00";
                        break;

                    case 1:
                        rnDbOcv.SvNgCode = "C1";
                        break;

                    case 2:
                        rnDbOcv.SvNgCode = "C2";
                        break;
                }
                listrnDbs.Add(rnDbOcv);
            }
            using (var context = new OcvDataDbContext())
            {
                foreach (var item in listrnDbs)
                {
                    context.RnDbOcv0.Add(item);
                }
                context.SaveChanges();
            }
            result.IsSuccess = true;
            return (OperateResult<string>)result;
        }
        catch (Exception ex)
        {
            LogHelper.UiLog.Error(ex.Message);
            result.Message = ex.Message;
            return (OperateResult<string>)result;
        }
    }
}