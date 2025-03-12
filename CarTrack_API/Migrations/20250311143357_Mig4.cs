using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CarTrack_API.Migrations
{
    /// <inheritdoc />
    public partial class Mig4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentDeal_Deal_DealId",
                table: "AppointmentDeal");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_MechanicProfile_MechanicId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientProfile_Users_UserId",
                table: "ClientProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_Deal_RepairShops_RepairShopId",
                table: "Deal");

            migrationBuilder.DropForeignKey(
                name: "FK_ManagerProfile_Users_UserId",
                table: "ManagerProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_MechanicProfile_RepairShops_RepairShopId",
                table: "MechanicProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_MechanicProfile_Users_UserId",
                table: "MechanicProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairShops_ManagerProfile_ManagerId",
                table: "RepairShops");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_ClientProfile_ClientId",
                table: "Vehicles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MechanicProfile",
                table: "MechanicProfile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ManagerProfile",
                table: "ManagerProfile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Deal",
                table: "Deal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientProfile",
                table: "ClientProfile");

            migrationBuilder.DropColumn(
                name: "BodyType",
                table: "VehicleModels");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "VehicleModels");

            migrationBuilder.DropColumn(
                name: "Series",
                table: "VehicleModels");

            migrationBuilder.RenameTable(
                name: "MechanicProfile",
                newName: "MechanicProfiles");

            migrationBuilder.RenameTable(
                name: "ManagerProfile",
                newName: "ManagerProfiles");

            migrationBuilder.RenameTable(
                name: "Deal",
                newName: "Deals");

            migrationBuilder.RenameTable(
                name: "ClientProfile",
                newName: "ClientProfiles");

            migrationBuilder.RenameColumn(
                name: "WheelDriveType",
                table: "VehicleModels",
                newName: "SeriesName");

            migrationBuilder.RenameColumn(
                name: "TransmissionType",
                table: "VehicleModels",
                newName: "ModelFullName");

            migrationBuilder.RenameColumn(
                name: "DoorNumber",
                table: "VehicleModels",
                newName: "ProducerId");

            migrationBuilder.RenameColumn(
                name: "Torque",
                table: "VehicleEngines",
                newName: "TorqueFtLbs");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "VehicleEngines",
                newName: "Transmission");

            migrationBuilder.RenameIndex(
                name: "IX_MechanicProfile_RepairShopId",
                table: "MechanicProfiles",
                newName: "IX_MechanicProfiles_RepairShopId");

            migrationBuilder.RenameIndex(
                name: "IX_Deal_RepairShopId",
                table: "Deals",
                newName: "IX_Deals_RepairShopId");

            migrationBuilder.AddColumn<int>(
                name: "BodyId",
                table: "VehicleModels",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Consumption",
                table: "VehicleModels",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<double>(
                name: "Size",
                table: "VehicleEngines",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Cylinders",
                table: "VehicleEngines",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "DriveType",
                table: "VehicleEngines",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MechanicProfiles",
                table: "MechanicProfiles",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ManagerProfiles",
                table: "ManagerProfiles",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Deals",
                table: "Deals",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientProfiles",
                table: "ClientProfiles",
                column: "UserId");

            migrationBuilder.CreateTable(
                name: "Bodies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BodyType = table.Column<string>(type: "text", nullable: false),
                    DoorNumber = table.Column<int>(type: "integer", nullable: false),
                    SeatNumber = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bodies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Producers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleModels_BodyId",
                table: "VehicleModels",
                column: "BodyId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleModels_ProducerId",
                table: "VehicleModels",
                column: "ProducerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentDeal_Deals_DealId",
                table: "AppointmentDeal",
                column: "DealId",
                principalTable: "Deals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_MechanicProfiles_MechanicId",
                table: "Appointments",
                column: "MechanicId",
                principalTable: "MechanicProfiles",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientProfiles_Users_UserId",
                table: "ClientProfiles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_RepairShops_RepairShopId",
                table: "Deals",
                column: "RepairShopId",
                principalTable: "RepairShops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ManagerProfiles_Users_UserId",
                table: "ManagerProfiles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MechanicProfiles_RepairShops_RepairShopId",
                table: "MechanicProfiles",
                column: "RepairShopId",
                principalTable: "RepairShops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MechanicProfiles_Users_UserId",
                table: "MechanicProfiles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RepairShops_ManagerProfiles_ManagerId",
                table: "RepairShops",
                column: "ManagerId",
                principalTable: "ManagerProfiles",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleModels_Bodies_BodyId",
                table: "VehicleModels",
                column: "BodyId",
                principalTable: "Bodies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleModels_Producers_ProducerId",
                table: "VehicleModels",
                column: "ProducerId",
                principalTable: "Producers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_ClientProfiles_ClientId",
                table: "Vehicles",
                column: "ClientId",
                principalTable: "ClientProfiles",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentDeal_Deals_DealId",
                table: "AppointmentDeal");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_MechanicProfiles_MechanicId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientProfiles_Users_UserId",
                table: "ClientProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Deals_RepairShops_RepairShopId",
                table: "Deals");

            migrationBuilder.DropForeignKey(
                name: "FK_ManagerProfiles_Users_UserId",
                table: "ManagerProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_MechanicProfiles_RepairShops_RepairShopId",
                table: "MechanicProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_MechanicProfiles_Users_UserId",
                table: "MechanicProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairShops_ManagerProfiles_ManagerId",
                table: "RepairShops");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleModels_Bodies_BodyId",
                table: "VehicleModels");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleModels_Producers_ProducerId",
                table: "VehicleModels");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_ClientProfiles_ClientId",
                table: "Vehicles");

            migrationBuilder.DropTable(
                name: "Bodies");

            migrationBuilder.DropTable(
                name: "Producers");

            migrationBuilder.DropIndex(
                name: "IX_VehicleModels_BodyId",
                table: "VehicleModels");

            migrationBuilder.DropIndex(
                name: "IX_VehicleModels_ProducerId",
                table: "VehicleModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MechanicProfiles",
                table: "MechanicProfiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ManagerProfiles",
                table: "ManagerProfiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Deals",
                table: "Deals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientProfiles",
                table: "ClientProfiles");

            migrationBuilder.DropColumn(
                name: "BodyId",
                table: "VehicleModels");

            migrationBuilder.DropColumn(
                name: "Consumption",
                table: "VehicleModels");

            migrationBuilder.DropColumn(
                name: "DriveType",
                table: "VehicleEngines");

            migrationBuilder.RenameTable(
                name: "MechanicProfiles",
                newName: "MechanicProfile");

            migrationBuilder.RenameTable(
                name: "ManagerProfiles",
                newName: "ManagerProfile");

            migrationBuilder.RenameTable(
                name: "Deals",
                newName: "Deal");

            migrationBuilder.RenameTable(
                name: "ClientProfiles",
                newName: "ClientProfile");

            migrationBuilder.RenameColumn(
                name: "SeriesName",
                table: "VehicleModels",
                newName: "WheelDriveType");

            migrationBuilder.RenameColumn(
                name: "ProducerId",
                table: "VehicleModels",
                newName: "DoorNumber");

            migrationBuilder.RenameColumn(
                name: "ModelFullName",
                table: "VehicleModels",
                newName: "TransmissionType");

            migrationBuilder.RenameColumn(
                name: "Transmission",
                table: "VehicleEngines",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "TorqueFtLbs",
                table: "VehicleEngines",
                newName: "Torque");

            migrationBuilder.RenameIndex(
                name: "IX_MechanicProfiles_RepairShopId",
                table: "MechanicProfile",
                newName: "IX_MechanicProfile_RepairShopId");

            migrationBuilder.RenameIndex(
                name: "IX_Deals_RepairShopId",
                table: "Deal",
                newName: "IX_Deal_RepairShopId");

            migrationBuilder.AddColumn<string>(
                name: "BodyType",
                table: "VehicleModels",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "VehicleModels",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Series",
                table: "VehicleModels",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Size",
                table: "VehicleEngines",
                type: "text",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<int>(
                name: "Cylinders",
                table: "VehicleEngines",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MechanicProfile",
                table: "MechanicProfile",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ManagerProfile",
                table: "ManagerProfile",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Deal",
                table: "Deal",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientProfile",
                table: "ClientProfile",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentDeal_Deal_DealId",
                table: "AppointmentDeal",
                column: "DealId",
                principalTable: "Deal",
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
                name: "FK_ClientProfile_Users_UserId",
                table: "ClientProfile",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Deal_RepairShops_RepairShopId",
                table: "Deal",
                column: "RepairShopId",
                principalTable: "RepairShops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ManagerProfile_Users_UserId",
                table: "ManagerProfile",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MechanicProfile_RepairShops_RepairShopId",
                table: "MechanicProfile",
                column: "RepairShopId",
                principalTable: "RepairShops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MechanicProfile_Users_UserId",
                table: "MechanicProfile",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RepairShops_ManagerProfile_ManagerId",
                table: "RepairShops",
                column: "ManagerId",
                principalTable: "ManagerProfile",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_ClientProfile_ClientId",
                table: "Vehicles",
                column: "ClientId",
                principalTable: "ClientProfile",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
