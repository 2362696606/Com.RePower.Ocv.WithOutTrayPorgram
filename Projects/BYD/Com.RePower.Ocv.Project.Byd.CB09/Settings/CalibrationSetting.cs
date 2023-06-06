using Com.RePower.Ocv.Project.Byd.CB09.Models;
using Newtonsoft.Json;
using System.Text;
using Org.BouncyCastle.Asn1.Crmf;

namespace Com.RePower.Ocv.Project.Byd.CB09.Settings;

public class CalibrationSetting
{
    [JsonIgnore]
    public static CalibrationSetting Default { get; } =
        new Lazy<CalibrationSetting>(() => new CalibrationSetting()).Value;

    private readonly string _filePath;

    public List<CalibrationItem> CalibrationItems { get; set; } = new();
    public bool IsUseCalibration { get; set; }
    public bool IsUseAutoCalibration { get; set; }

    public CalibrationItem this[int channel]
    {
        get
        {
            var item = CalibrationItems.FirstOrDefault(x => x.Channel == channel);
            if (item == null)
            {
                var temp = new CalibrationItem() { Channel = channel };
                CalibrationItems.Add(temp);
                SaveChanged();
                return temp;
            }
            else
            {
                return item;
            }
        }
    }

    public CalibrationSetting(string filePath)
    {
        _filePath = Path.GetFullPath(filePath);
        if (File.Exists(_filePath))
        {
            string jsonStr = File.ReadAllText(filePath);
            CalibrationSettingDto tempObj = JsonConvert.DeserializeObject<CalibrationSettingDto>(jsonStr) ??
                                            throw new ArgumentNullException();
            Dictionary<int, CalibrationItem> dic = new Dictionary<int, CalibrationItem>();
            foreach (var tempItem in tempObj.CalibrationItems)
            {
                //var dic = tempObj.CalibrationItems.ToDictionary(x => x.Channel);
                dic.TryAdd(tempItem.Channel, tempItem);
            }
            this.CalibrationItems = dic.Values.ToList();
            this.IsUseAutoCalibration = tempObj.IsUseAutoCalibration;
            this.IsUseCalibration = tempObj.IsUseCalibration;
        }
        else
        {
            SaveChanged();
        }
    }

    public CalibrationSetting():this("./Configs/CalibrationSetting.json")
    {
        
    }
    public void SaveChanged()
    {
        StringWriter textWriter = new StringWriter();
        JsonWriter writer = new JsonTextWriter(textWriter)
        {
            Formatting = Formatting.Indented,
            Indentation = 4,
            IndentChar = ' '
        };
        JsonSerializer serializer = new JsonSerializer();
        CalibrationSettingDto dto = new CalibrationSettingDto
        {
            CalibrationItems = this.CalibrationItems,
            IsUseAutoCalibration = this.IsUseAutoCalibration,
            IsUseCalibration = this.IsUseCalibration
        };
        serializer.Serialize(writer, dto);
        File.WriteAllText(_filePath, textWriter.ToString(), Encoding.UTF8);
        textWriter.Dispose();
    }

    public class CalibrationSettingDto
    {
        public List<CalibrationItem> CalibrationItems { get; set; } = new List<CalibrationItem>();
        public bool IsUseCalibration { get; set; }
        public bool IsUseAutoCalibration { get; set; }
    }
}