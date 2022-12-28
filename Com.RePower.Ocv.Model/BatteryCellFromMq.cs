using System;

namespace Com.RePower.Ocv.Model
{
    /// <summary>
    /// 电芯信息Model（可以当作保存Excel的Model）,拷自Mq
    /// </summary>
    public class BatteryCellFromMq
    {
        #region 字段

        private string _batchName;

        private string _trayCode;

        private int _cellNo;

        private string _cellCode;

        private decimal _ocvValue;

        private int _ocvResultValue = 1;

        private decimal _positiveVoltageValue;

        private int _positiveVoltageResultValue = 1;

        private decimal _negativeVoltageValue;

        private decimal _acirValue;

        private int _acirResultValue = 1;

        private decimal _acirMeterRealValue;

        private int _testResultChooseValue = 1;

        private DateTime _beginTime;

        private DateTime _endTime;

        private int _isDelete;

        private string _userName;



        private string _currentStep;

        private int _position;

        private int _probeNum;

        private double _agingTimeH;

        private double _oCV1HCTimeDiff;

        private decimal _oCV1HCVoltDiff;

        private double _oCV1HeTimeDiff;

        private decimal _k12Value;

        private int _k12ValueResult = 1;

        private double _normalTempTime;

        private double _oCV12TimeDiff;

        private decimal _oCV12VoltDiff;

        private decimal _k13Value;

        private int _k13ValueResult = 1;

        private decimal _k23Value;

        private int _k23ValueResult = 1;

        private double _oCV23TimeDiff;

        private decimal _oCV23VoltDiff;

        private double _oCV13TimeDiff;

        private decimal _oCV13VoltDiff;

        private double _oCV4DCIRTimeDiff;

        private decimal _oCV4DCIRVoltDiff;

        private double _oCV4FRTimeDiff;

        private decimal _oCV4FRVoltDiff;

        private double _highTempTime;

        private DateTime _fRLastEndTime;

        private decimal _dcirValue;

        #endregion 字段

        /// <summary>
        /// 电池位置
        /// </summary>
        [TableColumnName(ColumnName = "电池位置", Enable = true)]
        public int CellNo
        {
            get
            {
                return _cellNo;
            }
            set
            {
                _cellNo = value;
            }
        }

        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchName
        {
            get
            {
                return _batchName;
            }
            set
            {
                _batchName = value;
            }
        }

        /// <summary>
        /// 托盘码
        /// </summary>
        [TableColumnName(ColumnName = "托盘条码", Enable = true)]
        public string TrayCode
        {
            get
            {
                return _trayCode;
            }
            set
            {
                _trayCode = value;
            }
        }

        /// <summary>
        /// 电芯条码
        /// </summary>
        [TableColumnName(ColumnName = "电芯条码", Enable = true)]
        public string CellCode
        {
            get
            {
                return _cellCode;
            }
            set
            {
                _cellCode = value;
            }
        }
        /// <summary>
        /// 电压最大值
        /// </summary>
        [TableColumnName(ColumnName = "壳体电压最大值", Enable = true)]
        public double OcvMaxValue { get; set; }
        /// <summary>
        /// 电压最小值
        /// </summary>
        [TableColumnName(ColumnName = "壳体电压最小值", Enable = true)]
        public double OcvMinValue { get; set; }

        /// <summary>
        /// 电池电压，开路电压
        /// </summary>
        [TableColumnName(ColumnName = "壳体电压(V)", Enable = true)]
        public decimal OcvValue
        {
            get
            {
                return _ocvValue;
            }
            set
            {
                _ocvValue = value;
            }
        }
        /// <summary>
        /// 电池电压，开路电压 是否OK
        /// </summary>
        public bool? OcvValueResult { get; set; }

        [TableColumnName(ColumnName = "壳体电压测试结果", Enable = true)]
        public string OcvValueResultTxt
        {
            get
            {
                if (OcvValueResult == true)
                {
                    return "OK";
                }
                else if (OcvValueResult == false)
                {
                    return "NG";
                }
                else
                {
                    return "无测试结果";
                }
            }
        }

        public int OcvResultValue
        {
            get
            {
                return _ocvResultValue;
            }
            set
            {
                _ocvResultValue = value;
            }
        }
        /// <summary>
        /// 最终的测试结果，OK或者NG
        /// </summary>
        public string LasResult
        {
            get
            {
                if (OcvValueResult == false || AcirValueResult == false || PositiveVoltageValueResult == false || NegativeVoltNgOrOk == false)
                {
                    return "NG";
                }
                return "OK";
            }
        }

