using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HumanResources.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig_update_Dicipline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IspatGorseliYolu",
                table: "DisiplinKayitlari",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TanikAdSoyad",
                table: "DisiplinKayitlari",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IspatGorseliYolu",
                table: "DisiplinKayitlari");

            migrationBuilder.DropColumn(
                name: "TanikAdSoyad",
                table: "DisiplinKayitlari");
        }
    }
}
