namespace Com.RePower.Ocv.Project.Byd.CB09.Messages;

public class DoMethodMessage
{
    /// <summary>
    /// 方法名
    /// </summary>
    public string MethodName { get; set; } = string.Empty;
    /// <summary>
    /// 参数
    /// </summary>
    public Dictionary<string, object> Parameters { get; set; } = new();

}