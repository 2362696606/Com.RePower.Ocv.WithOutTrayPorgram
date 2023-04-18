using Newtonsoft.Json.Linq;

namespace Com.RePower.Ocv.Project.Byd.CB09.Models;

public class PlcAddressItem
{
    private readonly JToken _token;

    public PlcAddressItem(JToken token)
    {
        _token = token;
    }

    public string Name => _token["Name"]?.Value<string>() ?? throw new ArgumentNullException();
    public string Address => _token["Address"]?.Value<string>() ?? throw new ArgumentNullException();
    public string Description => _token["Description"]?.Value<string>()??throw new ArgumentNullException();
    public int Length => _token["Length"]?.Value<int>() ?? throw new ArgumentNullException();
    public TypeCode Type => Enum.Parse<TypeCode>(_token["Type"]?.Value<string>() ?? throw new ArgumentNullException());
    //Type.GetType(_token["Type"]?.Value<string>() ?? throw new ArgumentNullException()) ??
        //throw new ArgumentNullException();
}