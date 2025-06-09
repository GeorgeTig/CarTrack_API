using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarTrack_API.Migrations
{
    /// <inheritdoc />
    public partial class mig31 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaintenanceNames",
                table: "MaintenanceUnverifiedRecord");

            migrationBuilder.AddColumn<List<string>>(
                name: "AttachmentUrls",
                table: "MaintenanceUnverifiedRecord",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "CustomMaintenanceNames",
                table: "MaintenanceUnverifiedRecord",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EntryType",
                table: "MaintenanceUnverifiedRecord",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<List<int>>(
                name: "RelatedConfigIds",
                table: "MaintenanceUnverifiedRecord",
                type: "jsonb",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttachmentUrls",
                table: "MaintenanceUnverifiedRecord");

            migrationBuilder.DropColumn(
                name: "CustomMaintenanceNames",
                table: "MaintenanceUnverifiedRecord");

            migrationBuilder.DropColumn(
                name: "EntryType",
                table: "MaintenanceUnverifiedRecord");

            migrationBuilder.DropColumn(
                name: "RelatedConfigIds",
                table: "MaintenanceUnverifiedRecord");

            migrationBuilder.AddColumn<List<string>>(
                name: "MaintenanceNames",
                table: "MaintenanceUnverifiedRecord",
                type: "text[]",
                nullable: false);
        }
    }
}
