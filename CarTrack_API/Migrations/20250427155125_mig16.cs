using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarTrack_API.Migrations
{
    /// <inheritdoc />
    public partial class mig16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Reminder");

            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "Reminder");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Reminder");

            migrationBuilder.RenameColumn(
                name: "DueMileage",
                table: "Reminder",
                newName: "LastMileageCkeck");

            migrationBuilder.RenameColumn(
                name: "DueDate",
                table: "Reminder",
                newName: "LastDateCheck");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastMileageCkeck",
                table: "Reminder",
                newName: "DueMileage");

            migrationBuilder.RenameColumn(
                name: "LastDateCheck",
                table: "Reminder",
                newName: "DueDate");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Reminder",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "Reminder",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Reminder",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
