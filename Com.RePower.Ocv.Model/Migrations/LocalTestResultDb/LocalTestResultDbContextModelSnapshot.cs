﻿// <auto-generated />
using System;
using Com.RePower.Ocv.Model.DataBaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Com.RePower.Ocv.Model.Migrations.LocalTestResultDb
{
    [DbContext(typeof(LocalTestResultDbContext))]
    partial class LocalTestResultDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true);

            modelBuilder.Entity("Com.RePower.Ocv.Model.Dto.BatteryDto", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("BarCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("BatteryType")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("KValue1")
                        .HasColumnType("REAL");

                    b.Property<double?>("KValue2")
                        .HasColumnType("REAL");

                    b.Property<double?>("KValue3")
                        .HasColumnType("REAL");

                    b.Property<double?>("KValue4")
                        .HasColumnType("REAL");

                    b.Property<double?>("KValue5")
                        .HasColumnType("REAL");

                    b.Property<double?>("NTemp")
                        .HasColumnType("REAL");

                    b.Property<double?>("NVolValue")
                        .HasColumnType("REAL");

                    b.Property<string>("OcvStationName")
                        .HasColumnType("TEXT");

                    b.Property<string>("OcvType")
                        .HasColumnType("TEXT");

                    b.Property<double?>("PTemp")
                        .HasColumnType("REAL");

                    b.Property<double?>("PVolValue")
                        .HasColumnType("REAL");

                    b.Property<int>("Position")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("Res")
                        .HasColumnType("REAL");

                    b.Property<int?>("ReserveInt1")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ReserveInt2")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ReserveInt3")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ReserveInt4")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ReserveInt5")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ReserveText1")
                        .HasColumnType("TEXT");

                    b.Property<string>("ReserveText2")
                        .HasColumnType("TEXT");

                    b.Property<string>("ReserveText3")
                        .HasColumnType("TEXT");

                    b.Property<string>("ReserveText4")
                        .HasColumnType("TEXT");

                    b.Property<string>("ReserveText5")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ReserveTime1")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ReserveTime2")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ReserveTime3")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ReserveTime4")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ReserveTime5")
                        .HasColumnType("TEXT");

                    b.Property<double?>("ReserveValue1")
                        .HasColumnType("REAL");

                    b.Property<double?>("ReserveValue2")
                        .HasColumnType("REAL");

                    b.Property<double?>("ReserveValue3")
                        .HasColumnType("REAL");

                    b.Property<double?>("ReserveValue4")
                        .HasColumnType("REAL");

                    b.Property<double?>("ReserveValue5")
                        .HasColumnType("REAL");

                    b.Property<double?>("Temp")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("TestTime")
                        .HasColumnType("TEXT");

                    b.Property<double?>("VolValue")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Batterys");
                });

            modelBuilder.Entity("Com.RePower.Ocv.Model.Dto.NgInfoDto", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("BatteryId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsNg")
                        .HasColumnType("INTEGER");

                    b.Property<string>("NgDescription")
                        .HasColumnType("TEXT");

                    b.Property<int?>("NgType")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("TrayDtoId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BatteryId");

                    b.HasIndex("TrayDtoId");

                    b.ToTable("NgInfos");
                });

            modelBuilder.Entity("Com.RePower.Ocv.Model.Dto.TrayDto", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("TrayCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Trays");
                });

            modelBuilder.Entity("Com.RePower.Ocv.Model.Dto.NgInfoDto", b =>
                {
                    b.HasOne("Com.RePower.Ocv.Model.Dto.BatteryDto", "Battery")
                        .WithMany()
                        .HasForeignKey("BatteryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Com.RePower.Ocv.Model.Dto.TrayDto", null)
                        .WithMany("NgInfos")
                        .HasForeignKey("TrayDtoId");

                    b.Navigation("Battery");
                });

            modelBuilder.Entity("Com.RePower.Ocv.Model.Dto.TrayDto", b =>
                {
                    b.Navigation("NgInfos");
                });
#pragma warning restore 612, 618
        }
    }
}
