using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Com.RePower.Ocv.Model.Migrations.LocalTestResultDb
{
    /// <inheritdoc />
    public partial class AddOcvStationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OcvStationName",
                table: "Batterys",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OcvStationName",
                table: "Batterys");
        }
    }
}
