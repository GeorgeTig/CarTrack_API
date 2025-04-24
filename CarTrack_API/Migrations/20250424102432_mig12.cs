using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CarTrack_API.Migrations
{
    /// <inheritdoc />
    public partial class mig12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Mileage",
                table: "VehicleInfo",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

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

            migrationBuilder.CreateTable(
                name: "MaintenanceType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleUsageStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Distance = table.Column<double>(type: "double precision", nullable: false),
                    VehicleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleUsageStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleUsageStats_Vehicle_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleMaintenanceConfig",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DateIntervalConfig = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MileageIntervalConfig = table.Column<double>(type: "double precision", nullable: false),
                    IsEditable = table.Column<bool>(type: "boolean", nullable: false),
                    MaintenanceTypeId = table.Column<int>(type: "integer", nullable: false),
                    MaintenanceCategoryId = table.Column<int>(type: "integer", nullable: false),
                    VehicleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleMaintenanceConfig", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleMaintenanceConfig_MaintenanceCategory_MaintenanceCat~",
                        column: x => x.MaintenanceCategoryId,
                        principalTable: "MaintenanceCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VehicleMaintenanceConfig_MaintenanceType_MaintenanceTypeId",
                        column: x => x.MaintenanceTypeId,
                        principalTable: "MaintenanceType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VehicleMaintenanceConfig_Vehicle_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleMaintenanceConfig_MaintenanceCategoryId",
                table: "VehicleMaintenanceConfig",
                column: "MaintenanceCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleMaintenanceConfig_MaintenanceTypeId",
                table: "VehicleMaintenanceConfig",
                column: "MaintenanceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleMaintenanceConfig_VehicleId",
                table: "VehicleMaintenanceConfig",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleUsageStats_VehicleId",
                table: "VehicleUsageStats",
                column: "VehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleMaintenanceConfig");

            migrationBuilder.DropTable(
                name: "VehicleUsageStats");

            migrationBuilder.DropTable(
                name: "MaintenanceCategory");

            migrationBuilder.DropTable(
                name: "MaintenanceType");

            migrationBuilder.AlterColumn<int>(
                name: "Mileage",
                table: "VehicleInfo",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");
        }
    }
}
