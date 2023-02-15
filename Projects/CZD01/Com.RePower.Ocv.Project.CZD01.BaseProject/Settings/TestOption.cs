using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.CZD01.BaseProject.Settings
{
    public class TestOption:Com.RePower.Ocv.Model.Settings.TestOption
    {

        private bool _isVerifyVolDifference;
        /// <summary>
        /// 是否验证压差
        /// </summary>
        public bool IsVerifyVolDifference
        {
            get { return _isVerifyVolDifference; }
            set { SetProperty(ref _isVerifyVolDifference, value); }
        }
        private bool _isVerifyCurrentKValue;
        /// <summary>
        /// 是否验证单托盘Ng
        /// </summary>
        public bool IsVerifyCurrentKValue
        {
            get { return _isVerifyCurrentKValue; }
            set { SetProperty(ref _isVerifyCurrentKValue, value); }
        }

    }
}