        /// <summary>
        /// 正极壳体电压
        /// </summary>
        public decimal PositiveVoltageValue
        {
            get
            {
                return _positiveVoltageValue;
            }
            set
            {
                _positiveVoltageValue = value;
            }
        }
        /// <summary>
        /// 正极壳体电压结果
        /// </summary>
        public bool? PositiveVoltageValueResult { get; set; }

        /// <summary>
        /// 负极壳体电压
        /// </summary>
        [TableColumnName(ColumnName = "负极壳体电压", Enable = true)]
        public decimal NegativeVoltageValue
        {
            get
            {
                return _negativeVoltageValue;
            }
            set
            {
                _negativeVoltageValue = value;
            }
        }

        public bool? NegativeVoltNgOrOk { get; set; }
        [TableColumnName(ColumnName = "负极壳体电压Result", Enable = true)]
        public string NegativeVoltNgOrOkStringResult
        {
            get
            {
                if (NegativeVoltNgOrOk == true)
                {
                    return "OK";
                }
                else if (NegativeVoltNgOrOk == false)
                {
                    return "NG";
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// 内阻最大值
        /// </summary>
        [TableColumnName(ColumnName = "OCR最大值", Enable = true)]
        public double AcirMaxValue { get; set; }
        /// <summary>
        /// 内阻最小值
        /// </summary>
        [TableColumnName(ColumnName = "OCR最小值", Enable = true)]

        public double AcirMinValue { get; set; }
        /// <summary>
        /// ACIR内阻
        /// </summary>
        [TableColumnName(ColumnName = "OCR(欧)", Enable = true)]
        public decimal AcirValue
        {
            get
            {
                return _acirValue;
            }
            set
            {
                _acirValue = value;
            }
        }
        /// <summary>
        /// ACIR内阻测试结果
        /// </summary>
        public bool? AcirValueResult { get; set; }

        [TableColumnName(ColumnName = "OCR测试结果", Enable = true)]
        public string AcirValueResultTxt
        {
            get
            {
                if (AcirValueResult == true)
                {
                    return "OK";
                }
                else if (AcirValueResult == false)
                {
                    return "NG";
                }
                else
                {
                    return "无结果";
                }
            }
        }


        /// <summary>
        /// DCIR内阻
        /// </summary>
        public decimal DcirValue
        {
            get => _dcirValue;
            set => _dcirValue = value;
        }

        public decimal AcirMeterRealValue
        {
            get
            {
                return _acirMeterRealValue;
            }
            set
            {
                _acirMeterRealValue = value;
            }
        }

        public int AcirResultValue
        {
            get
            {
                return _acirResultValue;
            }
            set
            {
                _acirResultValue = value;
            }
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        [TableColumnName(ColumnName = "开始时间", Enable = true)]
        public DateTime BeginTime
        {
            get
            {
                return _beginTime;
            }
            set
            {
                _beginTime = value;
            }
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        [TableColumnName(ColumnName = "结束时间", Enable = true)]
        public DateTime EndTime
        {
            get
            {
                return _endTime;
            }
            set
            {
                _endTime = value;
            }
        }

        public int IsDelete
        {
            get
            {
                return _isDelete;
            }
            set
            {
                _isDelete = value;
            }
        }

        /// <summary>
        /// 电池挑选结果值,或者说档位信息
        /// </summary>
        public int TestResultChooseValue
        {
            get
            {
                return _testResultChooseValue;
            }
            set
            {
                _testResultChooseValue = value;
            }
        }

        /// <summary>
        /// 操作者
        /// </summary>
        [TableColumnName(ColumnName = "操作者", Enable = true)]
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
            }
        }

        /// <summary>
        /// 测试次数
        /// </summary>
        [TableColumnName(ColumnName = "测试次数", Enable = true)]
        public int TestCount { get; set; }
        [TableColumnName(ColumnName = "当前工步", Enable = true)]
        public string OcvType { get; set; }

        /// <summary>
        /// 测试工艺
        /// </summary>
        public string CurrentStep
        {
            get
            {
                return _currentStep;
            }
            set
            {
                _currentStep = value;
            }
        }

        /// <summary>
        /// 电压结果
        /// </summary>
        public string OcvResultTxt
        {
            get
            {
                if (_ocvResultValue == 1)
                {
                    return "OK";
                }
                if (_ocvResultValue == 0)
                {
                    return "NG";
                }
                return "";
            }
        }

        /// <summary>
        /// ACIR内阻结果
        /// </summary>
        public string AcirResultTxt
        {
            get
            {
                if (_acirResultValue == 1)
                {
                    return "OK";
                }
                if (_acirResultValue == 0)
                {
                    return "NG";
                }
                return "";
            }
        }

        /// <summary>
        /// 壳体电压结果
        /// </summary>
        public string PositiveVoltageResultTxt
        {
            get
            {
                if (_positiveVoltageResultValue == 1)
                {
                    return "OK";
                }
                if (_positiveVoltageResultValue == 0)
                {
                    return "NG";
                }
                return "";
            }
        }

        public string BatteryResultTxt
        {
            get
            {
                string result = string.Empty;
                if (OcvResultValue == 0 || AcirResultValue == 0)
                {
                    return "NG";
                }
                return "OK";
            }
        }
        /// <summary>
        /// 外部NG,false为ng，true为正常
        /// </summary>
        public bool ExternalNg { get; set; } = true;

        public string ExternNgTxt { get; set; }

        /// <summary>
        /// NG原因
        /// </summary>
        [TableColumnName(ColumnName = "NG原因", Enable = true)]
        public string NgReasonTxt
        {
            get
            {
                string ngReasonTxt = string.Empty;
                if (ExternalNg == false)
                {
                    ngReasonTxt += (string.IsNullOrEmpty(ExternNgTxt) ? "外部ng|" : ExternNgTxt + "|");
                }
                if (OcvValueResult == false)
                {
                    ngReasonTxt += "OCV电压NG|";
                }
                //if (_acirResultValue == 0)
                //{
                //    ngReasonTxt += "Acir内阻NG|";
                //}

                if (AcirValueResult == false)
                {
                    ngReasonTxt += "Acir内阻NG|";
                }
                //if (_k12ValueResult == 0)
                //{
                //    ngReasonTxt += "K12值NG|";
                //}
                //if (_k13ValueResult == 0)
                //{
                //    ngReasonTxt += "K13值NG|";
                //}
                //if (_k23ValueResult == 0)
                //{
                //    ngReasonTxt += "K23值NG|";
                //}
                if (PositiveVoltageValueResult == false)
                {
                    ngReasonTxt += "Positive Volt NG";
                }
                if (NegativeVoltNgOrOk == false)
                {
                    ngReasonTxt += "Negative Volt Ng";
                }
                return ngReasonTxt.TrimEnd('|');
            }
        }

        /// <summary>
        /// 位置？
        /// </summary>
        public int Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
            }
        }

        /// <summary>
        /// 探针号
        /// </summary>
        [TableColumnName(ColumnName = "探针号", Enable = true)]
        public int ProbeNum
        {
            get
            {
                return _probeNum;
            }
            set
            {
                _probeNum = value;
            }
        }

        /// <summary>
        /// 高温陈化时长
        /// </summary>
        public double AgingTimeH
        {
            get
            {
                return _agingTimeH;
            }
            set
            {
                _agingTimeH = value;
            }
        }

        /// <summary>
        /// OCV1与化成结束时间差
        /// </summary>
        public double OCV1HCTimeDiff
        {
            get
            {
                return _oCV1HCTimeDiff;
            }
            set
            {
                _oCV1HCTimeDiff = value;
            }
        }

        /// <summary>
        /// OCV1与化成结束电压压差
        /// </summary>
        public decimal OCV1HCVoltDiff
        {
            get
            {
                return _oCV1HCVoltDiff;
            }
            set
            {
                _oCV1HCVoltDiff = value;
            }
        }

        /// <summary>
        /// OCV1与后氦检时间差
        /// </summary>
        public double OCV1HeTimeDiff
        {
            get
            {
                return _oCV1HeTimeDiff;
            }
            set
            {
                _oCV1HeTimeDiff = value;
            }
        }

        /// <summary>
        /// 高温老化时长
        /// </summary>
        public double HighTempTime
        {
            get
            {
                return _highTempTime;
            }
            set
            {
                _highTempTime = value;
            }
        }

        /// <summary>
        /// K12值
        /// </summary>
        public decimal K12Value
        {
            get
            {
                return _k12Value;
            }
            set
            {
                _k12Value = value;
            }
        }

        /// <summary>
        /// K12结果判定值
        /// </summary>
        public int K12ValueResult
        {
            get
            {
                return _k12ValueResult;
            }
            set
            {
                _k12ValueResult = value;
            }
        }

        /// <summary>
        /// 常温静置1时长
        /// </summary>
        public double NormalTempTime
        {
            get
            {
                return _normalTempTime;
            }
            set
            {
                _normalTempTime = value;
            }
        }

        /// <summary>
        /// OCV2与OCV1测试时间差
        /// </summary>
        public double OCV12TimeDiff
        {
            get
            {
                return _oCV12TimeDiff;
            }
            set
            {
                _oCV12TimeDiff = value;
            }
        }

        /// <summary>
        /// OCV2与OCV1压降
        /// </summary>
        public decimal OCV12VoltDiff
        {
            get
            {
                return _oCV12VoltDiff;
            }
            set
            {
                _oCV12VoltDiff = value;
            }
        }

        /// <summary>
        /// K13值
        /// </summary>
        public decimal K13Value
        {
            get
            {
                return _k13Value;
            }
            set
            {
                _k13Value = value;
            }
        }

        /// <summary>
        /// K13判定结果值
        /// </summary>
        public int K13ValueResult
        {
            get
            {
                return _k13ValueResult;
            }
            set
            {
                _k13ValueResult = value;
            }
        }

        /// <summary>
        /// K23值
        /// </summary>
        public decimal K23Value
        {
            get
            {
                return _k23Value;
            }
            set
            {
                _k23Value = value;
            }
        }

        /// <summary>
        /// K23判定结果值
        /// </summary>
        public int K23ValueResult
        {
            get
            {
                return _k23ValueResult;
            }
            set
            {
                _k23ValueResult = value;
            }
        }

        /// <summary>
        /// OCV3与OCV2时间差
        /// </summary>
        public double OCV23TimeDiff
        {
            get
            {
                return _oCV23TimeDiff;
            }
            set
            {
                _oCV23TimeDiff = value;
            }
        }

        /// <summary>
        /// OCV2与OCV3压降
        /// </summary>
        public decimal OCV23VoltDiff
        {
            get
            {
                return _oCV23VoltDiff;
            }
            set
            {
                _oCV23VoltDiff = value;
            }
        }

        /// <summary>
        /// OCV3与OCV1时间差
        /// </summary>
        public double OCV13TimeDiff
        {
            get
            {
                return _oCV13TimeDiff;
            }
            set
            {
                _oCV13TimeDiff = value;
            }
        }

        /// <summary>
        /// OCV1与OCV3压降
        /// </summary>
        public decimal OCV13VoltDiff
        {
            get
            {
                return _oCV13VoltDiff;
            }
            set
            {
                _oCV13VoltDiff = value;
            }
        }

        /// <summary>
        /// OCV4与DCIR时间差
        /// </summary>
        public double OCV4DCIRTimeDiff
        {
            get
            {
                return _oCV4DCIRTimeDiff;
            }
            set
            {
                _oCV4DCIRTimeDiff = value;
            }
        }

        /// <summary>
        /// DCIR结束电压与OCV4压差
        /// </summary>
        public decimal OCV4DCIRVoltDiff
        {
            get
            {
                return _oCV4DCIRVoltDiff;
            }
            set
            {
                _oCV4DCIRVoltDiff = value;
            }
        }

        /// <summary>
        /// OCV4与分容结束时间差
        /// </summary>
        public double OCV4FRTimeDiff
        {
            get
            {
                return _oCV4FRTimeDiff;
            }
            set
            {
                _oCV4FRTimeDiff = value;
            }
        }

        /// <summary>
        /// 分容结束电压与OCV4压差
        /// </summary>
        public decimal OCV4FRVoltDiff
        {
            get
            {
                return _oCV4FRVoltDiff;
            }
            set
            {
                _oCV4FRVoltDiff = value;
            }
        }

        /// <summary>
        /// 分容末次结束时间
        /// </summary>
        public DateTime FRLastEndTime
        {
            get
            {
                return _fRLastEndTime;
            }
            set
            {
                _fRLastEndTime = value;
            }
        }

        public void InitMOdelValue()
        {
            this.TrayCode = string.Empty;
            this.AcirValue = 0;
            this.AcirValueResult = null;
            this.OcvValue = 0;
            this.OcvValueResult = null;
            this.NegativeVoltageValue = 0;
            this.NegativeVoltNgOrOk = null;
            this.PositiveVoltageValue = 0;
            this.PositiveVoltageValueResult = null;
            this.ExternalNg = true;
            this.ExternNgTxt = string.Empty;
        }
    }
}