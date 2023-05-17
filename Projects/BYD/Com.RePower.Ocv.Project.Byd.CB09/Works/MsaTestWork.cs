using Com.RePower.Ocv.Model.Helper;
using Com.RePower.Ocv.Project.Byd.CB09.Serivces.Dtos;
using Com.RePower.Ocv.Project.Byd.CB09.Settings;
using Com.RePower.WpfBase;
using Npoi.Mapper;

namespace Com.RePower.Ocv.Project.Byd.CB09.Works;

public partial class MainWork
{
    protected virtual OperateResult MsaTest()
    {
        #region 测试电池
        LogHelper.UiLog.Info("测试电池");
        var testResult = TestBatteries();
        if (testResult.IsFailed)
            return testResult;
        LogHelper.UiLog.Info("测试电池成功");
        DoPauseOrStop();
        #endregion
        #region 验证Ng信息
        LogHelper.UiLog.Info("验证Ng信息");
        var verifyResult = VerifyNg();
        if (verifyResult.IsFailed)
            return verifyResult;
        LogHelper.UiLog.Info("验证Ng信息成功");
        #endregion
        #region 保存到excel
        var saveResult = SaveToExcelForMsa();
        if (saveResult.IsFailed)
            return saveResult; 
        #endregion
        int i = 0;
        while (i < TestOption.Default.MsaTestTimes)
        {
            //#region 复测
            //var retestResult = ReTest();
            //if (retestResult.IsFailed)
            //    return retestResult;
            //#endregion
            var sendRetestSingalResult = SendRetestSignal();
            if (sendRetestSingalResult.IsFailed)
                return sendRetestSingalResult;
            LogHelper.UiLog.Info("等待测试请求");
            var waitResult = WaitTestRequest();
            if (waitResult.IsFailed)
                return waitResult;
            LogHelper.UiLog.Info("等待测试请求成功");
            LogHelper.UiLog.Info("测试Ng电池");
            var ngBatteries = Tray.NgInfos.Where(x => x.IsNg);
            foreach (var ngBattery in ngBatteries)
            {
                testResult = TestOneBattery(ngBattery);
                if (testResult.IsFailed)
                    return testResult;
            }
            LogHelper.UiLog.Info("测试Ng电池成功");
            LogHelper.UiLog.Info("验证Ng信息");
            verifyResult = VerifyNg();
            if (verifyResult.IsFailed)
                return verifyResult;
            LogHelper.UiLog.Info("验证Ng信息成功");
            #region 保存到excel
            saveResult = SaveToExcelForMsa();
            if (saveResult.IsFailed)
                return saveResult;
            #endregion
            i++;
        }
        //下发测试完成
        var sendResult = SendTestComplete();
        if (sendResult.IsFailed) return sendResult;
        IsMsaTest = false;
        return OperateResult.CreateSuccessResult();
    }

    protected OperateResult SaveToExcelForMsa()
    {
        List<ExcelSaveDto> saveDtos = new List<ExcelSaveDto>();
        foreach (var item in Tray.NgInfos)
        {
            ExcelSaveDto temp = new ExcelSaveDto
            {
                Position = item.Battery.Position,
                BarCode = item.Battery.BarCode,
                NVolValue = item.Battery.NVolValue ?? 0,
                NgResult = item.IsNg ? "Ng" : "OK",
                NgDescription = item.NgDescription ?? string.Empty
            };
            saveDtos.Add(temp);
        }

        var exMapper = new Mapper();
        exMapper.Map<ExcelSaveDto>("位置", n => n.Position)
            .Map<ExcelSaveDto>("电芯条码", n => n.BarCode)
            .Map<ExcelSaveDto>("负极壳体电压", n => n.NVolValue)
            .Map<ExcelSaveDto>("测试结果", n => n.NgResult)
            .Map<ExcelSaveDto>("Ng原因", n => n.NgDescription);
        string dir = @$"./正常数据文件夹_normal/{DateTime.Now:yyyy-MM-dd}";
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
        exMapper.Save(@$"{dir}/{Tray.TrayCode}.xlsx", saveDtos, "sheet1", overwrite: true, xlsx: true);
        return OperateResult.CreateSuccessResult();
    }
}