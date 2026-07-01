using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HumanResources.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig_Added_Education_UserEducation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Egitim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(type: "text", nullable: false),
                    Egitmen = table.Column<string>(type: "text", nullable: false),
                    EgitimAciklamasi = table.Column<string>(type: "text", nullable: false),
                    EgitimTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SuresiSaat = table.Column<int>(type: "integer", nullable: false),
                    Durumu = table.Column<int>(type: "integer", nullable: false),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SilindiMi = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Egitim", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserEgitim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AppUserId = table.Column<int>(type: "integer", nullable: false),
                    EgitimId = table.Column<int>(type: "integer", nullable: false),
                    BasvuruTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BasvuruDurumu = table.Column<int>(type: "integer", nullable: false),
                    AdminAciklamasi = table.Column<string>(type: "text", nullable: true),
                    SonGecerlilikTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    YenilemeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SilindiMi = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserEgitim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUserEgitim_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserEgitim_Egitim_EgitimId",
                        column: x => x.EgitimId,
                        principalTable: "Egitim",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserEgitim_AppUserId",
                table: "AppUserEgitim",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserEgitim_EgitimId",
                table: "AppUserEgitim",
                column: "EgitimId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserEgitim");

            migrationBuilder.DropTable(
                name: "Egitim");
        }
    }
}
