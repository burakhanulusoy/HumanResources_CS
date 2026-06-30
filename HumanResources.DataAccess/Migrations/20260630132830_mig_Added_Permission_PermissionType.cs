using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HumanResources.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig_Added_Permission_PermissionType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IzinTuru",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(type: "text", nullable: false),
                    UcretliMi = table.Column<bool>(type: "boolean", nullable: false),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SilindiMi = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IzinTuru", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Izin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PersonelId = table.Column<int>(type: "integer", nullable: false),
                    IzinTuruId = table.Column<int>(type: "integer", nullable: false),
                    BaslangicTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BitisTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Aciklama = table.Column<string>(type: "text", nullable: false),
                    AmirOnayi = table.Column<bool>(type: "boolean", nullable: true),
                    IkOnayi = table.Column<bool>(type: "boolean", nullable: true),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SilindiMi = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Izin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Izin_AspNetUsers_PersonelId",
                        column: x => x.PersonelId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Izin_IzinTuru_IzinTuruId",
                        column: x => x.IzinTuruId,
                        principalTable: "IzinTuru",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Izin_IzinTuruId",
                table: "Izin",
                column: "IzinTuruId");

            migrationBuilder.CreateIndex(
                name: "IX_Izin_PersonelId",
                table: "Izin",
                column: "PersonelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Izin");

            migrationBuilder.DropTable(
                name: "IzinTuru");
        }
    }
}
