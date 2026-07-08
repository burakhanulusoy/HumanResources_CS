using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HumanResources.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig_updated_Shiftt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Vardiyalar_VardiyaId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "YoneticiId",
                table: "Vardiyalar",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vardiyalar_YoneticiId",
                table: "Vardiyalar",
                column: "YoneticiId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Vardiyalar_VardiyaId",
                table: "AspNetUsers",
                column: "VardiyaId",
                principalTable: "Vardiyalar",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Vardiyalar_AspNetUsers_YoneticiId",
                table: "Vardiyalar",
                column: "YoneticiId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Vardiyalar_VardiyaId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Vardiyalar_AspNetUsers_YoneticiId",
                table: "Vardiyalar");

            migrationBuilder.DropIndex(
                name: "IX_Vardiyalar_YoneticiId",
                table: "Vardiyalar");

            migrationBuilder.DropColumn(
                name: "YoneticiId",
                table: "Vardiyalar");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Vardiyalar_VardiyaId",
                table: "AspNetUsers",
                column: "VardiyaId",
                principalTable: "Vardiyalar",
                principalColumn: "Id");
        }
    }
}
