using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Com.RePower.Ocv.Model.Migrations.LocalTestResultDb
{
    /// <inheritdoc />
    public partial class AddOcvType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Batterys",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BarCode = table.Column<string>(type: "TEXT", nullable: false),
                    Position = table.Column<int>(type: "INTEGER", nullable: false),
                    BatteryType = table.Column<int>(type: "INTEGER", nullable: true),
                    OcvType = table.Column<string>(type: "TEXT", nullable: true),
                    VolValue = table.Column<double>(type: "REAL", nullable: true),
                    PVolValue = table.Column<double>(type: "REAL", nullable: true),
                    NVolValue = table.Column<double>(type: "REAL", nullable: true),
                    Res = table.Column<double>(type: "REAL", nullable: true),
                    Temp = table.Column<double>(type: "REAL", nullable: true),
                    PTemp = table.Column<double>(type: "REAL", nullable: true),
                    NTemp = table.Column<double>(type: "REAL", nullable: true),
                    KValue1 = table.Column<double>(type: "REAL", nullable: true),
                    KValue2 = table.Column<double>(type: "REAL", nullable: true),
                    KValue3 = table.Column<double>(type: "REAL", nullable: true),
                    KValue4 = table.Column<double>(type: "REAL", nullable: true),
                    KValue5 = table.Column<double>(type: "REAL", nullable: true),
                    TestTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ReserveInt1 = table.Column<int>(type: "INTEGER", nullable: true),
                    ReserveInt2 = table.Column<int>(type: "INTEGER", nullable: true),
                    ReserveInt3 = table.Column<int>(type: "INTEGER", nullable: true),
                    ReserveInt4 = table.Column<int>(type: "INTEGER", nullable: true),
                    ReserveInt5 = table.Column<int>(type: "INTEGER", nullable: true),
                    ReserveValue1 = table.Column<double>(type: "REAL", nullable: true),
                    ReserveValue2 = table.Column<double>(type: "REAL", nullable: true),
                    ReserveValue3 = table.Column<double>(type: "REAL", nullable: true),
                    ReserveValue4 = table.Column<double>(type: "REAL", nullable: true),
                    ReserveValue5 = table.Column<double>(type: "REAL", nullable: true),
                    ReserveText1 = table.Column<string>(type: "TEXT", nullable: true),
                    ReserveText2 = table.Column<string>(type: "TEXT", nullable: true),
                    ReserveText3 = table.Column<string>(type: "TEXT", nullable: true),
                    ReserveText4 = table.Column<string>(type: "TEXT", nullable: true),
                    ReserveText5 = table.Column<string>(type: "TEXT", nullable: true),
                    ReserveTime1 = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ReserveTime2 = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ReserveTime3 = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ReserveTime4 = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ReserveTime5 = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Batterys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trays",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TrayCode = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NgInfos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BatteryId = table.Column<long>(type: "INTEGER", nullable: false),
                    NgDescription = table.Column<string>(type: "TEXT", nullable: true),
                    IsNg = table.Column<bool>(type: "INTEGER", nullable: false),
                    NgType = table.Column<int>(type: "INTEGER", nullable: true),
                    TrayDtoId = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NgInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NgInfos_Batterys_BatteryId",
                        column: x => x.BatteryId,
                        principalTable: "Batterys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NgInfos_Trays_TrayDtoId",
                        column: x => x.TrayDtoId,
                        principalTable: "Trays",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_NgInfos_BatteryId",
                table: "NgInfos",
                column: "BatteryId");

            migrationBuilder.CreateIndex(
                name: "IX_NgInfos_TrayDtoId",
                table: "NgInfos",
                column: "TrayDtoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NgInfos");

            migrationBuilder.DropTable(
                name: "Batterys");

            migrationBuilder.DropTable(
                name: "Trays");
        }
    }
}
