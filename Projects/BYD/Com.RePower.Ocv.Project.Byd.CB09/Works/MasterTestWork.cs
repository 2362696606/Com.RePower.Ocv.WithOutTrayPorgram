using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using Com.RePower.Device.Plc.Impl;
using Com.RePower.Ocv.Project.Byd.CB09.Messages;
using Com.RePower.Ocv.Project.Byd.CB09.Settings;
using Com.RePower.WpfBase;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Npoi.Mapper;

namespace Com.RePower.Ocv.Project.Byd.CB09.Works;

public partial class MainWork
{

    public virtual void DoMasterTest()
    {
        var calibrationSetting = CalibrationSetting.Default;
        var connectedResult = ConnectDevices();
        if (connectedResult.IsFailed)
        {
            return;
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
                item.AutoCalibrationValue = 0;
                CalibrationChangedMessage param = new CalibrationChangedMessage()
                {
                    CalibrationItem = item,
                    ReadResValue = 0,
                    SubValue = 0
                };
                DoMethodMessage message = new DoMethodMessage()
                {
                    MethodName = "CalibrationChange",
                    Parameters = new Dictionary<string, object> { { "ChangeItem", param } },
                };
                WeakReferenceMessenger.Default.Send<DoMethodMessage, string>(message,
                    "CalibrationChangeMessage");
            }
            else
            {
                var readValue = readResult.Content;
                double subValue = (Com.RePower.Ocv.Model.Extensions.DoubleExtensions.Sub(item.StandRes, readValue) ??
                                   throw new Exception("计算异常"));
                dto.ReadValue = readValue.ToString(CultureInfo.InvariantCulture);
                dto.SubValue = subValue.ToString(CultureInfo.InvariantCulture);
                item.AutoCalibrationValue = subValue;
                calibrationSetting.SaveChanged();
                CalibrationChangedMessage param = new CalibrationChangedMessage()
                {
                    CalibrationItem = item,
                    ReadResValue = readValue,
                    SubValue = subValue,
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
        string dirPath = Path.GetFullPath("./Master");
        string fullPath = Path.Combine(dirPath, fileName);
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }

        mapper.Map<CalibrationExcelDto>("通道", s => s.Channel)
            .Map<CalibrationExcelDto>("自动校准值", s => s.AutoCalibrationValue)
            .Map<CalibrationExcelDto>("手动校准值", s => s.ManualCalibrationValue)
            .Map<CalibrationExcelDto>("标准值", s => s.StandRes)
            .Map<CalibrationExcelDto>("读取值", s => s.ReadValue)
            .Map<CalibrationExcelDto>("偏差值", s => s.SubValue);
        mapper.Save(fullPath, dtos, "sheet1", true, true);
    }
    public virtual bool CanDoMasterTest()
    {
        if (this.WorkStatus == 0 && TestOption.Default.IsTestRes)
        {
            return true;
        }
        return false;
    }
}

public class CalibrationExcelDto
{
    public string Channel { get; set; } = "UnKnown";
    public string AutoCalibrationValue { get; set; } = "UnKnown";
    public string ManualCalibrationValue { get; set; } = "UnKnown";
    public string StandRes { get; set; } = "UnKnown";
    public string ReadValue { get; set; } = "UnKnown";
    public string SubValue { get; set; } = "UnKnown";
}