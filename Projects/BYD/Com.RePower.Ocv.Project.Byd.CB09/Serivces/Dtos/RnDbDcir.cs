namespace Com.RePower.Ocv.Project.Byd.CB09.Serivces.Dtos;

public partial class RnDbDcir
{
    public long Id { get; set; }

    public string? EqpId { get; set; }

    public string? PcId { get; set; }

    public string? Operation { get; set; }

    public decimal? IsTrans { get; set; }

    public string TrayId { get; set; } = null!;

    public string CellId { get; set; } = null!;

    public int? BatteryPos { get; set; }

    public string? ModelNo { get; set; }

    public decimal? OpenVoltage { get; set; }

    public decimal? DDcir { get; set; }

    public decimal? CDcir { get; set; }

    public decimal? TcDDcir { get; set; }

    public decimal? TcCDcir { get; set; }

    public decimal? DEndVoltage { get; set; }

    public decimal? CEndVoltage { get; set; }

    public decimal? DEndCurrent { get; set; }

    public decimal? CEndCurrent { get; set; }

    public decimal? DDcirDelta { get; set; }

    public string? NgCode { get; set; }

    public decimal? AnodeTemp { get; set; }

    public decimal? CathodeTemp { get; set; }

    public decimal? MaxAnodeTemp { get; set; }

    public decimal? MaxCathodeTemp { get; set; }

    public DateTime? EndDateTime { get; set; }
}