using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CV_ASP.NET.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Anvandare",
                columns: table => new
                {
                    Anvid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Anvandarnamn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fornamn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Efternamn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Profilbild = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrivatProfil = table.Column<bool>(type: "bit", nullable: false),
                    ListadStartsida = table.Column<bool>(type: "bit", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anvandare", x => x.Anvid);
                });

            migrationBuilder.CreateTable(
                name: "Erfarenhet",
                columns: table => new
                {
                    Eid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Beskrivning = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Arbetsgivare = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erfarenhet", x => x.Eid);
                });

            migrationBuilder.CreateTable(
                name: "Kompetenser",
                columns: table => new
                {
                    Kid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KompetensNamn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Beskrivning = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kompetenser", x => x.Kid);
                });

            migrationBuilder.CreateTable(
                name: "Utbildning",
                columns: table => new
                {
                    Uid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Instutition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kurs_program = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Beskrivning = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utbildning", x => x.Uid);
                });

            migrationBuilder.CreateTable(
                name: "Adresser",
                columns: table => new
                {
                    Aid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gatunamn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Postnummer = table.Column<int>(type: "int", nullable: false),
                    Anvid = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresser", x => x.Aid);
                    table.ForeignKey(
                        name: "FK_Adresser_Anvandare_Anvid",
                        column: x => x.Anvid,
                        principalTable: "Anvandare",
                        principalColumn: "Anvid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CVs",
                columns: table => new
                {
                    Cvid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Profilbild = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Beskrivning = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnvandarNamn = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AntalBesokare = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CVs", x => x.Cvid);
                    table.ForeignKey(
                        name: "FK_CVs_Anvandare_AnvandarNamn",
                        column: x => x.AnvandarNamn,
                        principalTable: "Anvandare",
                        principalColumn: "Anvid");
                });

            migrationBuilder.CreateTable(
                name: "Meddelande",
                columns: table => new
                {
                    Mid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Innehall = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Last = table.Column<bool>(type: "bit", nullable: true),
                    FranAnvandareId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TillAnvandareId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AnonymAnvandare = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meddelande", x => x.Mid);
                    table.ForeignKey(
                        name: "FK_Meddelande_Anvandare_FranAnvandareId",
                        column: x => x.FranAnvandareId,
                        principalTable: "Anvandare",
                        principalColumn: "Anvid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Meddelande_Anvandare_TillAnvandareId",
                        column: x => x.TillAnvandareId,
                        principalTable: "Anvandare",
                        principalColumn: "Anvid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CV_Erfarenhet",
                columns: table => new
                {
                    Eid = table.Column<int>(type: "int", nullable: false),
                    Cvid = table.Column<int>(type: "int", nullable: false),
                    Startdatum = table.Column<DateOnly>(type: "date", nullable: false),
                    Slutdatum = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CV_Erfarenhet", x => new { x.Cvid, x.Eid });
                    table.ForeignKey(
                        name: "FK_CV_Erfarenhet_CVs_Cvid",
                        column: x => x.Cvid,
                        principalTable: "CVs",
                        principalColumn: "Cvid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CV_Erfarenhet_Erfarenhet_Eid",
                        column: x => x.Eid,
                        principalTable: "Erfarenhet",
                        principalColumn: "Eid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CV_Kompetenser",
                columns: table => new
                {
                    Kid = table.Column<int>(type: "int", nullable: false),
                    Cvid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CV_Kompetenser", x => new { x.Cvid, x.Kid });
                    table.ForeignKey(
                        name: "FK_CV_Kompetenser_CVs_Cvid",
                        column: x => x.Cvid,
                        principalTable: "CVs",
                        principalColumn: "Cvid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CV_Kompetenser_Kompetenser_Kid",
                        column: x => x.Kid,
                        principalTable: "Kompetenser",
                        principalColumn: "Kid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CV_Utbildning",
                columns: table => new
                {
                    Uid = table.Column<int>(type: "int", nullable: false),
                    CVid = table.Column<int>(type: "int", nullable: false),
                    Startdatum = table.Column<DateOnly>(type: "date", nullable: false),
                    Slutdatum = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CV_Utbildning", x => new { x.CVid, x.Uid });
                    table.ForeignKey(
                        name: "FK_CV_Utbildning_CVs_CVid",
                        column: x => x.CVid,
                        principalTable: "CVs",
                        principalColumn: "Cvid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CV_Utbildning_Utbildning_Uid",
                        column: x => x.Uid,
                        principalTable: "Utbildning",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adresser_Anvid",
                table: "Adresser",
                column: "Anvid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CV_Erfarenhet_Eid",
                table: "CV_Erfarenhet",
                column: "Eid");

            migrationBuilder.CreateIndex(
                name: "IX_CV_Kompetenser_Kid",
                table: "CV_Kompetenser",
                column: "Kid");

            migrationBuilder.CreateIndex(
                name: "IX_CV_Utbildning_Uid",
                table: "CV_Utbildning",
                column: "Uid");

            migrationBuilder.CreateIndex(
                name: "IX_CVs_AnvandarNamn",
                table: "CVs",
                column: "AnvandarNamn",
                unique: true,
                filter: "[AnvandarNamn] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Meddelande_FranAnvandareId",
                table: "Meddelande",
                column: "FranAnvandareId");

            migrationBuilder.CreateIndex(
                name: "IX_Meddelande_TillAnvandareId",
                table: "Meddelande",
                column: "TillAnvandareId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adresser");

            migrationBuilder.DropTable(
                name: "CV_Erfarenhet");

            migrationBuilder.DropTable(
                name: "CV_Kompetenser");

            migrationBuilder.DropTable(
                name: "CV_Utbildning");

            migrationBuilder.DropTable(
                name: "Meddelande");

            migrationBuilder.DropTable(
                name: "Erfarenhet");

            migrationBuilder.DropTable(
                name: "Kompetenser");

            migrationBuilder.DropTable(
                name: "CVs");

            migrationBuilder.DropTable(
                name: "Utbildning");

            migrationBuilder.DropTable(
                name: "Anvandare");
        }
    }
}
