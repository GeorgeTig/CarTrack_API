using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarTrack_API.Migrations
{
    /// <inheritdoc />
    public partial class mig22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DueDate",
                table: "Reminder",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "DueMileage",
                table: "Reminder",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Notification",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RemiderId",
                table: "Notification",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VehicleId",
                table: "Notification",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Notification_RemiderId",
                table: "Notification",
                column: "RemiderId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_VehicleId",
                table: "Notification",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_Reminder_RemiderId",
                table: "Notification",
                column: "RemiderId",
                principalTable: "Reminder",
                principalColumn: "VehicleMaintenanceConfigId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_Vehicle_VehicleId",
                table: "Notification",
                column: "VehicleId",
                principalTable: "Vehicle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_Reminder_RemiderId",
                table: "Notification");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_Vehicle_VehicleId",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_RemiderId",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_VehicleId",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Reminder");

            migrationBuilder.DropColumn(
                name: "DueMileage",
                table: "Reminder");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "RemiderId",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "Notification");
        }
    }
}
