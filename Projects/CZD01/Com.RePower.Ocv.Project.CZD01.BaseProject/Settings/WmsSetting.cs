using Com.RePower.Ocv.Model.Attributes;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.CZD01.BaseProject.Settings
{
    public class WmsSetting:ObservableObject
    {
        private string _getBatteryInfoUrl = "RequetEquipmentStationBingAsset";
        /// <summary>
        /// 获取托盘条码接口
        /// </summary>
        [SettingName("获取托盘条码接口")]
        public string GetBatteryInfoUrl
        {
            get { return _getBatteryInfoUrl; }
            set { SetProperty(ref _getBatteryInfoUrl, value); }
        }
        private string _uploadTestResultUrl = "UploadOCVTestResult";
        /// <summary>
        /// 上传测试结果接口
        /// </summary>
        [SettingName("上传测试结果接口")]
        public string UploadTestResultUrl
        {
            get { return _uploadTestResultUrl; }
            set { SetProperty(ref _uploadTestResultUrl, value); }
        }
        private string _requestAllocateCellToWmsUrl = "RequestAllocateCell";
        /// <summary>
        /// 请求总完成到wms
        /// </summary>
        [SettingName("请求总完成到wms")]
        [IgnorSetting]
        public string RequestAllocateCellToWmsUrl
        {
            get { return _requestAllocateCellToWmsUrl; }
            set { SetProperty(ref _requestAllocateCellToWmsUrl, value); }
        }
        private string _baseAddress = "http://172.17.20.6:8082/swagger/index.html/api/MERequest";
        /// <summary>
        /// 基础地址
        /// </summary>
        [SettingName("基础地址")]
        public string BaseAddress
        {
            get { return _baseAddress; }
            set { SetProperty(ref _baseAddress, value); }
        }

        private string _requestLocation = "FrontOcvStation";
        /// <summary>
        /// 工站号
        /// </summary>
        [SettingName("FrontOcvStation")]
        public string RequestLocation
        {
            get { return _requestLocation; }
            set { SetProperty(ref _requestLocation, value); }
        }
        private string _whCode = "F";
        /// <summary>
        /// 库房编号
        /// </summary>
        [SettingName("库房编号")]
        public string WhCode
        {
            get { return _whCode; }
            set { SetProperty(ref _whCode, value); }
        }
        private string? _location;
        /// <summary>
        /// 请求位置
        /// </summary>
        [SettingName("请求位置")]
        public string? Location
        {
            get { return _location; }
            set { SetProperty(ref _location, value); }
        }
        private string? _projectCode;
        /// <summary>
        /// 项目编号
        /// </summary>
        [SettingName("项目编号")]
        public string? ProjectCode
        {
            get { return _projectCode; }
            set { SetProperty(ref _projectCode, value); }
        }
    }
}
