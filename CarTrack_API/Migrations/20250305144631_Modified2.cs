using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarTrack_API.Migrations
{
    /// <inheritdoc />
    public partial class Modified2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deal_Appointments_AppointmentId",
                table: "Deal");

            migrationBuilder.DropIndex(
                name: "IX_Deal_AppointmentId",
                table: "Deal");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "Deal");

            migrationBuilder.CreateTable(
                name: "AppointmentDeal",
                columns: table => new
                {
                    AppointmentId = table.Column<int>(type: "integer", nullable: false),
                    DealId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentDeal", x => new { x.AppointmentId, x.DealId });
                    table.ForeignKey(
                        name: "FK_AppointmentDeal_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppointmentDeal_Deal_DealId",
                        column: x => x.DealId,
                        principalTable: "Deal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentDeal_DealId",
                table: "AppointmentDeal",
                column: "DealId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentDeal");

            migrationBuilder.AddColumn<int>(
                name: "AppointmentId",
                table: "Deal",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deal_AppointmentId",
                table: "Deal",
                column: "AppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deal_Appointments_AppointmentId",
                table: "Deal",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id");
        }
    }
}
