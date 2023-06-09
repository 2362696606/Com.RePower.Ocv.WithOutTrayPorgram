using Com.RePower.Ocv.Project.Byd.CB09.Messages;
using Com.RePower.Ocv.Project.Byd.CB09.Settings;
using Com.RePower.WpfBase;
using CommunityToolkit.Mvvm.Messaging;
using Npoi.Mapper;
using System.Globalization;

namespace Com.RePower.Ocv.Project.Byd.CB09.Works;

public partial class MainWork
{
    public OperateResult DoMeasure()
    {
        try
        {
            var calibrationSetting = CalibrationSetting.Default;
            var connectedResult = ConnectDevices();
            if (connectedResult.IsFailed)
            {
                return connectedResult;
            }
            List<CalibrationExcelDto> dtos = new List<CalibrationExcelDto>();
            foreach (var item in calibrationSetting.CalibrationItems)
            {
                CalibrationExcelDto dto = new CalibrationExcelDto
                {
                    Channel = item.Channel.ToString(),
                    StandRes = item.StandRes.ToString(CultureInfo.InvariantCulture),
                    AutoCalibrationValue = item.AutoCalibrationValue.ToString(CultureInfo.InvariantCulture),
                    ManualCalibrationValue = item.ManualCalibrationValue.ToString(CultureInfo.InvariantCulture)
                };
                CloseAllChannel();
                int channel = item.Channel;
                OpenChannelByBattery(channel);
                var readResult = Ohm.ReadRes();
                if (readResult.IsFailed)
                {
                    return readResult;
                    //item.AutoCalibrationValue = 0;
                    //CalibrationChangedMessage param = new CalibrationChangedMessage()
                    //{
                    //    CalibrationItem = item,
                    //    ReadResValue = 0,
                    //    SubValue = 0
                    //};
                    //DoMethodMessage message = new DoMethodMessage()
                    //{
                    //    MethodName = "CalibrationChange",
                    //    Parameters = new Dictionary<string, object> { { "ChangeItem", param } },
                    //};
                    //WeakReferenceMessenger.Default.Send<DoMethodMessage, string>(message,
                    //    "CalibrationChangeMessage");
                }
                else
                {
                    var readValue = readResult.Content;
                    double subValue = (Model.Extensions.DoubleExtensions.Sub(item.StandRes, readValue) ??
                                       throw new Exception("计算异常"));
                    //double measureDev = Model.Extensions.DoubleExtensions.Div(
                    //                        Math.Abs(Model.Extensions.DoubleExtensions.Add(readValue,
                    //                            item.AutoCalibrationValue) ?? throw new Exception("计算异常")),
                    //                        item.StandRes) ??
                    //                    throw new Exception("计算异常");
                    double afterCalibrationValue;
                    if (calibrationSetting.IsUseAutoCalibration)
                    {
                        afterCalibrationValue = Model.Extensions.DoubleExtensions.Add(readValue, item.AutoCalibrationValue) ??
                                                throw new Exception("计算异常");
                    }
                    else
                    {
                        afterCalibrationValue = Model.Extensions.DoubleExtensions.Add(readValue, item.ManualCalibrationValue) ??
                                                throw new Exception("计算异常");
                    }
                        
                    double measureDev = Math.Abs(
                        Model.Extensions.DoubleExtensions.Sub(afterCalibrationValue, item.StandRes) ??
                        throw new Exception("计算异常"));
                    string measureResult = (measureDev < OtherSetting.Default.MaxMeasureDev) ? "Ok" : "Ng";
                    dto.ReadValue = readValue.ToString(CultureInfo.InvariantCulture);
                    dto.SubValue = subValue.ToString(CultureInfo.InvariantCulture);
                    dto.MeasureDev = measureDev.ToString(CultureInfo.InvariantCulture);
                    dto.AfterCalibrationValue = afterCalibrationValue.ToString(CultureInfo.InvariantCulture);
                    dto.MeasureResult = measureResult;
                    CalibrationChangedMessage param = new CalibrationChangedMessage()
                    {
                        CalibrationItem = item,
                        ReadResValue = readValue,
                        SubValue = subValue,
                        AfterCalibrationValue = afterCalibrationValue,
                        MeasureDev = measureDev,
                        MeasureResult = measureResult
                    };
                    DoMethodMessage message = new DoMethodMessage()
                    {
                        MethodName = "CalibrationChange",
                        Parameters = new Dictionary<string, object> { { "ChangeItem", param } },
                    };
                    WeakReferenceMessenger.Default.Send<DoMethodMessage, string>(message,
                        "CalibrationChangeMessage");
                }
                dtos.Add(dto);
            }
            Npoi.Mapper.Mapper mapper = new Npoi.Mapper.Mapper();
            string fileName = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".xlsx";
            //string fileName = DateTime.Now.ToString("yyyy/MM/dd-HH:mm:ss") + ".xlsx";
            string dirPath = Path.GetFullPath("./Master/Measure");
            string fullPath = Path.Combine(dirPath, fileName);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            mapper.Map<CalibrationExcelDto>("通道", s => s.Channel)
                //.Map<CalibrationExcelDto>("自动校准值", s => s.AutoCalibrationValue)
                //.Map<CalibrationExcelDto>("手动校准值", s => s.ManualCalibrationValue)
                .Map<CalibrationExcelDto>("标准值", s => s.StandRes)
                //.Map<CalibrationExcelDto>("读取值", s => s.ReadValue)
                .Map<CalibrationExcelDto>("校准后",s=>s.AfterCalibrationValue)
                //.Map<CalibrationExcelDto>("偏差值", s => s.SubValue)
                .Map<CalibrationExcelDto>("计量偏差", s => s.MeasureDev)
                .Map<CalibrationExcelDto>("计量结果", s => s.MeasureResult)
                .Ignore<CalibrationExcelDto>(s => s.AutoCalibrationValue)
                .Ignore<CalibrationExcelDto>(s=>s.SubValue)
                .Ignore<CalibrationExcelDto>(s=>s.ReadValue)
                .Ignore<CalibrationExcelDto>(s => s.ManualCalibrationValue);
            mapper.Save(fullPath, dtos, "sheet1");
            return OperateResult.CreateSuccessResult();
        }
        catch (Exception e)
        {
            return OperateResult.CreateFailedResult(e.ToString());
        }
    }
    public virtual bool CanDoMeasure()
    {
        if (this.WorkStatus == 0 && TestOption.Default.IsTestRes)
        {
            return true;
        }
        return false;
    }
}