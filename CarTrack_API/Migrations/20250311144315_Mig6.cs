using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarTrack_API.Migrations
{
    /// <inheritdoc />
    public partial class Mig6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleModel_Bodie_BodyId",
                table: "VehicleModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bodie",
                table: "Bodie");

            migrationBuilder.RenameTable(
                name: "Bodie",
                newName: "Body");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Body",
                table: "Body",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleModel_Body_BodyId",
                table: "VehicleModel",
                column: "BodyId",
                principalTable: "Body",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleModel_Body_BodyId",
                table: "VehicleModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Body",
                table: "Body");

            migrationBuilder.RenameTable(
                name: "Body",
                newName: "Bodie");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bodie",
                table: "Bodie",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleModel_Bodie_BodyId",
                table: "VehicleModel",
                column: "BodyId",
                principalTable: "Bodie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
