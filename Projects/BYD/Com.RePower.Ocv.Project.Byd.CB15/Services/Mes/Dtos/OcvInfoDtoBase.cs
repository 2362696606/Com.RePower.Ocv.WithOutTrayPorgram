using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Com.RePower.Ocv.Project.Byd.CB15.Services.Mes.Dtos
{
    public class OcvInfoDtoBase
    {
        /// <summary>
        /// Id
        /// </summary>
        [Column(TypeName = "bigint")]
        [CsvHelper.Configuration.Attributes.Ignore]
        public long ID { get; set; }
        /// <summary>
        /// 上传时间
        /// </summary>
        [Column(TypeName = "datetime")]
        [CsvHelper.Configuration.Attributes.Ignore]
        public DateTime UploadTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 电池条码
        /// </summary>
        [Column(TypeName = "varchar(26)")]
        [CsvHelper.Configuration.Attributes.Index(0)]
        public string Batcode { get; set; } = string.Empty;
        /// <summary>
        /// 结束时间
        /// </summary>
        [Column(TypeName = "datetime")]
        [CsvHelper.Configuration.Attributes.Index(7)]
        public DateTime WorkedTime { get; set; }
        /// <summary>
        /// 托盘条码
        /// </summary>
        [Column(TypeName = "varchar(20)")]
        [CsvHelper.Configuration.Attributes.Index(1)]
        public string TrayCode { get; set; } = string.Empty;
        /// <summary>
        /// 托盘上下层 1:下层 2:上层
        /// </summary>
        [Column(TypeName = "smallint")]
        [CsvHelper.Configuration.Attributes.Index(2)]
        public short TrayPosition { get; set; } = 1;
        /// <summary>
        /// 电池位置
        /// </summary>
        [Column(TypeName = "smallint")]
        [CsvHelper.Configuration.Attributes.Index(3)]
        public short BatChannel { get; set; }
        /// <summary>
        /// 电池温度
        /// </summary>
        [Column(TypeName = "DECIMAL(8, 1)")]
        [CsvHelper.Configuration.Attributes.Index(5)]
        public decimal? BatTemp { get; set; }
        /// <summary>
        /// 结束信息,例：B1
        /// </summary>
        [Column(TypeName = "varchar(2)")]
        [CsvHelper.Configuration.Attributes.Index(6)]
        public string? NGCode { get; set; }
        /// <summary>
        /// 例O102
        /// </summary>
        [Column(TypeName = "varchar(4)")]
        [CsvHelper.Configuration.Attributes.Ignore]
        public string? PCID { get; set; }
        /// <summary>
        /// 例：6楼1单元OCV1
        /// </summary>
        [Column(TypeName = "varchar(20)")]
        [CsvHelper.Configuration.Attributes.Ignore]
        public string? Location { get; set; }
    }
}
