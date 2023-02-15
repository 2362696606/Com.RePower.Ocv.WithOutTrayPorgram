using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Model.Enums
{
    [Flags]
    public enum NgTypeEnum
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        正常 = 1,
        /// <summary>
        /// 未定义
        /// </summary>
        [Description("未定义")]
        未定义 = 2,
        /// <summary>
        /// 电压过高
        /// </summary>
        [Description("电压过高")]
        电压过高 = 4,
        /// <summary>
        /// 电压过低
        /// </summary>
        [Description("电压过低")]
        电压过低 = 8,
        /// <summary>
        /// 正极壳体电压过高
        /// </summary>
        [Description("正极壳体电压过高")]
        正极壳体电压过高 = 16,
        /// <summary>
        /// 正极壳体电压过低
        /// </summary>
        [Description("正极壳体电压过低")]
        正极壳体电压过低 = 32,
        /// <summary>
        /// 负极壳体电压过高
        /// </summary>
        [Description("负极壳体电压过高")]
        负极壳体电压过高 = 64,
        /// <summary>
        /// 负极壳体电压过低
        /// </summary>
        [Description("负极壳体电压过低")]
        负极壳体电压过低 = 128,
        /// <summary>
        /// 内阻过高
        /// </summary>
        [Description("内阻过高")]
        内阻过高 = 256,
        /// <summary>
        /// 内阻过低
        /// </summary>
        [Description("内阻过低")]
        内阻过低 = 512,
        /// <summary>
        /// 温度过高
        /// </summary>
        [Description("温度过高")]
        温度过高 = 1024,
        /// <summary>
        /// 温度过低
        /// </summary>
        [Description("温度过低")]
        温度过低 = 2048,
        /// <summary>
        /// 正极温度过高
        /// </summary>
        [Description("正极温度过高")]
        正极温度过高 = 4096,
        /// <summary>
        /// 正极温度过低
        /// </summary>
        [Description("正极温度过低")]
        正极温度过低 = 8192,
        /// <summary>
        /// 负极温度过高
        /// </summary>
        [Description("负极温度过高")]
        负极温度过高 = 16384,
        /// <summary>
        /// 负极温度过低
        /// </summary>
        [Description("负极温度过低")]
        负极温度过低 = 32768,
        /// <summary>
        /// K1过高
        /// </summary>
        [Description("K1过高")]
        K1过高 = 65536,
        /// <summary>
        /// K1过低
        /// </summary>
        [Description("K1过低")]
        K1过低 = 131072,
        /// <summary>
        /// K2过高
        /// </summary>
        [Description("K2过高")]
        K2过高 = 262144,
        /// <summary>
        /// K2过低
        /// </summary>
        [Description("K2过低")]
        K2过低 = 524288,
        /// <summary>
        /// K3过高
        /// </summary>
        [Description("K3过高")]
        K3过高 = 1048576,
        /// <summary>
        /// K3过低
        /// </summary>
        [Description("K3过低")]
        K3过低 = 2097152,
        /// <summary>
        /// 保留Ng类型1
        /// </summary>
        [Description("K值计算失败")]
        K值计算失败 = 4194304,
        /// <summary>
        /// 保留Ng类型2
        /// </summary>
        [Description("压差过高")]
        压差过高 = 8388608,
        /// <summary>
        /// 保留Ng类型3
        /// </summary>
        [Description("压差过低")]
        压差过低 = 16777216,
        /// <summary>
        /// 保留Ng类型4
        /// </summary>
        [Description("压差计算失败")]
        压差计算失败 = 33554432,
        /// <summary>
        /// 保留Ng类型5
        /// </summary>
        [Description("单托盘K值过高")]
        单托盘K值过高 = 67108864,
        /// <summary>
        /// 保留Ng类型6
        /// </summary>
        [Description("单托盘K值过低")]
        单托盘K值过低 = 67108864,
        /// <summary>
        /// 保留Ng类型7
        /// </summary>
        [Description("整体K值过高")]
        整体K值过高 = 134217728,
        /// <summary>
        /// 保留Ng类型8
        /// </summary>
        [Description("整体K值过低")]
        整体K值过低 = 268435456,
        /// <summary>
        /// 保留Ng类型9
        /// </summary>
        [Description("保留Ng类型9")]
        保留Ng类型9 = 536870912,
        /// <summary>
        /// 保留Ng类型10
        /// </summary>
        [Description("保留Ng类型10")]
        保留Ng类型10 = 1073741824,
    }
}
