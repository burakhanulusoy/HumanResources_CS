using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HumanResources.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Demirbas_Ekleme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Zimmetler_ZimmetTurleri_ZimmetTuruId",
                table: "Zimmetler");

            migrationBuilder.DropColumn(
                name: "Marka",
                table: "Zimmetler");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "Zimmetler");

            migrationBuilder.DropColumn(
                name: "SeriNumarasi",
                table: "Zimmetler");

            migrationBuilder.RenameColumn(
                name: "ZimmetTuruId",
                table: "Zimmetler",
                newName: "DemirbasId");

            migrationBuilder.RenameIndex(
                name: "IX_Zimmetler_ZimmetTuruId",
                table: "Zimmetler",
                newName: "IX_Zimmetler_DemirbasId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "IadeTarihi",
                table: "Zimmetler",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "Aciklama",
                table: "Zimmetler",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "SuresizMi",
                table: "Zimmetler",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Demirbaslar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ZimmetTuruId = table.Column<int>(type: "integer", nullable: false),
                    Marka = table.Column<string>(type: "text", nullable: false),
                    Model = table.Column<string>(type: "text", nullable: false),
                    SeriNumarasi = table.Column<string>(type: "text", nullable: true),
                    Aciklama = table.Column<string>(type: "text", nullable: true),
                    Durumu = table.Column<int>(type: "integer", nullable: false),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SilindiMi = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Demirbaslar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Demirbaslar_ZimmetTurleri_ZimmetTuruId",
                        column: x => x.ZimmetTuruId,
                        principalTable: "ZimmetTurleri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Demirbaslar_ZimmetTuruId",
                table: "Demirbaslar",
                column: "ZimmetTuruId");

            migrationBuilder.AddForeignKey(
                name: "FK_Zimmetler_Demirbaslar_DemirbasId",
                table: "Zimmetler",
                column: "DemirbasId",
                principalTable: "Demirbaslar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Zimmetler_Demirbaslar_DemirbasId",
                table: "Zimmetler");

            migrationBuilder.DropTable(
                name: "Demirbaslar");

            migrationBuilder.DropColumn(
                name: "SuresizMi",
                table: "Zimmetler");

            migrationBuilder.RenameColumn(
                name: "DemirbasId",
                table: "Zimmetler",
                newName: "ZimmetTuruId");

            migrationBuilder.RenameIndex(
                name: "IX_Zimmetler_DemirbasId",
                table: "Zimmetler",
                newName: "IX_Zimmetler_ZimmetTuruId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "IadeTarihi",
                table: "Zimmetler",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Aciklama",
                table: "Zimmetler",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Marka",
                table: "Zimmetler",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Zimmetler",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeriNumarasi",
                table: "Zimmetler",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Zimmetler_ZimmetTurleri_ZimmetTuruId",
                table: "Zimmetler",
                column: "ZimmetTuruId",
                principalTable: "ZimmetTurleri",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
