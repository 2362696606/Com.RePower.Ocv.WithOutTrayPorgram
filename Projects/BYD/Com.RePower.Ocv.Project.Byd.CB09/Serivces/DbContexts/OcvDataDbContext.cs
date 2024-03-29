﻿using System.Configuration;
using Com.RePower.Ocv.Project.Byd.CB09.Serivces.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Com.RePower.Ocv.Project.Byd.CB09.Serivces.DbContexts;

public partial class OcvDataDbContext : DbContext
{
    public virtual DbSet<RnDbDcir> RnDbDcir { get; set; }

    public virtual DbSet<RnDbOcv> RnDbOcv0 { get; set; }

    public virtual DbSet<RnDbOcv> RnDbOcv1 { get; set; }

    public virtual DbSet<RnDbOcv> RnDbOcv2 { get; set; }

    public virtual DbSet<RnDbOcv> RnDbOcv3 { get; set; }

    public virtual DbSet<RnDbOcv> RnDbOcv4 { get; set; }

    public virtual DbSet<RnDbOcv> RnDbOcv5 { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //string connectString = string.Empty;
        //using (var settingContext = new OcvSettingDbContext())
        //{
        //    var item = settingContext.SettingItems.First(x => x.SettingName == "MES数据库连接字符串");
        //    connectString = item.JsonValue;
        //}
        optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["MesDataBase"].ConnectionString);
        //optionsBuilder.UseLazyLoadingProxies();
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region
        modelBuilder.Entity<RnDbDcir>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__rn_db_dc__3214EC274B16DA18");

