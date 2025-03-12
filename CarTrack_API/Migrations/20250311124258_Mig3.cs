using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarTrack_API.Migrations
{
    /// <inheritdoc />
    public partial class Mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DoorNumber",
                table: "VehicleModels",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FuelTankCapacity",
                table: "VehicleModels",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "WheelDriveType",
                table: "VehicleModels",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoorNumber",
                table: "VehicleModels");

            migrationBuilder.DropColumn(
                name: "FuelTankCapacity",
                table: "VehicleModels");

            migrationBuilder.DropColumn(
                name: "WheelDriveType",
                table: "VehicleModels");
        }
    }
}
