using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HumanResources.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig_Update_Certificate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserEgitim_AspNetUsers_AppUserId",
                table: "AppUserEgitim");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserEgitim_Egitim_EgitimId",
                table: "AppUserEgitim");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Vardiya_VardiyaId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Izin_AspNetUsers_PersonelId",
                table: "Izin");

            migrationBuilder.DropForeignKey(
                name: "FK_Izin_IzinTuru_IzinTuruId",
                table: "Izin");

            migrationBuilder.DropForeignKey(
                name: "FK_Sertifika_AspNetUsers_AppUserId",
                table: "Sertifika");

            migrationBuilder.DropForeignKey(
                name: "FK_Sertifika_SertifikaTuru_SertifikaTuruId",
                table: "Sertifika");

            migrationBuilder.DropForeignKey(
                name: "FK_Zimmet_AspNetUsers_AppUserId",
                table: "Zimmet");

            migrationBuilder.DropForeignKey(
                name: "FK_Zimmet_ZimmetTuru_ZimmetTuruId",
                table: "Zimmet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ZimmetTuru",
                table: "ZimmetTuru");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Zimmet",
                table: "Zimmet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vardiya",
                table: "Vardiya");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SertifikaTuru",
                table: "SertifikaTuru");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sertifika",
                table: "Sertifika");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IzinTuru",
                table: "IzinTuru");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Izin",
                table: "Izin");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Egitim",
                table: "Egitim");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUserEgitim",
                table: "AppUserEgitim");

            migrationBuilder.RenameTable(
                name: "ZimmetTuru",
                newName: "ZimmetTurleri");

            migrationBuilder.RenameTable(
                name: "Zimmet",
                newName: "Zimmetler");

            migrationBuilder.RenameTable(
                name: "Vardiya",
                newName: "Vardiyalar");

            migrationBuilder.RenameTable(
                name: "SertifikaTuru",
                newName: "SertifikaTurleri");

            migrationBuilder.RenameTable(
                name: "Sertifika",
                newName: "Sertifikalar");

            migrationBuilder.RenameTable(
                name: "IzinTuru",
                newName: "IzinTurleri");

            migrationBuilder.RenameTable(
                name: "Izin",
                newName: "Izinler");

            migrationBuilder.RenameTable(
                name: "Egitim",
                newName: "Egitimler");

            migrationBuilder.RenameTable(
                name: "AppUserEgitim",
                newName: "AppUserEgitimler");

            migrationBuilder.RenameIndex(
                name: "IX_Zimmet_ZimmetTuruId",
                table: "Zimmetler",
                newName: "IX_Zimmetler_ZimmetTuruId");

            migrationBuilder.RenameIndex(
                name: "IX_Zimmet_AppUserId",
                table: "Zimmetler",
                newName: "IX_Zimmetler_AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Sertifika_SertifikaTuruId",
                table: "Sertifikalar",
                newName: "IX_Sertifikalar_SertifikaTuruId");

            migrationBuilder.RenameIndex(
                name: "IX_Sertifika_AppUserId",
                table: "Sertifikalar",
                newName: "IX_Sertifikalar_AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Izin_PersonelId",
                table: "Izinler",
                newName: "IX_Izinler_PersonelId");

            migrationBuilder.RenameIndex(
                name: "IX_Izin_IzinTuruId",
                table: "Izinler",
                newName: "IX_Izinler_IzinTuruId");

            migrationBuilder.RenameIndex(
                name: "IX_AppUserEgitim_EgitimId",
                table: "AppUserEgitimler",
                newName: "IX_AppUserEgitimler_EgitimId");

            migrationBuilder.RenameIndex(
                name: "IX_AppUserEgitim_AppUserId",
                table: "AppUserEgitimler",
                newName: "IX_AppUserEgitimler_AppUserId");

            migrationBuilder.AddColumn<string>(
                name: "Aciklama",
                table: "Sertifikalar",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DosyaYolu",
                table: "Sertifikalar",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ZimmetTurleri",
                table: "ZimmetTurleri",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Zimmetler",
                table: "Zimmetler",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vardiyalar",
                table: "Vardiyalar",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SertifikaTurleri",
                table: "SertifikaTurleri",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sertifikalar",
                table: "Sertifikalar",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IzinTurleri",
                table: "IzinTurleri",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Izinler",
                table: "Izinler",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Egitimler",
                table: "Egitimler",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUserEgitimler",
                table: "AppUserEgitimler",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserEgitimler_AspNetUsers_AppUserId",
                table: "AppUserEgitimler",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserEgitimler_Egitimler_EgitimId",
                table: "AppUserEgitimler",
                column: "EgitimId",
                principalTable: "Egitimler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Vardiyalar_VardiyaId",
                table: "AspNetUsers",
                column: "VardiyaId",
                principalTable: "Vardiyalar",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Izinler_AspNetUsers_PersonelId",
                table: "Izinler",
                column: "PersonelId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Izinler_IzinTurleri_IzinTuruId",
                table: "Izinler",
                column: "IzinTuruId",
                principalTable: "IzinTurleri",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sertifikalar_AspNetUsers_AppUserId",
                table: "Sertifikalar",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sertifikalar_SertifikaTurleri_SertifikaTuruId",
                table: "Sertifikalar",
                column: "SertifikaTuruId",
                principalTable: "SertifikaTurleri",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Zimmetler_AspNetUsers_AppUserId",
                table: "Zimmetler",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Zimmetler_ZimmetTurleri_ZimmetTuruId",
                table: "Zimmetler",
                column: "ZimmetTuruId",
                principalTable: "ZimmetTurleri",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserEgitimler_AspNetUsers_AppUserId",
                table: "AppUserEgitimler");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserEgitimler_Egitimler_EgitimId",
                table: "AppUserEgitimler");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Vardiyalar_VardiyaId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Izinler_AspNetUsers_PersonelId",
                table: "Izinler");

            migrationBuilder.DropForeignKey(
                name: "FK_Izinler_IzinTurleri_IzinTuruId",
                table: "Izinler");

            migrationBuilder.DropForeignKey(
                name: "FK_Sertifikalar_AspNetUsers_AppUserId",
                table: "Sertifikalar");

            migrationBuilder.DropForeignKey(
                name: "FK_Sertifikalar_SertifikaTurleri_SertifikaTuruId",
                table: "Sertifikalar");

            migrationBuilder.DropForeignKey(
                name: "FK_Zimmetler_AspNetUsers_AppUserId",
                table: "Zimmetler");

            migrationBuilder.DropForeignKey(
                name: "FK_Zimmetler_ZimmetTurleri_ZimmetTuruId",
                table: "Zimmetler");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ZimmetTurleri",
                table: "ZimmetTurleri");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Zimmetler",
                table: "Zimmetler");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vardiyalar",
                table: "Vardiyalar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SertifikaTurleri",
                table: "SertifikaTurleri");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sertifikalar",
                table: "Sertifikalar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IzinTurleri",
                table: "IzinTurleri");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Izinler",
                table: "Izinler");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Egitimler",
                table: "Egitimler");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUserEgitimler",
                table: "AppUserEgitimler");

            migrationBuilder.DropColumn(
                name: "Aciklama",
                table: "Sertifikalar");

            migrationBuilder.DropColumn(
                name: "DosyaYolu",
                table: "Sertifikalar");

            migrationBuilder.RenameTable(
                name: "ZimmetTurleri",
                newName: "ZimmetTuru");

            migrationBuilder.RenameTable(
                name: "Zimmetler",
                newName: "Zimmet");

            migrationBuilder.RenameTable(
                name: "Vardiyalar",
                newName: "Vardiya");

            migrationBuilder.RenameTable(
                name: "SertifikaTurleri",
                newName: "SertifikaTuru");

            migrationBuilder.RenameTable(
                name: "Sertifikalar",
                newName: "Sertifika");

            migrationBuilder.RenameTable(
                name: "IzinTurleri",
                newName: "IzinTuru");

            migrationBuilder.RenameTable(
                name: "Izinler",
                newName: "Izin");

            migrationBuilder.RenameTable(
                name: "Egitimler",
                newName: "Egitim");

            migrationBuilder.RenameTable(
                name: "AppUserEgitimler",
                newName: "AppUserEgitim");

            migrationBuilder.RenameIndex(
                name: "IX_Zimmetler_ZimmetTuruId",
                table: "Zimmet",
                newName: "IX_Zimmet_ZimmetTuruId");

            migrationBuilder.RenameIndex(
                name: "IX_Zimmetler_AppUserId",
                table: "Zimmet",
                newName: "IX_Zimmet_AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Sertifikalar_SertifikaTuruId",
                table: "Sertifika",
                newName: "IX_Sertifika_SertifikaTuruId");

            migrationBuilder.RenameIndex(
                name: "IX_Sertifikalar_AppUserId",
                table: "Sertifika",
                newName: "IX_Sertifika_AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Izinler_PersonelId",
                table: "Izin",
                newName: "IX_Izin_PersonelId");

            migrationBuilder.RenameIndex(
                name: "IX_Izinler_IzinTuruId",
                table: "Izin",
                newName: "IX_Izin_IzinTuruId");

            migrationBuilder.RenameIndex(
                name: "IX_AppUserEgitimler_EgitimId",
                table: "AppUserEgitim",
                newName: "IX_AppUserEgitim_EgitimId");

            migrationBuilder.RenameIndex(
                name: "IX_AppUserEgitimler_AppUserId",
                table: "AppUserEgitim",
                newName: "IX_AppUserEgitim_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ZimmetTuru",
                table: "ZimmetTuru",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Zimmet",
                table: "Zimmet",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vardiya",
                table: "Vardiya",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SertifikaTuru",
                table: "SertifikaTuru",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sertifika",
                table: "Sertifika",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IzinTuru",
                table: "IzinTuru",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Izin",
                table: "Izin",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Egitim",
                table: "Egitim",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUserEgitim",
                table: "AppUserEgitim",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserEgitim_AspNetUsers_AppUserId",
                table: "AppUserEgitim",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserEgitim_Egitim_EgitimId",
                table: "AppUserEgitim",
                column: "EgitimId",
                principalTable: "Egitim",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Vardiya_VardiyaId",
                table: "AspNetUsers",
                column: "VardiyaId",
                principalTable: "Vardiya",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Izin_AspNetUsers_PersonelId",
                table: "Izin",
                column: "PersonelId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Izin_IzinTuru_IzinTuruId",
                table: "Izin",
                column: "IzinTuruId",
                principalTable: "IzinTuru",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sertifika_AspNetUsers_AppUserId",
                table: "Sertifika",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sertifika_SertifikaTuru_SertifikaTuruId",
                table: "Sertifika",
                column: "SertifikaTuruId",
                principalTable: "SertifikaTuru",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Zimmet_AspNetUsers_AppUserId",
                table: "Zimmet",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Zimmet_ZimmetTuru_ZimmetTuruId",
                table: "Zimmet",
                column: "ZimmetTuruId",
                principalTable: "ZimmetTuru",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
