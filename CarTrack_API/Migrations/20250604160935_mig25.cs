using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarTrack_API.Migrations
{
    /// <inheritdoc />
    public partial class mig25 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "MaintenanceUnverifiedRecord",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "DoneMileage",
                table: "MaintenanceUnverifiedRecord",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "MaintenanceUnverifiedRecord");

            migrationBuilder.DropColumn(
                name: "DoneMileage",
                table: "MaintenanceUnverifiedRecord");
        }
    }
}
