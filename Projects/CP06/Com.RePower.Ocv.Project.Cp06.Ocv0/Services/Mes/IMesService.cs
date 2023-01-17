﻿using Com.RePower.Ocv.Model.Dto;
using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Mes
{
    public interface IMesService
    {
        /// <summary>
        /// 上传历史数据
        /// </summary>
        /// <param name="ngInfo"></param>
        /// <returns></returns>
        public OperateResult<string> UploadHistoricalResult(List<NgInfoDto> ngInfos);
        /// <summary>
        /// 上传结果
        /// </summary>
        /// <returns></returns>
        public OperateResult<string> UploadResult();
        /// <summary>
        /// 电芯拆盘数据上传
        /// </summary>
        /// <returns></returns>
        public OperateResult<string> BatteryDismantlingDiskDataUploadToMes();
        /// <summary>
        /// OCV1/OCV2测试柜电芯拆盘数据校验
        /// </summary>
        /// <returns></returns>
        public OperateResult<string> VerifyDataOCV1OCV2TestCabinet();
        /// <summary>
        /// 分容下柜NG
        /// </summary>
        /// <returns></returns>
        public OperateResult<string> CapacitySortingNG();
        /// <summary>
        /// 化成下柜NG
        /// </summary>
        /// <returns></returns>
        public OperateResult<string> FormingNG();
        /// <summary>
        /// 设备状态上传
        /// </summary>
        /// <returns></returns>
        public OperateResult<string> UploadingDeviceStatus();
        /// <summary>
        /// 用户上岗认证
        /// </summary>
        /// <returns></returns>
        public OperateResult<string> UserAuthentication(string userName, string password);
    }
}