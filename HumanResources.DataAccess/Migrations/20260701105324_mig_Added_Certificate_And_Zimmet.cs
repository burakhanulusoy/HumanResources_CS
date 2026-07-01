using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HumanResources.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig_Added_Certificate_And_Zimmet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SonGecerlilikTarihi",
                table: "AppUserEgitim");

            migrationBuilder.DropColumn(
                name: "YenilemeTarihi",
                table: "AppUserEgitim");

            migrationBuilder.CreateTable(
                name: "SertifikaTuru",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(type: "text", nullable: false),
                    Aciklama = table.Column<string>(type: "text", nullable: true),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SilindiMi = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SertifikaTuru", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ZimmetTuru",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(type: "text", nullable: false),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SilindiMi = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZimmetTuru", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sertifika",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AppUserId = table.Column<int>(type: "integer", nullable: false),
                    SertifikaTuruId = table.Column<int>(type: "integer", nullable: false),
                    VerenKurum = table.Column<string>(type: "text", nullable: false),
                    BelgeNo = table.Column<string>(type: "text", nullable: false),
                    AlinmaTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GecerlilikTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    YenilemeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Durumu = table.Column<int>(type: "integer", nullable: false),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SilindiMi = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sertifika", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sertifika_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sertifika_SertifikaTuru_SertifikaTuruId",
                        column: x => x.SertifikaTuruId,
                        principalTable: "SertifikaTuru",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zimmet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AppUserId = table.Column<int>(type: "integer", nullable: false),
                    ZimmetTuruId = table.Column<int>(type: "integer", nullable: false),
                    SeriNumarasi = table.Column<string>(type: "text", nullable: true),
                    TeslimTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IadeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Durumu = table.Column<int>(type: "integer", nullable: false),
                    Aciklama = table.Column<string>(type: "text", nullable: true),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SilindiMi = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zimmet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zimmet_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Zimmet_ZimmetTuru_ZimmetTuruId",
                        column: x => x.ZimmetTuruId,
                        principalTable: "ZimmetTuru",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sertifika_AppUserId",
                table: "Sertifika",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Sertifika_SertifikaTuruId",
                table: "Sertifika",
                column: "SertifikaTuruId");

            migrationBuilder.CreateIndex(
                name: "IX_Zimmet_AppUserId",
                table: "Zimmet",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Zimmet_ZimmetTuruId",
                table: "Zimmet",
                column: "ZimmetTuruId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sertifika");

            migrationBuilder.DropTable(
                name: "Zimmet");

            migrationBuilder.DropTable(
                name: "SertifikaTuru");

            migrationBuilder.DropTable(
                name: "ZimmetTuru");

            migrationBuilder.AddColumn<DateTime>(
                name: "SonGecerlilikTarihi",
                table: "AppUserEgitim",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "YenilemeTarihi",
                table: "AppUserEgitim",
                type: "timestamp with time zone",
                nullable: true);
        }
    }
}
