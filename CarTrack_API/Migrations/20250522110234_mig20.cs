using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarTrack_API.Migrations
{
    /// <inheritdoc />
    public partial class mig20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AverageTravelDistance",
                table: "VehicleInfo",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageTravelDistance",
                table: "VehicleInfo");
        }
    }
}
