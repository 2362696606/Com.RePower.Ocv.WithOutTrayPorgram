namespace Com.RePower.Ocv.Model.Settings.Dtos
{
    public class TestOptionDto
    {
        /// <summary>
        /// 是否测试电压
        /// </summary>
        public bool IsTestVol { get; set; }

        /// <summary>
        /// 是否测试内阻
        /// </summary>
        public bool IsTestRes { get; set; }

        /// <summary>
        /// 是否测试正极壳电压
        /// </summary>
        public bool IsTestPVol { get; set; }

        /// <summary>
        /// 是否测试负极壳电压
        /// </summary>
        public bool IsTestNVol { get; set; }

        /// <summary>
        /// 是否测试温度
        /// </summary>
        public bool IsTestTemp { get; set; }

        /// <summary>
        /// 是否测试正极温度
        /// </summary>
        public bool IsTestPTemp { get; set; }

        /// <summary>
        /// 是否测试负极温度
        /// </summary>
        public bool IsTestNTemp { get; set; }

        /// <summary>
        /// 是否复测
        /// </summary>
        public bool IsDoRetest { get; set; }

        /// <summary>
        /// 复测次数
        /// </summary>
        public int RetestTimes { get; set; }

        /// <summary>
        /// 是否验证k值
        /// </summary>
        public bool IsVerifyKValue { get; set; }
    }
}