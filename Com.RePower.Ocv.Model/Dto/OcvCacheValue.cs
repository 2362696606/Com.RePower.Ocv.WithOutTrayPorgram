using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Model.Dto
{
    /// <summary>
    /// Ocv值缓存
    /// </summary>
    public class OcvCacheValue
    {
        public int Id { get; set; }
        /// <summary>
        /// 缓存名
        /// </summary>
        public string SettingName { get; set; } = string.Empty;
        /// <summary>
        /// 缓存值
        /// </summary>
        public string? Value { get; set; }
        /// <summary>
        /// 保留字段1
        /// </summary>
        public string? ReserveText1 { get; set; }
        /// <summary>
        /// 保留字段2
        /// </summary>
        public string? ReserveText2 { get; set; }
        /// <summary>
        /// 保留字段3
        /// </summary>
        public string? ReserveText3 { get; set; }
        /// <summary>
        /// 保留时间1
        /// </summary>
        public DateTime? ReserveTime1 { get; set; } = DateTime.Now;
        /// <summary>
        /// 保留时间2
        /// </summary>
        public DateTime? ReserveTime2 { get; set; } = DateTime.Now;
        /// <summary>
        /// 保留时间3
        /// </summary>
        public DateTime? ReserveTime3 { get; set; } = DateTime.Now;
    }
}
