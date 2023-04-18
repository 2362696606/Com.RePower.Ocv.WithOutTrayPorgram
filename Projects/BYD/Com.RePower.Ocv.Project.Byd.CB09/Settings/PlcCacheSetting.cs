using Com.RePower.Ocv.Project.Byd.CB09.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Com.RePower.Ocv.Project.Byd.CB09.Settings;

public class PlcCacheSetting
{
    public static PlcCacheSetting Default { get; } = new Lazy<PlcCacheSetting>(() => new PlcCacheSetting()).Value;

    private readonly JArray _config;

    public PlcCacheSetting(string filePath)
    {
        string path = Path.GetFullPath(filePath);
        JsonReader reader = new JsonTextReader(new StreamReader(path));
        _config = JArray.Load(reader);
    }

    public PlcCacheSetting() : this("./Configs/PlcAddress.json")
    {
    }

    public PlcAddressGroup this[string groupName] 
    {
        get
        {
            var group = _config.First(x => x["Name"]?.Value<string>() == groupName);
            return new PlcAddressGroup(group);
        }
    }
}