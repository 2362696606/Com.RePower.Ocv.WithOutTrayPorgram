using System.Collections;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Com.RePower.Ocv.Project.Byd.CB09.Models;

namespace Com.RePower.Ocv.Project.Byd.CB09.Settings;
[SettingsProvider(typeof(CustomSettingsProvider))]
public class SwitchBoardChannelSetting : IEnumerable<SwitchBoardChannelConfig>
{
    public static SwitchBoardChannelSetting Default { get; } =
        new Lazy<SwitchBoardChannelSetting>(() => new SwitchBoardChannelSetting()).Value;

    private readonly JArray _config;

    public SwitchBoardChannelSetting() : this("./Configs/SwitchBoardChannelSetting.json")
    {
    }
    public SwitchBoardChannelSetting(string filePath)
    {
        string path = Path.GetFullPath(filePath);
        JsonReader reader = new JsonTextReader(new StreamReader(path));
        _config = JArray.Load(reader);
    }
    public SwitchBoardChannelConfig this[int boardIndex]
    {
        get
        {
            var config = _config.First(x => x["BoardIndex"]?.Value<int>() == boardIndex);
            return new SwitchBoardChannelConfig(config);
        }
    }

    public IEnumerator<SwitchBoardChannelConfig> GetEnumerator()
    {
        List<SwitchBoardChannelConfig> result = new List<SwitchBoardChannelConfig>();
        foreach (var item in _config)
        {
            result.Add(new SwitchBoardChannelConfig(item));
        }
        return result.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
public class SwitchBoardChannelConfig
{
    private readonly JToken _token;

    public SwitchBoardChannelConfig(JToken token)
    {
        _token = token;
    }

    /// <summary>
    /// 板号
    /// </summary>
    public int BoardIndex => _token[nameof(BoardIndex)]?.Value<int>() ?? throw new ArgumentNullException();

    /// <summary>
    /// 对应电池列表
    /// </summary>
    public Dictionary<int, int> Correspondence => _token[nameof(Correspondence)]?.ToObject<Dictionary<int, int>>() ?? throw new ArgumentNullException();
    /// <summary>
    /// 是否需要附加通道
    /// </summary>
    public bool IsNeedAttached => _token[nameof(IsNeedAttached)]?.Value<bool>() ?? throw new ArgumentNullException();

    /// <summary>
    /// 正负极附加通道
    /// </summary>
    public List<int> VolAttachedChannels =>
        _token[nameof(VolAttachedChannels)]?.ToObject<List<int>>() ?? throw new ArgumentNullException();

    /// <summary>
    /// 正对壳附加通道
    /// </summary>
    public List<int> PVolAttachedChannels =>
        _token[nameof(PVolAttachedChannels)]?.ToObject<List<int>>() ?? throw new ArgumentNullException();
    /// <summary>
    /// 负对壳附加通道
    /// </summary>
    public List<int> NVolAttachedChannels => _token[nameof(NVolAttachedChannels)]?.ToObject<List<int>>() ?? throw new InvalidOperationException();
}