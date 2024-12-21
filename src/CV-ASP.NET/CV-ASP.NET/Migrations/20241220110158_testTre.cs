using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CV_ASP.NET.Migrations
{
    /// <inheritdoc />
    public partial class testTre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projekt",
                columns: table => new
                {
                    Pid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Namn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Beskrivning = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SkapadAv = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DatumSkapad = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projekt", x => x.Pid);
                    table.ForeignKey(
                        name: "FK_Projekt_Anvandare_SkapadAv",
                        column: x => x.SkapadAv,
                        principalTable: "Anvandare",
                        principalColumn: "Anvid");
                });

            migrationBuilder.CreateTable(
                name: "AnvProjekt",
                columns: table => new
                {
                    Anvid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Pid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnvProjekt", x => new { x.Anvid, x.Pid });
                    table.ForeignKey(
                        name: "FK_AnvProjekt_Anvandare_Anvid",
                        column: x => x.Anvid,
                        principalTable: "Anvandare",
                        principalColumn: "Anvid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnvProjekt_Projekt_Pid",
                        column: x => x.Pid,
                        principalTable: "Projekt",
                        principalColumn: "Pid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnvProjekt_Pid",
                table: "AnvProjekt",
                column: "Pid");

            migrationBuilder.CreateIndex(
                name: "IX_Projekt_SkapadAv",
                table: "Projekt",
                column: "SkapadAv");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnvProjekt");

            migrationBuilder.DropTable(
                name: "Projekt");
        }
    }
}
