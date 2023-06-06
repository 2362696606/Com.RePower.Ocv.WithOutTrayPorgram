namespace Com.RePower.Ocv.Project.CZD01.BaseProject.Messages;

public class DoMethodMessage
{
    public string MethodName { get; set; } = string.Empty;
    public Dictionary<string,object> Parameters { get; set; } = new();
}