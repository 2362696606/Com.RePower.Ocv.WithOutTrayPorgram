using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Model.Enums
{
    [Flags]
    public enum NgTypeEnum: ulong
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        正常 = 1<<0,
        /// <summary>
        /// 未定义
        /// </summary>
        [Description("未定义")]
        未定义 = 1<<1,
        /// <summary>
        /// 电压过高
        /// </summary>
        [Description("电压过高")]
        电压过高 = 1<<2,
        /// <summary>
        /// 电压过低
        /// </summary>
        [Description("电压过低")]
        电压过低 = 1<<3,
        /// <summary>
        /// 正极壳体电压过高
        /// </summary>
        [Description("正极壳体电压过高")]
        正极壳体电压过高 = 1<<4,
        /// <summary>
        /// 正极壳体电压过低
        /// </summary>
        [Description("正极壳体电压过低")]
        正极壳体电压过低 = 1<<5,
        /// <summary>
        /// 负极壳体电压过高
        /// </summary>
        [Description("负极壳体电压过高")]
        负极壳体电压过高 = 1<<6,
        /// <summary>
        /// 负极壳体电压过低
        /// </summary>
        [Description("负极壳体电压过低")]
        负极壳体电压过低 = 1<<7,
        /// <summary>
        /// 内阻过高
        /// </summary>
        [Description("内阻过高")]
        内阻过高 = 1<<8,
        /// <summary>
        /// 内阻过低
        /// </summary>
        [Description("内阻过低")]
        内阻过低 = 1<<9,
        /// <summary>
        /// 温度过高
        /// </summary>
        [Description("温度过高")]
        温度过高 = 1<<10,
        /// <summary>
        /// 温度过低
        /// </summary>
        [Description("温度过低")]
        温度过低 = 1<<11,
        /// <summary>
        /// 正极温度过高
        /// </summary>
        [Description("正极温度过高")]
        正极温度过高 = 1<<12,
        /// <summary>
        /// 正极温度过低
        /// </summary>
        [Description("正极温度过低")]
        正极温度过低 = 1<<13,
        /// <summary>
        /// 负极温度过高
        /// </summary>
        [Description("负极温度过高")]
        负极温度过高 = 1<<14,
        /// <summary>
        /// 负极温度过低
        /// </summary>
        [Description("负极温度过低")]
        负极温度过低 = 1<<15,
        /// <summary>
        /// K1过高
        /// </summary>
        [Description("K1过高")]
        K1过高 = 1<<16,
        /// <summary>
        /// K1过低
        /// </summary>
        [Description("K1过低")]
        K1过低 = 1<<17,
        /// <summary>
        /// K2过高
        /// </summary>
        [Description("K2过高")]
        K2过高 = 1<<18,
        /// <summary>
        /// K2过低
        /// </summary>
        [Description("K2过低")]
        K2过低 = 1<<19,
        /// <summary>
        /// K3过高
        /// </summary>
        [Description("K3过高")]
        K3过高 = 1<<20,
        /// <summary>
        /// K3过低
        /// </summary>
        [Description("K3过低")]
        K3过低 = 1<<21,
        /// <summary>
        /// 保留Ng类型1
        /// </summary>
        [Description("K值计算失败")]
        K值计算失败 = 1<<22,
        /// <summary>
        /// 保留Ng类型2
        /// </summary>
        [Description("压差过高")]
        压差过高 = 1<<23,
        /// <summary>
        /// 保留Ng类型3
        /// </summary>
        [Description("压差过低")]
        压差过低 = 1<<24,
        /// <summary>
        /// 保留Ng类型4
        /// </summary>
        [Description("压差计算失败")]
        压差计算失败 = 1<<25,
        /// <summary>
        /// 保留Ng类型5
        /// </summary>
        [Description("单托盘K值过高")]
        单托盘k值过高 = 1<<26,
        /// <summary>
        /// 保留Ng类型6
        /// </summary>
        [Description("单托盘K值过低")]
        单托盘k值过低 = 1<<27,
        /// <summary>
        /// 保留Ng类型7
        /// </summary>
        [Description("整体K值过高")]
        整体k值过高 = 1<<28,
        /// <summary>
        /// 保留Ng类型8
        /// </summary>
        [Description("整体K值过低")]
        整体k值过低 = 1<<29,
        /// <summary>
        /// 保留Ng类型9
        /// </summary>
        [Description("保留Ng类型9")]
        保留Ng类型9 = 1<<30,
        /// <summary>
        /// 保留Ng类型10
        /// </summary>
        [Description("保留Ng类型10")]
        保留Ng类型10 = (ulong)1<<31,
    }
}
