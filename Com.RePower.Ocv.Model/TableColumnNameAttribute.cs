using System;

namespace Com.RePower.Ocv.Model
{
    [AttributeUsage(AttributeTargets.Property)]

    public class TableColumnNameAttribute : Attribute
    {
        /// <summary>
        /// 表上的列名称值，展示出来看的
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// 是否启动，默认为true
        /// </summary>
        public bool Enable { get; set; } = true;
        /// <summary>
        /// 父数据中对应的属性名称，拷数据时使用
        /// </summary>
        public string ParentPropString { get; set; }
    }
}