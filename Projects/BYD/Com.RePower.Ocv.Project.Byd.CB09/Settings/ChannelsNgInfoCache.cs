using System.Collections;
using System.Threading.Channels;
using Com.RePower.Ocv.Project.Byd.CB09.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using static Org.BouncyCastle.Math.EC.ECCurve;
using System.IO;
using System.Net.Http.Json;
using System.Text;

namespace Com.RePower.Ocv.Project.Byd.CB09.Settings;

public class ChannelsNgInfoCache
{
    public static ChannelsNgInfoCache Default { get; } = new Lazy<ChannelsNgInfoCache>(() => new ChannelsNgInfoCache()).Value;

    private readonly string _filePath;

    private readonly JArray _config;

    public List<ChannelsNgInfo> ChannelsGroups { get; }

    public ChannelsNgInfoCache(string filePath)
    {
        _filePath = Path.GetFullPath(filePath);
        JsonReader reader = new JsonTextReader(new StreamReader(_filePath));
        _config = JArray.Load(reader);
        ChannelsGroups = new List<ChannelsNgInfo>();
        foreach (var item in _config)
        {
            ChannelsGroups.Add(new ChannelsNgInfo(item));
        }
        reader.Close();
    }

    public ChannelsNgInfoCache() : this("./Configs/ChannelsNgInfo.json")
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
        serializer.Serialize(writer,ChannelsGroups);
        File.WriteAllText(_filePath, textWriter.ToString(), Encoding.UTF8);
        textWriter.Dispose();
    }
}