            entity.ToTable("rn_db_dcir");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AnodeTemp).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.BatteryPos).HasColumnName("BATTERY_POS");
            entity.Property(e => e.CDcir)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("C_DCIR");
            entity.Property(e => e.CEndCurrent)
                .HasColumnType("decimal(8, 4)")
                .HasColumnName("C_EndCurrent");
            entity.Property(e => e.CEndVoltage)
                .HasColumnType("decimal(8, 4)")
                .HasColumnName("C_EndVoltage");
            entity.Property(e => e.CathodeTemp).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.CellId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CELL_ID");
            entity.Property(e => e.DDcir)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("D_DCIR");
            entity.Property(e => e.DDcirDelta)
                .HasColumnType("decimal(8, 4)")
                .HasColumnName("D_DCIR_Delta");
            entity.Property(e => e.DEndCurrent)
                .HasColumnType("decimal(8, 4)")
                .HasColumnName("D_EndCurrent");
            entity.Property(e => e.DEndVoltage)
                .HasColumnType("decimal(8, 4)")
                .HasColumnName("D_EndVoltage");
            entity.Property(e => e.EndDateTime)
                .HasColumnType("datetime")
                .HasColumnName("END_DATE_TIME");
            entity.Property(e => e.EqpId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Eqp_ID");
            entity.Property(e => e.IsTrans)
                .HasColumnType("decimal(1, 0)")
                .HasColumnName("IS_TRANS");
            entity.Property(e => e.MaxAnodeTemp)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("Max_AnodeTemp");
            entity.Property(e => e.MaxCathodeTemp)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("Max_CathodeTemp");
            entity.Property(e => e.ModelNo)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("MODEL_NO");
            entity.Property(e => e.NgCode)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("NG_CODE");
            entity.Property(e => e.OpenVoltage).HasColumnType("decimal(8, 4)");
            entity.Property(e => e.Operation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("OPERATION");
            entity.Property(e => e.PcId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PC_ID");
            entity.Property(e => e.TcCDcir)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("TC_C_DCIR");
            entity.Property(e => e.TcDDcir)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("TC_D_DCIR");
            entity.Property(e => e.TrayId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TRAY_ID");
        });
        #endregion
        #region
        modelBuilder.Entity<RnDbOcv>(entity =>
        {
            entity.ToTable("rn_db_ocv1");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Acir)
                .HasColumnType("decimal(20, 7)")
                .HasColumnName("ACIR");
            entity.Property(e => e.AcirRange)
                .HasColumnType("decimal(20, 6)")
                .HasColumnName("ACIR_RANGE");
            entity.Property(e => e.BatchNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("BATCH_NO");
            entity.Property(e => e.BatteryPos).HasColumnName("BATTERY_POS");
            entity.Property(e => e.Capacity)
                .HasColumnType("decimal(16, 7)")
                .HasColumnName("CAPACITY");
            entity.Property(e => e.CellId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CELL_ID");
            entity.Property(e => e.DischargeTime).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.EndDateTime)
                .HasColumnType("datetime")
                .HasColumnName("END_DATE_TIME");
            entity.Property(e => e.EqpId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Eqp_ID");
            entity.Property(e => e.InsertTime).HasColumnType("datetime");
            entity.Property(e => e.IsTrans)
                .HasColumnType("decimal(1, 0)")
                .HasColumnName("IS_TRANS");
            entity.Property(e => e.K).HasColumnType("decimal(16, 6)");
            entity.Property(e => e.ModelNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MODEL_NO");
            entity.Property(e => e.NegativeTemp)
                .HasColumnType("decimal(8, 1)")
                .HasColumnName("NEGATIVE_TEMP");
            entity.Property(e => e.OcvVoltage)
                .HasColumnType("decimal(16, 7)")
                .HasColumnName("OCV_VOLTAGE");
            entity.Property(e => e.Operation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("OPERATION");
            entity.Property(e => e.PcId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PC_ID");
            entity.Property(e => e.PostiveShellVoltage)
                .HasColumnType("decimal(16, 7)")
                .HasColumnName("PostiveSHELL_VOLTAGE");
            entity.Property(e => e.PostiveSvNgCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PostiveSV_NG_CODE");
            entity.Property(e => e.PostiveSvResult)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PostiveSV_RESULT");
            entity.Property(e => e.PostiveSvResultDesc)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("PostiveSV_RESULT_DESC");
            entity.Property(e => e.PostiveTemp)
                .HasColumnType("decimal(8, 1)")
                .HasColumnName("POSTIVE_TEMP");
            entity.Property(e => e.RRangeNgCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("R_RANGE_NG_CODE");
            entity.Property(e => e.RRangeResult)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("R_RANGE_RESULT");
            entity.Property(e => e.RRangeResultDesc)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("R_RANGE_RESULT_DESC");
            entity.Property(e => e.RevOcv)
                .HasColumnType("decimal(16, 7)")
                .HasColumnName("REV_OCV");
            entity.Property(e => e.ShellVoltage)
                .HasColumnType("decimal(16, 7)")
                .HasColumnName("SHELL_VOLTAGE");
            entity.Property(e => e.SvNgCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SV_NG_CODE");
            entity.Property(e => e.SvResult)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SV_RESULT");
            entity.Property(e => e.SvResultDesc)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("SV_RESULT_DESC");
            entity.Property(e => e.TestMode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TestNgCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TEST_NG_CODE");
            entity.Property(e => e.TestResult)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TEST_RESULT");
            entity.Property(e => e.TestResultDesc)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("TEST_RESULT_DESC");
            entity.Property(e => e.TotalNgState)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TOTAL_NG_STATE");
            entity.Property(e => e.TrayId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TRAY_ID");
            entity.Property(e => e.VDrop)
                .HasColumnType("decimal(16, 7)")
                .HasColumnName("V_DROP");
            entity.Property(e => e.VDropRange)
                .HasColumnType("decimal(16, 7)")
                .HasColumnName("V_DROP_RANGE");
            entity.Property(e => e.VDropRangeCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("V_DROP_RANGE_CODE");
            entity.Property(e => e.VDropResult)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("V_DROP_RESULT");
            entity.Property(e => e.VDropResultDesc)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("V_DROP_RESULT_DESC");
        });

        modelBuilder.Entity<RnDbOcv>(entity =>
        {
            entity.ToTable("rn_db_ocv2");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Acir)
                .HasColumnType("decimal(20, 7)")
                .HasColumnName("ACIR");
            entity.Property(e => e.AcirRange)
                .HasColumnType("decimal(20, 6)")
                .HasColumnName("ACIR_RANGE");
            entity.Property(e => e.BatchNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("BATCH_NO");
            entity.Property(e => e.BatteryPos).HasColumnName("BATTERY_POS");
            entity.Property(e => e.Capacity)
                .HasColumnType("decimal(16, 7)")
                .HasColumnName("CAPACITY");
            entity.Property(e => e.CellId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CELL_ID");
            entity.Property(e => e.DischargeTime).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.EndDateTime)
                .HasColumnType("datetime")
                .HasColumnName("END_DATE_TIME");
            entity.Property(e => e.EqpId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Eqp_ID");
            entity.Property(e => e.InsertTime).HasColumnType("datetime");
            entity.Property(e => e.IsTrans)
                .HasColumnType("decimal(1, 0)")
                .HasColumnName("IS_TRANS");
            entity.Property(e => e.K).HasColumnType("decimal(16, 6)");
            entity.Property(e => e.ModelNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MODEL_NO");
            entity.Property(e => e.NegativeTemp)
                .HasColumnType("decimal(8, 1)")
                .HasColumnName("NEGATIVE_TEMP");
            entity.Property(e => e.OcvVoltage)
                .HasColumnType("decimal(16, 7)")
                .HasColumnName("OCV_VOLTAGE");
            entity.Property(e => e.Operation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("OPERATION");
            entity.Property(e => e.PcId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PC_ID");
            entity.Property(e => e.PostiveShellVoltage)
                .HasColumnType("decimal(16, 7)")
                .HasColumnName("PostiveSHELL_VOLTAGE");
            entity.Property(e => e.PostiveSvNgCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PostiveSV_NG_CODE");
            entity.Property(e => e.PostiveSvResult)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PostiveSV_RESULT");
            entity.Property(e => e.PostiveSvResultDesc)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("PostiveSV_RESULT_DESC");
            entity.Property(e => e.PostiveTemp)
                .HasColumnType("decimal(8, 1)")
                .HasColumnName("POSTIVE_TEMP");
            entity.Property(e => e.RRangeNgCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("R_RANGE_NG_CODE");
            entity.Property(e => e.RRangeResult)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("R_RANGE_RESULT");
            entity.Property(e => e.RRangeResultDesc)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("R_RANGE_RESULT_DESC");
            entity.Property(e => e.RevOcv)
                .HasColumnType("decimal(16, 7)")
                .HasColumnName("REV_OCV");
            entity.Property(e => e.ShellVoltage)
                .HasColumnType("decimal(16, 7)")
                .HasColumnName("SHELL_VOLTAGE");
            entity.Property(e => e.SvNgCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SV_NG_CODE");
            entity.Property(e => e.SvResult)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SV_RESULT");
            entity.Property(e => e.SvResultDesc)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("SV_RESULT_DESC");
            entity.Property(e => e.TestMode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TestNgCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TEST_NG_CODE");
            entity.Property(e => e.TestResult)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TEST_RESULT");
            entity.Property(e => e.TestResultDesc)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("TEST_RESULT_DESC");
            entity.Property(e => e.TotalNgState)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TOTAL_NG_STATE");
            entity.Property(e => e.TrayId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TRAY_ID");
            entity.Property(e => e.VDrop)
                .HasColumnType("decimal(16, 7)")
                .HasColumnName("V_DROP");
            entity.Property(e => e.VDropRange)
                .HasColumnType("decimal(16, 7)")
                .HasColumnName("V_DROP_RANGE");
            entity.Property(e => e.VDropRangeCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("V_DROP_RANGE_CODE");
            entity.Property(e => e.VDropResult)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("V_DROP_RESULT");
            entity.Property(e => e.VDropResultDesc)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("V_DROP_RESULT_DESC");
        });

        modelBuilder.Entity<RnDbOcv>(entity =>
        {
            entity.ToTable("rn_db_ocv3");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Acir)
                .HasColumnType("decimal(20, 7)")
                .HasColumnName("ACIR");
            entity.Property(e => e.AcirRange)
                .HasColumnType("decimal(20, 6)")
                .HasColumnName("ACIR_RANGE");
            entity.Property(e => e.BatchNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("BATCH_NO");
            entity.Property(e => e.BatteryPos).HasColumnName("BATTERY_POS");
            entity.Property(e => e.Capacity)
                .HasColumnType("decimal(16, 7)")
                .HasColumnName("CAPACITY");
            entity.Property(e => e.CellId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CELL_ID");
            entity.Property(e => e.DischargeTime).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.EndDateTime)
                .HasColumnType("datetime")
                .HasColumnName("END_DATE_TIME");
            entity.Property(e => e.EqpId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Eqp_ID");
            entity.Property(e => e.InsertTime).HasColumnType("datetime");
            entity.Property(e => e.IsTrans)
                .HasColumnType("decimal(1, 0)")
                .HasColumnName("IS_TRANS");
            entity.Property(e => e.K).HasColumnType("decimal(16, 6)");
            entity.Property(e => e.ModelNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MODEL_NO");
            entity.Property(e => e.NegativeTemp)
                .HasColumnType("decimal(8, 1)")
                .HasColumnName("NEGATIVE_TEMP");
            entity.Property(e => e.OcvVoltage)
                .HasColumnType("decimal(16, 7)")
                .HasColumnName("OCV_VOLTAGE");
            entity.Property(e => e.Operation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("OPERATION");
            entity.Property(e => e.PcId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PC_ID");
            entity.Property(e => e.PostiveShellVoltage)
                .HasColumnType("decimal(16, 7)")
                .HasColumnName("PostiveSHELL_VOLTAGE");
            entity.Property(e => e.PostiveSvNgCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PostiveSV_NG_CODE");
            entity.Property(e => e.PostiveSvResult)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PostiveSV_RESULT");
            entity.Property(e => e.PostiveSvResultDesc)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("PostiveSV_RESULT_DESC");
            entity.Property(e => e.PostiveTemp)
                .HasColumnType("decimal(8, 1)")
                .HasColumnName("POSTIVE_TEMP");
            entity.Property(e => e.RRangeNgCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("R_RANGE_NG_CODE");
            entity.Property(e => e.RRangeResult)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("R_RANGE_RESULT");
            entity.Property(e => e.RRangeResultDesc)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("R_RANGE_RESULT_DESC");
            entity.Property(e => e.RevOcv)
                .HasColumnType("decimal(16, 7)")
                .HasColumnName("REV_OCV");
            entity.Property(e => e.ShellVoltage)
                .HasColumnType("decimal(16, 7)")
                .HasColumnName("SHELL_VOLTAGE");
            entity.Property(e => e.SvNgCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SV_NG_CODE");
            entity.Property(e => e.SvResult)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SV_RESULT");
            entity.Property(e => e.SvResultDesc)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("SV_RESULT_DESC");
            entity.Property(e => e.TestMode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TestNgCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TEST_NG_CODE");
            entity.Property(e => e.TestResult)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TEST_RESULT");
            entity.Property(e => e.TestResultDesc)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("TEST_RESULT_DESC");
            entity.Property(e => e.TotalNgState)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TOTAL_NG_STATE");
            entity.Property(e => e.TrayId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TRAY_ID");
            entity.Property(e => e.VDrop)
                .HasColumnType("decimal(16, 7)")
                .HasColumnName("V_DROP");
            entity.Property(e => e.VDropRange)
                .HasColumnType("decimal(16, 7)")
                .HasColumnName("V_DROP_RANGE");
            entity.Property(e => e.VDropRangeCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("V_DROP_RANGE_CODE");
            entity.Property(e => e.VDropResult)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("V_DROP_RESULT");
            entity.Property(e => e.VDropResultDesc)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("V_DROP_RESULT_DESC");
        });

        modelBuilder.Entity<RnDbOcv>(entity =>
        {
            entity.ToTable("rn_db_ocv4");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Acir)
                .HasColumnType("decimal(20, 7)")
                .HasColumnName("ACIR");
            entity.Property(e => e.AcirRange)
                .HasColumnType("decimal(20, 6)")
                .HasColumnName("ACIR_RANGE");
            entity.Property(e => e.BatchNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("BATCH_NO");
            entity.Property(e => e.BatteryPos).HasColumnName("BATTERY_POS");
            entity.Property(e => e.Capacity)
                .HasColumnType("decimal(16, 7)")
                .HasColumnName("CAPACITY");
            entity.Property(e => e.CellId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CELL_ID");
            entity.Property(e => e.DischargeTime).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.EndDateTime)
                .HasColumnType("datetime")
                .HasColumnName("END_DATE_TIME");
            entity.Property(e => e.EqpId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Eqp_ID");
            entity.Property(e => e.InsertTime).HasColumnType("datetime");
            entity.Property(e => e.IsTrans)
                .HasColumnType("decimal(1, 0)")
                .HasColumnName("IS_TRANS");
            entity.Property(e => e.K).HasColumnType("decimal(16, 6)");
            entity.Property(e => e.ModelNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MODEL_NO");
            entity.Property(e => e.NegativeTemp)
                .HasColumnType("decimal(8, 1)")
                .HasColumnName("NEGATIVE_TEMP");
            entity.Property(e => e.OcvVoltage)
                .HasColumnType("decimal(16, 7)")
                .HasColumnName("OCV_VOLTAGE");
            entity.Property(e => e.Operation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("OPERATION");
            entity.Property(e => e.PcId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PC_ID");
            entity.Property(e => e.PostiveShellVoltage)
                .HasColumnType("decimal(16, 7)")
                .HasColumnName("PostiveSHELL_VOLTAGE");
            entity.Property(e => e.PostiveSvNgCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PostiveSV_NG_CODE");
            entity.Property(e => e.PostiveSvResult)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PostiveSV_RESULT");
            entity.Property(e => e.PostiveSvResultDesc)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("PostiveSV_RESULT_DESC");
            entity.Property(e => e.PostiveTemp)
                .HasColumnType("decimal(8, 1)")
                .HasColumnName("POSTIVE_TEMP");
            entity.Property(e => e.RRangeNgCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("R_RANGE_NG_CODE");
            entity.Property(e => e.RRangeResult)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("R_RANGE_RESULT");
            entity.Property(e => e.RRangeResultDesc)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("R_RANGE_RESULT_DESC");
            entity.Property(e => e.RevOcv)
                .HasColumnType("decimal(16, 7)")
                .HasColumnName("REV_OCV");
            entity.Property(e => e.ShellVoltage)
                .HasColumnType("decimal(16, 7)")
                .HasColumnName("SHELL_VOLTAGE");
            entity.Property(e => e.SvNgCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SV_NG_CODE");
            entity.Property(e => e.SvResult)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SV_RESULT");
            entity.Property(e => e.SvResultDesc)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("SV_RESULT_DESC");
            entity.Property(e => e.TestMode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TestNgCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TEST_NG_CODE");
            entity.Property(e => e.TestResult)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TEST_RESULT");
            entity.Property(e => e.TestResultDesc)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("TEST_RESULT_DESC");
            entity.Property(e => e.TotalNgState)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TOTAL_NG_STATE");
            entity.Property(e => e.TrayId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TRAY_ID");
            entity.Property(e => e.VDrop)
                .HasColumnType("decimal(16, 7)")
                .HasColumnName("V_DROP");
            entity.Property(e => e.VDropRange)
                .HasColumnType("decimal(16, 7)")
                .HasColumnName("V_DROP_RANGE");
            entity.Property(e => e.VDropRangeCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("V_DROP_RANGE_CODE");
            entity.Property(e => e.VDropResult)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("V_DROP_RESULT");
            entity.Property(e => e.VDropResultDesc)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("V_DROP_RESULT_DESC");
        });

        modelBuilder.Entity<RnDbOcv>(entity =>
        {
            entity.ToTable("rn_db_ocv5");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Acir)
                .HasColumnType("decimal(20, 7)")
                .HasColumnName("ACIR");
            entity.Property(e => e.AcirRange)
                .HasColumnType("decimal(20, 6)")
                .HasColumnName("ACIR_RANGE");
            entity.Property(e => e.BatchNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("BATCH_NO");
            entity.Property(e => e.BatteryPos).HasColumnName("BATTERY_POS");
            entity.Property(e => e.Capacity)
                .HasColumnType("decimal(16, 7)")
                .HasColumnName("CAPACITY");
            entity.Property(e => e.CellId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CELL_ID");
            entity.Property(e => e.DischargeTime).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.EndDateTime)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("END_DATE_TIME");
            entity.Property(e => e.EqpId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Eqp_ID");
            entity.Property(e => e.InsertTime)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IsTrans)
                .HasColumnType("decimal(1, 0)")
                .HasColumnName("IS_TRANS");
            entity.Property(e => e.K).HasColumnType("decimal(16, 6)");
            entity.Property(e => e.ModelNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MODEL_NO");
            entity.Property(e => e.NegativeTemp)
                .HasColumnType("decimal(8, 1)")
                .HasColumnName("NEGATIVE_TEMP");
            entity.Property(e => e.OcvVoltage)
                .HasColumnType("decimal(16, 7)")
                .HasColumnName("OCV_VOLTAGE");
            entity.Property(e => e.Operation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("OPERATION");
            entity.Property(e => e.PcId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PC_ID");
            entity.Property(e => e.PostiveShellVoltage)
                .HasColumnType("decimal(16, 7)")
                .HasColumnName("PostiveSHELL_VOLTAGE");
            entity.Property(e => e.PostiveSvNgCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PostiveSV_NG_CODE");
            entity.Property(e => e.PostiveSvResult)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PostiveSV_RESULT");
            entity.Property(e => e.PostiveSvResultDesc)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("PostiveSV_RESULT_DESC");
            entity.Property(e => e.PostiveTemp)
                .HasColumnType("decimal(8, 1)")
                .HasColumnName("POSTIVE_TEMP");
            entity.Property(e => e.RRangeNgCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("R_RANGE_NG_CODE");
            entity.Property(e => e.RRangeResult)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("R_RANGE_RESULT");
            entity.Property(e => e.RRangeResultDesc)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("R_RANGE_RESULT_DESC");
            entity.Property(e => e.RevOcv)
                .HasColumnType("decimal(16, 7)")
                .HasColumnName("REV_OCV");
            entity.Property(e => e.ShellVoltage)
                .HasColumnType("decimal(16, 7)")
                .HasColumnName("SHELL_VOLTAGE");
            entity.Property(e => e.SvNgCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SV_NG_CODE");
            entity.Property(e => e.SvResult)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SV_RESULT");
            entity.Property(e => e.SvResultDesc)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("SV_RESULT_DESC");
            entity.Property(e => e.TestMode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TestNgCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TEST_NG_CODE");
            entity.Property(e => e.TestResult)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TEST_RESULT");
            entity.Property(e => e.TestResultDesc)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("TEST_RESULT_DESC");
            entity.Property(e => e.TotalNgState)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TOTAL_NG_STATE");
            entity.Property(e => e.TrayId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TRAY_ID");
            entity.Property(e => e.VDrop)
                .HasColumnType("decimal(16, 7)")
                .HasColumnName("V_DROP");
            entity.Property(e => e.VDropRange)
                .HasColumnType("decimal(16, 7)")
                .HasColumnName("V_DROP_RANGE");
            entity.Property(e => e.VDropRangeCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("V_DROP_RANGE_CODE");
            entity.Property(e => e.VDropResult)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("V_DROP_RESULT");
            entity.Property(e => e.VDropResultDesc)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("V_DROP_RESULT_DESC");
        });
        #endregion
        modelBuilder.Entity<RnDbOcv>(entity =>
        {
            entity.ToTable("rn_db_ocv0");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Acir)
                .HasColumnType("decimal(20, 7)")
                .HasColumnName("ACIR");
            entity.Property(e => e.AcirRange)
                .HasColumnType("decimal(20, 6)")
                .HasColumnName("ACIR_RANGE");
            entity.Property(e => e.BatchNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("BATCH_NO");
            entity.Property(e => e.BatteryPos).HasColumnName("BATTERY_POS");
            entity.Property(e => e.Capacity)
                .HasColumnType("decimal(16, 7)")
                .HasColumnName("CAPACITY");
            entity.Property(e => e.CellId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CELL_ID");
            entity.Property(e => e.DischargeTime).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.EndDateTime)
                .HasColumnType("datetime")
                .HasColumnName("END_DATE_TIME");
            entity.Property(e => e.EqpId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Eqp_ID");
            entity.Property(e => e.InsertTime).HasColumnType("datetime");
            entity.Property(e => e.IsTrans)
                .HasColumnType("decimal(1, 0)")
                .HasColumnName("IS_TRANS");
            entity.Property(e => e.K).HasColumnType("decimal(16, 6)");
            entity.Property(e => e.ModelNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MODEL_NO");
            entity.Property(e => e.NegativeTemp)
                .HasColumnType("decimal(8, 1)")
                .HasColumnName("NEGATIVE_TEMP");
            entity.Property(e => e.OcvVoltage)
                .HasColumnType("decimal(16, 7)")
                .HasColumnName("OCV_VOLTAGE");
            entity.Property(e => e.Operation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("OPERATION");
            entity.Property(e => e.PcId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PC_ID");
            entity.Property(e => e.PostiveShellVoltage)
                .HasColumnType("decimal(16, 7)")
                .HasColumnName("PostiveSHELL_VOLTAGE");
            entity.Property(e => e.PostiveSvNgCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PostiveSV_NG_CODE");
            entity.Property(e => e.PostiveSvResult)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PostiveSV_RESULT");
            entity.Property(e => e.PostiveSvResultDesc)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("PostiveSV_RESULT_DESC");
            entity.Property(e => e.PostiveTemp)
                .HasColumnType("decimal(8, 1)")
                .HasColumnName("POSTIVE_TEMP");
            entity.Property(e => e.RRangeNgCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("R_RANGE_NG_CODE");
            entity.Property(e => e.RRangeResult)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("R_RANGE_RESULT");
            entity.Property(e => e.RRangeResultDesc)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("R_RANGE_RESULT_DESC");
            entity.Property(e => e.RevOcv)
                .HasColumnType("decimal(16, 7)")
                .HasColumnName("REV_OCV");
            entity.Property(e => e.ShellVoltage)
                .HasColumnType("decimal(16, 7)")
                .HasColumnName("SHELL_VOLTAGE");
            entity.Property(e => e.SvNgCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SV_NG_CODE");
            entity.Property(e => e.SvResult)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SV_RESULT");
            entity.Property(e => e.SvResultDesc)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("SV_RESULT_DESC");
            entity.Property(e => e.TestMode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TestNgCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TEST_NG_CODE");
            entity.Property(e => e.TestResult)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TEST_RESULT");
            entity.Property(e => e.TestResultDesc)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("TEST_RESULT_DESC");
            entity.Property(e => e.TotalNgState)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TOTAL_NG_STATE");
            entity.Property(e => e.TrayId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TRAY_ID");
            entity.Property(e => e.VDrop)
                .HasColumnType("decimal(16, 7)")
                .HasColumnName("V_DROP");
            entity.Property(e => e.VDropRange)
                .HasColumnType("decimal(16, 7)")
                .HasColumnName("V_DROP_RANGE");
            entity.Property(e => e.VDropRangeCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("V_DROP_RANGE_CODE");
            entity.Property(e => e.VDropResult)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("V_DROP_RESULT");
            entity.Property(e => e.VDropResultDesc)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("V_DROP_RESULT_DESC");
        });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}