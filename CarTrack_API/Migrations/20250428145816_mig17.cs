using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CarTrack_API.Migrations
{
    /// <inheritdoc />
    public partial class mig17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Reminder");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Reminder",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reminder_StatusId",
                table: "Reminder",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reminder_Status_StatusId",
                table: "Reminder",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reminder_Status_StatusId",
                table: "Reminder");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropIndex(
                name: "IX_Reminder_StatusId",
                table: "Reminder");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Reminder");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Reminder",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
