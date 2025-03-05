using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CarTrack_API.Migrations
{
    /// <inheritdoc />
    public partial class Modified1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Services_ServiceId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Users_MechanicId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Users_UserId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceRecords_Services_ServiceId",
                table: "MaintenanceRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceRecords_Users_MechanicId",
                table: "MaintenanceRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceRecords_Users_UserId",
                table: "MaintenanceRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_VehiclePapers_Users_UserId",
                table: "VehiclePapers");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Engines_EngineId",
                table: "Vehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Users_UserId",
                table: "Vehicles");

            migrationBuilder.DropTable(
                name: "Engines");

            migrationBuilder.DropTable(
                name: "Worker_Service");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropIndex(
                name: "IX_VehiclePapers_UserId",
                table: "VehiclePapers");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceRecords_MechanicId",
                table: "MaintenanceRecords");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceRecords_ServiceId",
                table: "MaintenanceRecords");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceRecords_UserId",
                table: "MaintenanceRecords");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceRecords_VehicleId",
                table: "MaintenanceRecords");

            migrationBuilder.DropColumn(
                name: "BodyType",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "VehiclePapers");

            migrationBuilder.DropColumn(
                name: "VehicleSlots",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "MaintenanceRecords");

            migrationBuilder.DropColumn(
                name: "MechanicId",
                table: "MaintenanceRecords");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "MaintenanceRecords");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MaintenanceRecords");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Vehicles",
                newName: "VehicleModelId");

            migrationBuilder.RenameColumn(
                name: "EngineId",
                table: "Vehicles",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_UserId",
                table: "Vehicles",
                newName: "IX_Vehicles_VehicleModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_EngineId",
                table: "Vehicles",
                newName: "IX_Vehicles_ClientId");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Users",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "MaintenanceRecords",
                newName: "LastMaintenanceDate");

            migrationBuilder.RenameColumn(
                name: "Cost",
                table: "MaintenanceRecords",
                newName: "TotalCost");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Appointments",
                newName: "RepairShopId");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "Appointments",
                newName: "MaintenanceRecordId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_UserId",
                table: "Appointments",
                newName: "IX_Appointments_RepairShopId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_ServiceId",
                table: "Appointments",
                newName: "IX_Appointments_MaintenanceRecordId");

            migrationBuilder.AddColumn<int>(
                name: "RepairShopId",
                table: "MaintenanceRecords",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ClientProfile",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientProfile", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_ClientProfile_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ManagerProfile",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagerProfile", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_ManagerProfile_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleEngines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EngineType = table.Column<string>(type: "text", nullable: false),
                    FuelType = table.Column<string>(type: "text", nullable: false),
                    Size = table.Column<string>(type: "text", nullable: false),
                    HorsePower = table.Column<int>(type: "integer", nullable: false),
                    Torque = table.Column<int>(type: "integer", nullable: false),
                    Cylinders = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleEngines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RepairShops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ManagerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairShops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RepairShops_ManagerProfile_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "ManagerProfile",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Series = table.Column<string>(type: "text", nullable: false),
                    Brand = table.Column<string>(type: "text", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    BodyType = table.Column<string>(type: "text", nullable: false),
                    TransmissionType = table.Column<string>(type: "text", nullable: false),
                    VehicleEngineId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleModels_VehicleEngines_VehicleEngineId",
                        column: x => x.VehicleEngineId,
                        principalTable: "VehicleEngines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Deal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    RepairShopId = table.Column<int>(type: "integer", nullable: false),
                    AppointmentId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deal_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Deal_RepairShops_RepairShopId",
                        column: x => x.RepairShopId,
                        principalTable: "RepairShops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MechanicProfile",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RepairShopId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MechanicProfile", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_MechanicProfile_RepairShops_RepairShopId",
                        column: x => x.RepairShopId,
                        principalTable: "RepairShops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MechanicProfile_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRecords_RepairShopId",
                table: "MaintenanceRecords",
                column: "RepairShopId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRecords_VehicleId",
                table: "MaintenanceRecords",
                column: "VehicleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deal_AppointmentId",
                table: "Deal",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Deal_RepairShopId",
                table: "Deal",
                column: "RepairShopId");

            migrationBuilder.CreateIndex(
                name: "IX_MechanicProfile_RepairShopId",
                table: "MechanicProfile",
                column: "RepairShopId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairShops_ManagerId",
                table: "RepairShops",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleModels_VehicleEngineId",
                table: "VehicleModels",
                column: "VehicleEngineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_MaintenanceRecords_MaintenanceRecordId",
                table: "Appointments",
                column: "MaintenanceRecordId",
                principalTable: "MaintenanceRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_MechanicProfile_MechanicId",
                table: "Appointments",
                column: "MechanicId",
                principalTable: "MechanicProfile",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_RepairShops_RepairShopId",
                table: "Appointments",
                column: "RepairShopId",
                principalTable: "RepairShops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceRecords_RepairShops_RepairShopId",
                table: "MaintenanceRecords",
                column: "RepairShopId",
                principalTable: "RepairShops",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_ClientProfile_ClientId",
                table: "Vehicles",
                column: "ClientId",
                principalTable: "ClientProfile",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_VehicleModels_VehicleModelId",
                table: "Vehicles",
                column: "VehicleModelId",
                principalTable: "VehicleModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_MaintenanceRecords_MaintenanceRecordId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_MechanicProfile_MechanicId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_RepairShops_RepairShopId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceRecords_RepairShops_RepairShopId",
                table: "MaintenanceRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_ClientProfile_ClientId",
                table: "Vehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_VehicleModels_VehicleModelId",
                table: "Vehicles");

            migrationBuilder.DropTable(
                name: "ClientProfile");

            migrationBuilder.DropTable(
                name: "Deal");

            migrationBuilder.DropTable(
                name: "MechanicProfile");

            migrationBuilder.DropTable(
                name: "VehicleModels");

            migrationBuilder.DropTable(
                name: "RepairShops");

            migrationBuilder.DropTable(
                name: "VehicleEngines");

            migrationBuilder.DropTable(
                name: "ManagerProfile");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceRecords_RepairShopId",
                table: "MaintenanceRecords");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceRecords_VehicleId",
                table: "MaintenanceRecords");

            migrationBuilder.DropColumn(
                name: "RepairShopId",
                table: "MaintenanceRecords");

            migrationBuilder.RenameColumn(
                name: "VehicleModelId",
                table: "Vehicles",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Vehicles",
                newName: "EngineId");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_VehicleModelId",
                table: "Vehicles",
                newName: "IX_Vehicles_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_ClientId",
                table: "Vehicles",
                newName: "IX_Vehicles_EngineId");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Users",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "TotalCost",
                table: "MaintenanceRecords",
                newName: "Cost");

            migrationBuilder.RenameColumn(
                name: "LastMaintenanceDate",
                table: "MaintenanceRecords",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "RepairShopId",
                table: "Appointments",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "MaintenanceRecordId",
                table: "Appointments",
                newName: "ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_RepairShopId",
                table: "Appointments",
                newName: "IX_Appointments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_MaintenanceRecordId",
                table: "Appointments",
                newName: "IX_Appointments_ServiceId");

            migrationBuilder.AddColumn<string>(
                name: "BodyType",
                table: "Vehicles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Vehicles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Vehicles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Year",
                table: "Vehicles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "VehiclePapers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VehicleSlots",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "MaintenanceRecords",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MechanicId",
                table: "MaintenanceRecords",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "MaintenanceRecords",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "MaintenanceRecords",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Engines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Cylinders = table.Column<int>(type: "integer", nullable: false),
                    FuelType = table.Column<string>(type: "text", nullable: false),
                    HorsePower = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Torque = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Engines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContactEmail = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Worker_Service",
                columns: table => new
                {
                    ServicesId = table.Column<int>(type: "integer", nullable: false),
                    WorkersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Worker_Service", x => new { x.ServicesId, x.WorkersId });
                    table.ForeignKey(
                        name: "FK_Worker_Service_Services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Worker_Service_Users_WorkersId",
                        column: x => x.WorkersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehiclePapers_UserId",
                table: "VehiclePapers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRecords_MechanicId",
                table: "MaintenanceRecords",
                column: "MechanicId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRecords_ServiceId",
                table: "MaintenanceRecords",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRecords_UserId",
                table: "MaintenanceRecords",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRecords_VehicleId",
                table: "MaintenanceRecords",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Worker_Service_WorkersId",
                table: "Worker_Service",
                column: "WorkersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Services_ServiceId",
                table: "Appointments",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Users_MechanicId",
                table: "Appointments",
                column: "MechanicId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Users_UserId",
                table: "Appointments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceRecords_Services_ServiceId",
                table: "MaintenanceRecords",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceRecords_Users_MechanicId",
                table: "MaintenanceRecords",
                column: "MechanicId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceRecords_Users_UserId",
                table: "MaintenanceRecords",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VehiclePapers_Users_UserId",
                table: "VehiclePapers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Engines_EngineId",
                table: "Vehicles",
                column: "EngineId",
                principalTable: "Engines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Users_UserId",
                table: "Vehicles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
