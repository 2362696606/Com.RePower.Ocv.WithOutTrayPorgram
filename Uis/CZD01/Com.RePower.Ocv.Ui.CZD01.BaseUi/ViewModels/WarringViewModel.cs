using CommunityToolkit.Mvvm.ComponentModel;

namespace Com.RePower.Ocv.Ui.CZD01.BaseUi.ViewModels;

public class WarringViewModel:ObservableObject
{
    private string _warringInfo = "警告";

	public string WarringInfo
	{
		get => _warringInfo;
        set => SetProperty(ref _warringInfo, value);
    }


}