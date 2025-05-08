using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CarTrack_API.Migrations
{
    /// <inheritdoc />
    public partial class mig18 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleMaintenanceConfig_MaintenanceCategory_MaintenanceCat~",
                table: "VehicleMaintenanceConfig");

            migrationBuilder.DropTable(
                name: "MaintenanceCategory");

            migrationBuilder.DropIndex(
                name: "IX_VehicleMaintenanceConfig_MaintenanceCategoryId",
                table: "VehicleMaintenanceConfig");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceRecord_VehicleId",
                table: "MaintenanceRecord");

            migrationBuilder.DropColumn(
                name: "MaintenanceCategoryId",
                table: "VehicleMaintenanceConfig");

            migrationBuilder.DropColumn(
                name: "LastUpdate",
                table: "VehicleInfo");

            migrationBuilder.DropColumn(
                name: "TravelDistanceAVG",
                table: "VehicleInfo");

            migrationBuilder.DropColumn(
                name: "Vin",
                table: "Vehicle");

            migrationBuilder.RenameColumn(
                name: "TotalCost",
                table: "MaintenanceRecord",
                newName: "Cost");

            migrationBuilder.RenameColumn(
                name: "LastMaintenanceDate",
                table: "MaintenanceRecord",
                newName: "DoneDate");

            migrationBuilder.AlterColumn<int>(
                name: "MileageIntervalConfig",
                table: "VehicleMaintenanceConfig",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DateIntervalConfig",
                table: "VehicleMaintenanceConfig",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Vin",
                table: "VehicleInfo",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "RepairShop",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<double>(
                name: "LastMileageCkeck",
                table: "Reminder",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastDateCheck",
                table: "Reminder",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "MaintenanceNames",
                table: "MaintenanceRecord",
                type: "text[]",
                nullable: false);

            migrationBuilder.CreateTable(
                name: "MaintenanceUnverifiedRecord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DoneDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Cost = table.Column<decimal>(type: "numeric", nullable: false),
                    VehicleId = table.Column<int>(type: "integer", nullable: false),
                    MaintenanceNames = table.Column<List<string>>(type: "text[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceUnverifiedRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaintenanceUnverifiedRecord_Vehicle_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRecord_VehicleId",
                table: "MaintenanceRecord",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceUnverifiedRecord_VehicleId",
                table: "MaintenanceUnverifiedRecord",
                column: "VehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaintenanceUnverifiedRecord");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceRecord_VehicleId",
                table: "MaintenanceRecord");

            migrationBuilder.DropColumn(
                name: "Vin",
                table: "VehicleInfo");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "RepairShop");

            migrationBuilder.DropColumn(
                name: "MaintenanceNames",
                table: "MaintenanceRecord");

            migrationBuilder.RenameColumn(
                name: "DoneDate",
                table: "MaintenanceRecord",
                newName: "LastMaintenanceDate");

            migrationBuilder.RenameColumn(
                name: "Cost",
                table: "MaintenanceRecord",
                newName: "TotalCost");

            migrationBuilder.AlterColumn<int>(
                name: "MileageIntervalConfig",
                table: "VehicleMaintenanceConfig",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "DateIntervalConfig",
                table: "VehicleMaintenanceConfig",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "MaintenanceCategoryId",
                table: "VehicleMaintenanceConfig",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdate",
                table: "VehicleInfo",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "TravelDistanceAVG",
                table: "VehicleInfo",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Vin",
                table: "Vehicle",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<double>(
                name: "LastMileageCkeck",
                table: "Reminder",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastDateCheck",
                table: "Reminder",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.CreateTable(
                name: "MaintenanceCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceCategory", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleMaintenanceConfig_MaintenanceCategoryId",
                table: "VehicleMaintenanceConfig",
                column: "MaintenanceCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRecord_VehicleId",
                table: "MaintenanceRecord",
                column: "VehicleId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleMaintenanceConfig_MaintenanceCategory_MaintenanceCat~",
                table: "VehicleMaintenanceConfig",
                column: "MaintenanceCategoryId",
                principalTable: "MaintenanceCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
