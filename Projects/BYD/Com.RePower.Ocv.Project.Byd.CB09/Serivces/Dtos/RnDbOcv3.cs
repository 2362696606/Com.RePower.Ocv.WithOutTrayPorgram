namespace Com.RePower.Ocv.Project.Byd.CB09.Serivces.Dtos;

public partial class RnDbOcv3
{
    public long Id { get; set; }

    public string? EqpId { get; set; }

    public string? PcId { get; set; }

    public string Operation { get; set; } = null!;

    public decimal IsTrans { get; set; }

    public string TrayId { get; set; } = null!;

    public string CellId { get; set; } = null!;

    public int BatteryPos { get; set; }

    public string? ModelNo { get; set; }

    public string? BatchNo { get; set; }

    public string? TotalNgState { get; set; }

    public decimal OcvVoltage { get; set; }

    public decimal? Acir { get; set; }

    public string? TestNgCode { get; set; }

    public string? TestResult { get; set; }

    public string? TestResultDesc { get; set; }

    public decimal? PostiveShellVoltage { get; set; }

    public string? PostiveSvNgCode { get; set; }

    public string? PostiveSvResult { get; set; }

    public string? PostiveSvResultDesc { get; set; }

    public decimal? ShellVoltage { get; set; }

    public string? SvNgCode { get; set; }

    public string? SvResult { get; set; }

    public string? SvResultDesc { get; set; }

    public decimal? PostiveTemp { get; set; }

    public decimal? NegativeTemp { get; set; }

    public decimal? K { get; set; }

    public decimal? VDrop { get; set; }

    public decimal? VDropRange { get; set; }

    public string? VDropRangeCode { get; set; }

    public string? VDropResult { get; set; }

    public string? VDropResultDesc { get; set; }

    public decimal? AcirRange { get; set; }

    public string? RRangeNgCode { get; set; }

    public string? RRangeResult { get; set; }

    public string? RRangeResultDesc { get; set; }

    public decimal? RevOcv { get; set; }

    public decimal? Capacity { get; set; }

    public DateTime EndDateTime { get; set; }

    public DateTime? InsertTime { get; set; }

    public string TestMode { get; set; } = null!;

    public decimal? DischargeTime { get; set; }
}