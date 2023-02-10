using Com.RePower.Ocv.Model.Attributes;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Model.Settings
{
    public partial class BatteryStandard : ObservableObject
    {
        private double _maxVol;
        private double _minVol;
        private double _maxRes;
        private double _minRes;
        private double _maxNVol;
        private double _minNvol;
        private double _maxPVol;
        private double _minPVol;
        private double _maxNTemp;
        private double _minNTemp;
        private double _maxPTemp;
        private double _minPTemp;
        private double _maxKValue;
        private double _minKValue;
        /// <summary>
        /// 最大电压
        /// </summary>
        [SettingName("最大电压")]
        public double MaxVol
        {
            get { return _maxVol; }
            set { SetProperty(ref _maxVol, value); }
        }

        /// <summary>
        /// 最小电压
        /// </summary>
        [SettingName("最小电压")]
        public double MinVol
        {
            get { return _minVol; }
            set { SetProperty(ref _minVol, value); }
        }

        /// <summary>
        /// 最大内阻
        /// </summary>
        [SettingName("最大内阻")]
        public double MaxRes
        {
            get { return _maxRes; }
            set { SetProperty(ref _maxRes, value); }
        }

        /// <summary>
        /// 最小内阻
        /// </summary>
        [SettingName("最小内阻")]
        public double MinRes
        {
            get { return _minRes; }
            set { SetProperty(ref _minRes, value); }
        }

        /// <summary>
        /// 最大负极壳体电压
        /// </summary>
        [SettingName("最大负极壳体电压")]
        public double MaxNVol
        {
            get { return _maxNVol; }
            set { SetProperty(ref _maxNVol, value); }
        }

        /// <summary>
        /// 最小负极壳体电压
        /// </summary>
        [SettingName("最小负极壳体电压")]
        public double MinNVol
        {
            get { return _minNvol; }
            set { SetProperty(ref _minNvol, value); }
        }

        /// <summary>
        /// 最大正极壳体电压
        /// </summary>
        [SettingName("最大正极壳体电压")]
        public double MaxPVol
        {
            get { return _maxPVol; }
            set { SetProperty(ref _maxPVol, value); }
        }

        /// <summary>
        /// 最小正极壳体电压
        /// </summary>
        [SettingName("最小正极壳体电压")]
        public double MinPVol
        {
            get { return _minPVol; }
            set { SetProperty(ref _minPVol, value); }
        }

        /// <summary>
        /// 最大负极温度
        /// </summary>
        [SettingName("最大负极温度")]
        public double MaxNTemp
        {
            get { return _maxNTemp; }
            set { SetProperty(ref _maxNTemp, value); }
        }

        /// <summary>
        /// 最小负极温度
        /// </summary>
        [SettingName("最小负极温度")]
        public double MinNTemp
        {
            get { return _minNTemp; }
            set { SetProperty(ref _minNTemp, value); }
        }

        /// <summary>
        /// 最大正极温度
        /// </summary>
        [SettingName("最大正极温度")]
        public double MaxPTemp
        {
            get { return _maxPTemp; }
            set { SetProperty(ref _maxPTemp, value); }
        }

        /// <summary>
        /// 最小正极温度
        /// </summary>
        [SettingName("最小正极温度")]
        public double MinPTemp
        {
            get { return _minPTemp; }
            set { SetProperty(ref _minPTemp, value); }
        }

        /// <summary>
        /// 最大K值
        /// </summary>
        [SettingName("最大K值")]
        public double MaxKValue
        {
            get { return _maxKValue; }
            set { SetProperty(ref _maxKValue, value); }
        }

        /// <summary>
        /// 最小K值
        /// </summary>
        [SettingName("最小K值")]
        public double MinKValue
        {
            get { return _minKValue; }
            set { SetProperty(ref _minKValue, value); }
        }

    }
}
