using Com.RePower.Ocv.Project.Cp06.Ocv0.Controllers;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Ui.Cp06.Ocv0.ViewModels
{
    public class SettingViewModel:ObservableObject
    {
        public SettingManager SettingManager => SettingManager.Instance;

		public bool IsOcv1OrOcv2
		{
			get
			{
				if(SettingManager.CurrentOcvType == Project.Cp06.Ocv0.Enums.OcvTypeEnmu.OCV1
					||SettingManager.CurrentOcvType == Project.Cp06.Ocv0.Enums.OcvTypeEnmu.OCV2)
				{
					return true;
				}
				return false;
			}
		}

	}
}
