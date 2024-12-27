using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CV_ASP.NET.Migrations
{
    /// <inheritdoc />
    public partial class ny : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adresser_Anvandare_Anvid",
                table: "Adresser");

            migrationBuilder.DropForeignKey(
                name: "FK_AnvProjekt_Anvandare_Anvid",
                table: "AnvProjekt");

            migrationBuilder.DropForeignKey(
                name: "FK_CV_Erfarenhet_CVs_Cvid",
                table: "CV_Erfarenhet");

            migrationBuilder.DropForeignKey(
                name: "FK_CV_Kompetenser_CVs_Cvid",
                table: "CV_Kompetenser");

            migrationBuilder.DropForeignKey(
                name: "FK_CV_Utbildning_CVs_CVid",
                table: "CV_Utbildning");

            migrationBuilder.DropForeignKey(
                name: "FK_CVs_Anvandare_AnvandarNamn",
                table: "CVs");

            migrationBuilder.DropForeignKey(
                name: "FK_Meddelande_Anvandare_FranAnvandareId",
                table: "Meddelande");

            migrationBuilder.DropForeignKey(
                name: "FK_Meddelande_Anvandare_TillAnvandareId",
                table: "Meddelande");

            migrationBuilder.DropForeignKey(
                name: "FK_Projekt_Anvandare_SkapadAv",
                table: "Projekt");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Anvandare",
                table: "Anvandare");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CVs",
                table: "CVs");

            migrationBuilder.DropColumn(
                name: "Anvid",
                table: "Anvandare");

            migrationBuilder.RenameTable(
                name: "CVs",
                newName: "CV");

            migrationBuilder.RenameIndex(
                name: "IX_CVs_AnvandarNamn",
                table: "CV",
                newName: "IX_CV_AnvandarNamn");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Anvandare",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Anvandare",
                table: "Anvandare",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CV",
                table: "CV",
                column: "Cvid");

            migrationBuilder.InsertData(
                table: "Anvandare",
                columns: new[] { "Id", "AccessFailedCount", "Anvandarnamn", "ConcurrencyStamp", "Efternamn", "Email", "EmailConfirmed", "Fornamn", "ListadStartsida", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PrivatProfil", "Profilbild", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1", 0, "LinaRos", "b29d929a-4114-403b-8783-d4c7b5615a8b", "Rosén", "lina@example.com", false, "Lina", false, false, null, "LINA@EXAMPLE.COM", "LINAROS", null, null, false, false, null, "5968e922-29f1-4b51-af68-de02baad00b5", false, "LinaRos" },
                    { "2", 0, "NoraBolin", "50bd6cf8-ac91-4d2d-8714-acd293fec102", "Bolin", "nora@example.com", false, "Nora", false, false, null, "NORA@EXAMPLE.COM", "NORABOLIN", null, null, false, false, null, "de9c93ea-d3e4-4326-af8e-a8a96125e02c", false, "NoraBolin" },
                    { "3", 0, "AmandaPerbro", "a444b5e0-5cbb-4cad-804e-172753d3c2f4", "Perbro", "amanda@example.com", false, "Amanda", false, false, null, "AMANDA@EXAMPLE.COM", "AMANDAPERBRO", null, null, false, false, null, "ee32c93e-3f3d-411e-9547-3b7aac6c5f57", false, "AmandaPerbro" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Adresser_Anvandare_Anvid",
                table: "Adresser",
                column: "Anvid",
                principalTable: "Anvandare",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnvProjekt_Anvandare_Anvid",
                table: "AnvProjekt",
                column: "Anvid",
                principalTable: "Anvandare",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CV_Anvandare_AnvandarNamn",
                table: "CV",
                column: "AnvandarNamn",
                principalTable: "Anvandare",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CV_Erfarenhet_CV_Cvid",
                table: "CV_Erfarenhet",
                column: "Cvid",
                principalTable: "CV",
                principalColumn: "Cvid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CV_Kompetenser_CV_Cvid",
                table: "CV_Kompetenser",
                column: "Cvid",
                principalTable: "CV",
                principalColumn: "Cvid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CV_Utbildning_CV_CVid",
                table: "CV_Utbildning",
                column: "CVid",
                principalTable: "CV",
                principalColumn: "Cvid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Meddelande_Anvandare_FranAnvandareId",
                table: "Meddelande",
                column: "FranAnvandareId",
                principalTable: "Anvandare",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Meddelande_Anvandare_TillAnvandareId",
                table: "Meddelande",
                column: "TillAnvandareId",
                principalTable: "Anvandare",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projekt_Anvandare_SkapadAv",
                table: "Projekt",
                column: "SkapadAv",
                principalTable: "Anvandare",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adresser_Anvandare_Anvid",
                table: "Adresser");

            migrationBuilder.DropForeignKey(
                name: "FK_AnvProjekt_Anvandare_Anvid",
                table: "AnvProjekt");

            migrationBuilder.DropForeignKey(
                name: "FK_CV_Anvandare_AnvandarNamn",
                table: "CV");

            migrationBuilder.DropForeignKey(
                name: "FK_CV_Erfarenhet_CV_Cvid",
                table: "CV_Erfarenhet");

            migrationBuilder.DropForeignKey(
                name: "FK_CV_Kompetenser_CV_Cvid",
                table: "CV_Kompetenser");

            migrationBuilder.DropForeignKey(
                name: "FK_CV_Utbildning_CV_CVid",
                table: "CV_Utbildning");

            migrationBuilder.DropForeignKey(
                name: "FK_Meddelande_Anvandare_FranAnvandareId",
                table: "Meddelande");

            migrationBuilder.DropForeignKey(
                name: "FK_Meddelande_Anvandare_TillAnvandareId",
                table: "Meddelande");

            migrationBuilder.DropForeignKey(
                name: "FK_Projekt_Anvandare_SkapadAv",
                table: "Projekt");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Anvandare",
                table: "Anvandare");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CV",
                table: "CV");

            migrationBuilder.DeleteData(
                table: "Anvandare",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "Anvandare",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "Anvandare",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.RenameTable(
                name: "CV",
                newName: "CVs");

            migrationBuilder.RenameIndex(
                name: "IX_CV_AnvandarNamn",
                table: "CVs",
                newName: "IX_CVs_AnvandarNamn");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Anvandare",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Anvid",
                table: "Anvandare",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Anvandare",
                table: "Anvandare",
                column: "Anvid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CVs",
                table: "CVs",
                column: "Cvid");

            migrationBuilder.AddForeignKey(
                name: "FK_Adresser_Anvandare_Anvid",
                table: "Adresser",
                column: "Anvid",
                principalTable: "Anvandare",
                principalColumn: "Anvid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnvProjekt_Anvandare_Anvid",
                table: "AnvProjekt",
                column: "Anvid",
                principalTable: "Anvandare",
                principalColumn: "Anvid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CV_Erfarenhet_CVs_Cvid",
                table: "CV_Erfarenhet",
                column: "Cvid",
                principalTable: "CVs",
                principalColumn: "Cvid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CV_Kompetenser_CVs_Cvid",
                table: "CV_Kompetenser",
                column: "Cvid",
                principalTable: "CVs",
                principalColumn: "Cvid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CV_Utbildning_CVs_CVid",
                table: "CV_Utbildning",
                column: "CVid",
                principalTable: "CVs",
                principalColumn: "Cvid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CVs_Anvandare_AnvandarNamn",
                table: "CVs",
                column: "AnvandarNamn",
                principalTable: "Anvandare",
                principalColumn: "Anvid");

            migrationBuilder.AddForeignKey(
                name: "FK_Meddelande_Anvandare_FranAnvandareId",
                table: "Meddelande",
                column: "FranAnvandareId",
                principalTable: "Anvandare",
                principalColumn: "Anvid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Meddelande_Anvandare_TillAnvandareId",
                table: "Meddelande",
                column: "TillAnvandareId",
                principalTable: "Anvandare",
                principalColumn: "Anvid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projekt_Anvandare_SkapadAv",
                table: "Projekt",
                column: "SkapadAv",
                principalTable: "Anvandare",
                principalColumn: "Anvid");
        }
    }
}
