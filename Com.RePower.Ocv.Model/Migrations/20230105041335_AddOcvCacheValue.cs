using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Com.RePower.Ocv.Model.Migrations
{
    /// <inheritdoc />
    public partial class AddOcvCacheValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CacheValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SettingName = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true),
                    ReserveText1 = table.Column<string>(type: "TEXT", nullable: true),
                    ReserveText2 = table.Column<string>(type: "TEXT", nullable: true),
                    ReserveText3 = table.Column<string>(type: "TEXT", nullable: true),
                    ReserveTime1 = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ReserveTime2 = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ReserveTime3 = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CacheValues", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CacheValues");
        }
    }
}
