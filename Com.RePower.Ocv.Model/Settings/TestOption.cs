using Com.RePower.Ocv.Model.Attributes;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Model.Settings
{
    public partial class TestOption:ObservableObject
    {
        private bool _isTestVol;
        private bool _isTestRes;
        private bool _isTestPVol;
        private bool _isTestNVol;
        private bool _isTestPTemp;
        private bool _isTestNTemp;
        private bool _isDoRetest;
        private int _retestTimes;

        /// <summary>
        /// 是否测试电压
        /// </summary>
        [SettingName("是否测试电压")]
        public bool IsTestVol
        {
            get { return _isTestVol; }
            set { SetProperty(ref _isTestVol, value); }
        }

        /// <summary>
        /// 是否测试内阻
        /// </summary>
        [SettingName("是否测试内阻")]
        public bool IsTestRes
        {
            get { return _isTestRes; }
            set { SetProperty(ref _isTestRes, value); }
        }

        /// <summary>
        /// 是否测试正极壳电压
        /// </summary>
        [SettingName("是否测试正极壳电压")]
        public bool IsTestPVol
        {
            get { return _isTestPVol; }
            set { SetProperty(ref _isTestPVol, value); }
        }

        /// <summary>
        /// 是否测试负极壳电压
        /// </summary>
        [SettingName("是否测试负极壳电压")]
        public bool IsTestNVol
        {
            get { return _isTestNVol; }
            set { SetProperty(ref _isTestNVol, value); }
        }

        /// <summary>
        /// 是否测试正极温度
        /// </summary>
        [SettingName("是否测试正极温度")]
        public bool IsTestPTemp
        {
            get { return _isTestPTemp; }
            set { SetProperty(ref _isTestPTemp, value); }
        }

        /// <summary>
        /// 是否测试负极温度
        /// </summary>
        [SettingName("是否测试负极温度")]
        public bool IsTestNTemp
        {
            get { return _isTestNTemp; }
            set { SetProperty(ref _isTestNTemp, value); }
        }

        /// <summary>
        /// 是否复测
        /// </summary>
        [SettingName("是否复测")]
        public bool IsDoRetest
        {
            get { return _isDoRetest; }
            set { SetProperty(ref _isDoRetest, value); }
        }

        /// <summary>
        /// 复测次数
        /// </summary>
        [SettingName("复测次数")]
        public int RetestTimes
        {
            get { return _retestTimes; }
            set { SetProperty(ref _retestTimes, value); }
        }
    }
}
