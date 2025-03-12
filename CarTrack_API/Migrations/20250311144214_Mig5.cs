using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarTrack_API.Migrations
{
    /// <inheritdoc />
    public partial class Mig5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentDeal_Appointments_AppointmentId",
                table: "AppointmentDeal");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentDeal_Deals_DealId",
                table: "AppointmentDeal");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_MaintenanceRecords_MaintenanceRecordId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_MechanicProfiles_MechanicId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_RepairShops_RepairShopId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Vehicles_VehicleId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientProfiles_Users_UserId",
                table: "ClientProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Deals_RepairShops_RepairShopId",
                table: "Deals");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceRecords_RepairShops_RepairShopId",
                table: "MaintenanceRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceRecords_Vehicles_VehicleId",
                table: "MaintenanceRecords");

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
                name: "FK_Notifications_Users_UserId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairShops_ManagerProfiles_ManagerId",
                table: "RepairShops");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserRoles_RoleId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleModels_Bodies_BodyId",
                table: "VehicleModels");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleModels_Producers_ProducerId",
                table: "VehicleModels");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleModels_VehicleEngines_VehicleEngineId",
                table: "VehicleModels");

            migrationBuilder.DropForeignKey(
                name: "FK_VehiclePapers_Vehicles_VehicleId",
                table: "VehiclePapers");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_ClientProfiles_ClientId",
                table: "Vehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_VehicleModels_VehicleModelId",
                table: "Vehicles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehicles",
                table: "Vehicles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehiclePapers",
                table: "VehiclePapers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleModels",
                table: "VehicleModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleEngines",
                table: "VehicleEngines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RepairShops",
                table: "RepairShops");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Producers",
                table: "Producers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MechanicProfiles",
                table: "MechanicProfiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ManagerProfiles",
                table: "ManagerProfiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MaintenanceRecords",
                table: "MaintenanceRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Deals",
                table: "Deals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientProfiles",
                table: "ClientProfiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bodies",
                table: "Bodies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments");

            migrationBuilder.RenameTable(
                name: "Vehicles",
                newName: "Vehicle");

            migrationBuilder.RenameTable(
                name: "VehiclePapers",
                newName: "VehiclePaper");

            migrationBuilder.RenameTable(
                name: "VehicleModels",
                newName: "VehicleModel");

            migrationBuilder.RenameTable(
                name: "VehicleEngines",
                newName: "VehicleEngine");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                newName: "UserRole");

            migrationBuilder.RenameTable(
                name: "RepairShops",
                newName: "RepairShop");

            migrationBuilder.RenameTable(
                name: "Producers",
                newName: "Producer");

            migrationBuilder.RenameTable(
                name: "Notifications",
                newName: "Notification");

            migrationBuilder.RenameTable(
                name: "MechanicProfiles",
                newName: "MechanicProfile");

            migrationBuilder.RenameTable(
                name: "ManagerProfiles",
                newName: "ManagerProfile");

            migrationBuilder.RenameTable(
                name: "MaintenanceRecords",
                newName: "MaintenanceRecord");

            migrationBuilder.RenameTable(
                name: "Deals",
                newName: "Deal");

            migrationBuilder.RenameTable(
                name: "ClientProfiles",
                newName: "ClientProfile");

            migrationBuilder.RenameTable(
                name: "Bodies",
                newName: "Bodie");

            migrationBuilder.RenameTable(
                name: "Appointments",
                newName: "Appointment");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_VehicleModelId",
                table: "Vehicle",
                newName: "IX_Vehicle_VehicleModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_ClientId",
                table: "Vehicle",
                newName: "IX_Vehicle_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_VehiclePapers_VehicleId",
                table: "VehiclePaper",
                newName: "IX_VehiclePaper_VehicleId");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleModels_VehicleEngineId",
                table: "VehicleModel",
                newName: "IX_VehicleModel_VehicleEngineId");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleModels_ProducerId",
                table: "VehicleModel",
                newName: "IX_VehicleModel_ProducerId");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleModels_BodyId",
                table: "VehicleModel",
                newName: "IX_VehicleModel_BodyId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_RoleId",
                table: "User",
                newName: "IX_User_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_RepairShops_ManagerId",
                table: "RepairShop",
                newName: "IX_RepairShop_ManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_UserId",
                table: "Notification",
                newName: "IX_Notification_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_MechanicProfiles_RepairShopId",
                table: "MechanicProfile",
                newName: "IX_MechanicProfile_RepairShopId");

            migrationBuilder.RenameIndex(
                name: "IX_MaintenanceRecords_VehicleId",
                table: "MaintenanceRecord",
                newName: "IX_MaintenanceRecord_VehicleId");

            migrationBuilder.RenameIndex(
                name: "IX_MaintenanceRecords_RepairShopId",
                table: "MaintenanceRecord",
                newName: "IX_MaintenanceRecord_RepairShopId");

            migrationBuilder.RenameIndex(
                name: "IX_Deals_RepairShopId",
                table: "Deal",
                newName: "IX_Deal_RepairShopId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_VehicleId",
                table: "Appointment",
                newName: "IX_Appointment_VehicleId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_RepairShopId",
                table: "Appointment",
                newName: "IX_Appointment_RepairShopId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_MechanicId",
                table: "Appointment",
                newName: "IX_Appointment_MechanicId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_MaintenanceRecordId",
                table: "Appointment",
                newName: "IX_Appointment_MaintenanceRecordId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehicle",
                table: "Vehicle",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehiclePaper",
                table: "VehiclePaper",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleModel",
                table: "VehicleModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleEngine",
                table: "VehicleEngine",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RepairShop",
                table: "RepairShop",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Producer",
                table: "Producer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notification",
                table: "Notification",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MechanicProfile",
                table: "MechanicProfile",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ManagerProfile",
                table: "ManagerProfile",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaintenanceRecord",
                table: "MaintenanceRecord",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Deal",
                table: "Deal",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientProfile",
                table: "ClientProfile",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bodie",
                table: "Bodie",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointment",
                table: "Appointment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_MaintenanceRecord_MaintenanceRecordId",
                table: "Appointment",
                column: "MaintenanceRecordId",
                principalTable: "MaintenanceRecord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_MechanicProfile_MechanicId",
                table: "Appointment",
                column: "MechanicId",
                principalTable: "MechanicProfile",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_RepairShop_RepairShopId",
                table: "Appointment",
                column: "RepairShopId",
                principalTable: "RepairShop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Vehicle_VehicleId",
                table: "Appointment",
                column: "VehicleId",
                principalTable: "Vehicle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentDeal_Appointment_AppointmentId",
                table: "AppointmentDeal",
                column: "AppointmentId",
                principalTable: "Appointment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentDeal_Deal_DealId",
                table: "AppointmentDeal",
                column: "DealId",
                principalTable: "Deal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientProfile_User_UserId",
                table: "ClientProfile",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Deal_RepairShop_RepairShopId",
                table: "Deal",
                column: "RepairShopId",
                principalTable: "RepairShop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceRecord_RepairShop_RepairShopId",
                table: "MaintenanceRecord",
                column: "RepairShopId",
                principalTable: "RepairShop",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceRecord_Vehicle_VehicleId",
                table: "MaintenanceRecord",
                column: "VehicleId",
                principalTable: "Vehicle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ManagerProfile_User_UserId",
                table: "ManagerProfile",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MechanicProfile_RepairShop_RepairShopId",
                table: "MechanicProfile",
                column: "RepairShopId",
                principalTable: "RepairShop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MechanicProfile_User_UserId",
                table: "MechanicProfile",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_User_UserId",
                table: "Notification",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RepairShop_ManagerProfile_ManagerId",
                table: "RepairShop",
                column: "ManagerId",
                principalTable: "ManagerProfile",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_UserRole_RoleId",
                table: "User",
                column: "RoleId",
                principalTable: "UserRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_ClientProfile_ClientId",
                table: "Vehicle",
                column: "ClientId",
                principalTable: "ClientProfile",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_VehicleModel_VehicleModelId",
                table: "Vehicle",
                column: "VehicleModelId",
                principalTable: "VehicleModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleModel_Bodie_BodyId",
                table: "VehicleModel",
                column: "BodyId",
                principalTable: "Bodie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleModel_Producer_ProducerId",
                table: "VehicleModel",
                column: "ProducerId",
                principalTable: "Producer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleModel_VehicleEngine_VehicleEngineId",
                table: "VehicleModel",
                column: "VehicleEngineId",
                principalTable: "VehicleEngine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VehiclePaper_Vehicle_VehicleId",
                table: "VehiclePaper",
                column: "VehicleId",
                principalTable: "Vehicle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_MaintenanceRecord_MaintenanceRecordId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_MechanicProfile_MechanicId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_RepairShop_RepairShopId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Vehicle_VehicleId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentDeal_Appointment_AppointmentId",
                table: "AppointmentDeal");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentDeal_Deal_DealId",
                table: "AppointmentDeal");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientProfile_User_UserId",
                table: "ClientProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_Deal_RepairShop_RepairShopId",
                table: "Deal");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceRecord_RepairShop_RepairShopId",
                table: "MaintenanceRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceRecord_Vehicle_VehicleId",
                table: "MaintenanceRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_ManagerProfile_User_UserId",
                table: "ManagerProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_MechanicProfile_RepairShop_RepairShopId",
                table: "MechanicProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_MechanicProfile_User_UserId",
                table: "MechanicProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_User_UserId",
                table: "Notification");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairShop_ManagerProfile_ManagerId",
                table: "RepairShop");

            migrationBuilder.DropForeignKey(
                name: "FK_User_UserRole_RoleId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_ClientProfile_ClientId",
                table: "Vehicle");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_VehicleModel_VehicleModelId",
                table: "Vehicle");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleModel_Bodie_BodyId",
                table: "VehicleModel");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleModel_Producer_ProducerId",
                table: "VehicleModel");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleModel_VehicleEngine_VehicleEngineId",
                table: "VehicleModel");

            migrationBuilder.DropForeignKey(
                name: "FK_VehiclePaper_Vehicle_VehicleId",
                table: "VehiclePaper");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehiclePaper",
                table: "VehiclePaper");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleModel",
                table: "VehicleModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleEngine",
                table: "VehicleEngine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehicle",
                table: "Vehicle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RepairShop",
                table: "RepairShop");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Producer",
                table: "Producer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notification",
                table: "Notification");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MechanicProfile",
                table: "MechanicProfile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ManagerProfile",
                table: "ManagerProfile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MaintenanceRecord",
                table: "MaintenanceRecord");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Deal",
                table: "Deal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientProfile",
                table: "ClientProfile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bodie",
                table: "Bodie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointment",
                table: "Appointment");

            migrationBuilder.RenameTable(
                name: "VehiclePaper",
                newName: "VehiclePapers");

            migrationBuilder.RenameTable(
                name: "VehicleModel",
                newName: "VehicleModels");

            migrationBuilder.RenameTable(
                name: "VehicleEngine",
                newName: "VehicleEngines");

            migrationBuilder.RenameTable(
                name: "Vehicle",
                newName: "Vehicles");

            migrationBuilder.RenameTable(
                name: "UserRole",
                newName: "UserRoles");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "RepairShop",
                newName: "RepairShops");

            migrationBuilder.RenameTable(
                name: "Producer",
                newName: "Producers");

            migrationBuilder.RenameTable(
                name: "Notification",
                newName: "Notifications");

            migrationBuilder.RenameTable(
                name: "MechanicProfile",
                newName: "MechanicProfiles");

            migrationBuilder.RenameTable(
                name: "ManagerProfile",
                newName: "ManagerProfiles");

            migrationBuilder.RenameTable(
                name: "MaintenanceRecord",
                newName: "MaintenanceRecords");

            migrationBuilder.RenameTable(
                name: "Deal",
                newName: "Deals");

            migrationBuilder.RenameTable(
                name: "ClientProfile",
                newName: "ClientProfiles");

            migrationBuilder.RenameTable(
                name: "Bodie",
                newName: "Bodies");

            migrationBuilder.RenameTable(
                name: "Appointment",
                newName: "Appointments");

            migrationBuilder.RenameIndex(
                name: "IX_VehiclePaper_VehicleId",
                table: "VehiclePapers",
                newName: "IX_VehiclePapers_VehicleId");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleModel_VehicleEngineId",
                table: "VehicleModels",
                newName: "IX_VehicleModels_VehicleEngineId");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleModel_ProducerId",
                table: "VehicleModels",
                newName: "IX_VehicleModels_ProducerId");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleModel_BodyId",
                table: "VehicleModels",
                newName: "IX_VehicleModels_BodyId");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicle_VehicleModelId",
                table: "Vehicles",
                newName: "IX_Vehicles_VehicleModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicle_ClientId",
                table: "Vehicles",
                newName: "IX_Vehicles_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_User_RoleId",
                table: "Users",
                newName: "IX_Users_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_RepairShop_ManagerId",
                table: "RepairShops",
                newName: "IX_RepairShops_ManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_Notification_UserId",
                table: "Notifications",
                newName: "IX_Notifications_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_MechanicProfile_RepairShopId",
                table: "MechanicProfiles",
                newName: "IX_MechanicProfiles_RepairShopId");

            migrationBuilder.RenameIndex(
                name: "IX_MaintenanceRecord_VehicleId",
                table: "MaintenanceRecords",
                newName: "IX_MaintenanceRecords_VehicleId");

            migrationBuilder.RenameIndex(
                name: "IX_MaintenanceRecord_RepairShopId",
                table: "MaintenanceRecords",
                newName: "IX_MaintenanceRecords_RepairShopId");

            migrationBuilder.RenameIndex(
                name: "IX_Deal_RepairShopId",
                table: "Deals",
                newName: "IX_Deals_RepairShopId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointment_VehicleId",
                table: "Appointments",
                newName: "IX_Appointments_VehicleId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointment_RepairShopId",
                table: "Appointments",
                newName: "IX_Appointments_RepairShopId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointment_MechanicId",
                table: "Appointments",
                newName: "IX_Appointments_MechanicId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointment_MaintenanceRecordId",
                table: "Appointments",
                newName: "IX_Appointments_MaintenanceRecordId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehiclePapers",
                table: "VehiclePapers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleModels",
                table: "VehicleModels",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleEngines",
                table: "VehicleEngines",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehicles",
                table: "Vehicles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RepairShops",
                table: "RepairShops",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Producers",
                table: "Producers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MechanicProfiles",
                table: "MechanicProfiles",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ManagerProfiles",
                table: "ManagerProfiles",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaintenanceRecords",
                table: "MaintenanceRecords",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Deals",
                table: "Deals",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientProfiles",
                table: "ClientProfiles",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bodies",
                table: "Bodies",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentDeal_Appointments_AppointmentId",
                table: "AppointmentDeal",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentDeal_Deals_DealId",
                table: "AppointmentDeal",
                column: "DealId",
                principalTable: "Deals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_MaintenanceRecords_MaintenanceRecordId",
                table: "Appointments",
                column: "MaintenanceRecordId",
                principalTable: "MaintenanceRecords",
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
                name: "FK_Appointments_RepairShops_RepairShopId",
                table: "Appointments",
                column: "RepairShopId",
                principalTable: "RepairShops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Vehicles_VehicleId",
                table: "Appointments",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
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
                name: "FK_MaintenanceRecords_RepairShops_RepairShopId",
                table: "MaintenanceRecords",
                column: "RepairShopId",
                principalTable: "RepairShops",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceRecords_Vehicles_VehicleId",
                table: "MaintenanceRecords",
                column: "VehicleId",
                principalTable: "Vehicles",
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
                name: "FK_Notifications_Users_UserId",
                table: "Notifications",
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
                name: "FK_Users_UserRoles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "UserRoles",
                principalColumn: "Id",
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
                name: "FK_VehicleModels_VehicleEngines_VehicleEngineId",
                table: "VehicleModels",
                column: "VehicleEngineId",
                principalTable: "VehicleEngines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VehiclePapers_Vehicles_VehicleId",
                table: "VehiclePapers",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_ClientProfiles_ClientId",
                table: "Vehicles",
                column: "ClientId",
                principalTable: "ClientProfiles",
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
    }
}
