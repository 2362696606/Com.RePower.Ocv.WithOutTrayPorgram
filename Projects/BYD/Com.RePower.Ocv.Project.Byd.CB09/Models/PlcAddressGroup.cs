using Newtonsoft.Json.Linq;

namespace Com.RePower.Ocv.Project.Byd.CB09.Models;

public class PlcAddressGroup
{
    private readonly JToken _token;

    public PlcAddressGroup(JToken token)
    {
        this._token = token;
    }

    public PlcAddressItem this[string itemName]
    {
        get
        {
            var items = _token["Items"] as JArray;
            var item = items?.First(x => x["Name"]?.Value<string>() == itemName);
            return  new PlcAddressItem(item??throw new ArgumentNullException($"未找到{itemName}"));
        }
    }
}