using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HumanResources.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig_Added_Vardiya : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VardiyaId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Vardiya",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Aciklama = table.Column<string>(type: "text", nullable: false),
                    BaslangicSaati = table.Column<TimeSpan>(type: "interval", nullable: false),
                    BitisSaati = table.Column<TimeSpan>(type: "interval", nullable: false),
                    AraDinlenmeSuresiDk = table.Column<int>(type: "integer", nullable: false),
                    CalismaSuresi = table.Column<TimeSpan>(type: "interval", nullable: false),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SilindiMi = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vardiya", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_VardiyaId",
                table: "AspNetUsers",
                column: "VardiyaId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Vardiya_VardiyaId",
                table: "AspNetUsers",
                column: "VardiyaId",
                principalTable: "Vardiya",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Vardiya_VardiyaId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Vardiya");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_VardiyaId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "VardiyaId",
                table: "AspNetUsers");
        }
    }
}
