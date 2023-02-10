using Com.RePower.Ocv.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Ui.CZD01.BaseUi.ViewModels
{
    public class ControlViewModel
    {
        public ControlViewModel(IProjectMainWork mainWork)
        {
            MainWork = mainWork;
        }

        public IProjectMainWork MainWork { get; }
    }
}
