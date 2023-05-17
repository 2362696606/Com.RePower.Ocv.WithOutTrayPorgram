using Newtonsoft.Json.Linq;

namespace Com.RePower.Ocv.Project.Byd.CB09.Models;

public class ChannelsNgInfo
{
    private readonly JToken _token;

    /// <summary>
    /// 板号
    /// </summary>
    public int BoardIndex { get; set; }
    /// <summary>
    /// 通道Ng信息
    /// </summary>
    public List<ChannelNgInfo> Nginfos { get; }

    public ChannelsNgInfo(JToken token)
    {
        _token = token;
        BoardIndex = _token[nameof(BoardIndex)]?.Value<int>()??throw new ArgumentNullException(nameof(BoardIndex));
        Nginfos = _token[nameof(Nginfos)]?.ToObject<List<ChannelNgInfo>>() ??
                  throw new ArgumentNullException(nameof(Nginfos));
    }

}