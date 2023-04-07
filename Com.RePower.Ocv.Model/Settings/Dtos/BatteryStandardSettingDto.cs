namespace Com.RePower.Ocv.Model.Settings.Dtos
{
    public class BatteryStandardSettingDto
    {
        /// <summary>
        /// 最大电压
        /// </summary>
        public double MaxVol { get; set; }

        /// <summary>
        /// 最小电压
        /// </summary>
        public double MinVol { get; set; }

        /// <summary>
        /// 最大内阻
        /// </summary>
        public double MaxRes { get; set; }

        /// <summary>
        /// 最小内阻
        /// </summary>
        public double MinRes { get; set; }

        /// <summary>
        /// 最大负极壳体电压
        /// </summary>
        public double MaxNVol { get; set; }

        /// <summary>
        /// 最小负极壳体电压
        /// </summary>
        public double MinNVol { get; set; }

        /// <summary>
        /// 最大正极壳体电压
        /// </summary>
        public double MaxPVol { get; set; }

        /// <summary>
        /// 最小正极壳体电压
        /// </summary>
        public double MinPVol { get; set; }

        /// <summary>
        /// 最大温度
        /// </summary>
        public double MaxTemp { get; set; }

        /// <summary>
        /// 最小温度
        /// </summary>
        public double MinTemp { get; set; }

        /// <summary>
        /// 最大负极温度
        /// </summary>
        public double MaxNTemp { get; set; }

        /// <summary>
        /// 最小负极温度
        /// </summary>
        public double MinNTemp { get; set; }

        /// <summary>
        /// 最大正极温度
        /// </summary>
        public double MaxPTemp { get; set; }

        /// <summary>
        /// 最小正极温度
        /// </summary>
        public double MinPTemp { get; set; }

        /// <summary>
        /// 最大K值
        /// </summary>
        public double MaxKValue { get; set; }

        /// <summary>
        /// 最小K值
        /// </summary>
        public double MinKValue { get; set; }
    }
}