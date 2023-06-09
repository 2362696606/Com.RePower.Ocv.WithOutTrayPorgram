using Com.RePower.WpfBase;
using Com.RePower.Ocv.Project.Byd.CB09.Serivces.Dtos;
using Npoi.Mapper;
using Com.RePower.Ocv.Model.DataBaseContext;
using Com.RePower.Ocv.Model.Dto;
using Com.RePower.Ocv.Model.Helper;
using Microsoft.EntityFrameworkCore;

namespace Com.RePower.Ocv.Project.Byd.CB09.Works;

public partial class MainWork
{
    protected OperateResult UploadTestResult()
    {
        var saveResult = SaveToExcel();
        if (saveResult.IsFailed)
            return saveResult;
        saveResult = SaveToLocalDataBase();
        if(saveResult.IsFailed)
            return saveResult;
        var uploadToMesResult = UploadToMes();
        if (uploadToMesResult.IsFailed)
            return uploadToMesResult;
        var uploadToWmsResult = UploadToWms();
        if(uploadToWmsResult.IsFailed)
            return uploadToWmsResult;
        return OperateResult.CreateSuccessResult();
    }

    protected OperateResult SaveToExcel()
    {
        List<ExcelSaveDto> saveDtos = new List<ExcelSaveDto>();
        foreach (var item in Tray.NgInfos)
        {
            ExcelSaveDto temp = new ExcelSaveDto
            {
                Position = item.Battery.Position,
                BarCode = item.Battery.BarCode,
                Vol = item.Battery.VolValue??0,
                Res = item.Battery.Res??0,
                PVolValue = item.Battery.PVolValue ?? 0,
                NVolValue = item.Battery.NVolValue ?? 0,
                Temp = item.Battery.Temp ?? 0,
                NgResult = item.IsNg ? "Ng" : "OK",
                NgDescription = item.NgDescription ?? string.Empty
            };
            saveDtos.Add(temp);
        }

        var exMapper = new Mapper();
        exMapper.Map<ExcelSaveDto>("位置", n => n.Position)
            .Map<ExcelSaveDto>("电芯条码", n => n.BarCode)
            .Map<ExcelSaveDto>("电压",n=>n.Vol)
            .Map<ExcelSaveDto>("内阻",n=>n.Res)
            .Map<ExcelSaveDto>("正极壳体电压",n=>n.PVolValue)
            .Map<ExcelSaveDto>("负极壳体电压", n => n.NVolValue)
            .Map<ExcelSaveDto>("温度",n=>n.Temp)
            .Map<ExcelSaveDto>("测试结果", n => n.NgResult)
            .Map<ExcelSaveDto>("Ng原因", n => n.NgDescription);
        string dir = @$"./正常数据文件夹_normal";
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
        exMapper.Save(@$"./正常数据文件夹_normal/{Tray.TrayCode}.xlsx", saveDtos, "sheet1", overwrite: true, xlsx: true);
        return OperateResult.CreateSuccessResult();
    }
    protected OperateResult SaveToLocalDataBase()
    {
        LogHelper.UiLog.Info("保存到本地数据库");
        using (var localContext = new LocalTestResultDbContext())
        {
            var dto = Mapper.Map<TrayDto>(Tray);

            var trays = localContext.Trays.Where(x => x.TrayCode == Tray.TrayCode).Include(x => x.NgInfos).ThenInclude(x => x.Battery).ToList();
            if (!trays.Any())
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
    protected OperateResult UploadToMes()
    {
        LogHelper.UiLog.Info("上传到Mes");
        var uploadResult = MesService.UploadTestResult();
        if (uploadResult.IsSuccess)
            LogHelper.UiLog.Info("上传到Mes成功");
        return uploadResult;
    }

    protected OperateResult UploadToWms()
    {
        LogHelper.UiLog.Info("上传到Wms");
        var uploadResult = WmsService.UploadTestResult();
        if(uploadResult.IsSuccess)
            LogHelper.UiLog.Info("上传到Wms成功");
        return uploadResult;
    }
}