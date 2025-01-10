using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CV_ASP.NET.Migrations
{
    /// <inheritdoc />
    public partial class anvandareadress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Adresser_Anvid",
                table: "Adresser");

            migrationBuilder.AddColumn<string>(
                name: "Gatunamn",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Postnummer",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Stad",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Postnummer",
                table: "Adresser",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Adresser_Anvid",
                table: "Adresser",
                column: "Anvid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Adresser_Anvid",
                table: "Adresser");

            migrationBuilder.DropColumn(
                name: "Gatunamn",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Postnummer",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Stad",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "Postnummer",
                table: "Adresser",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Adresser_Anvid",
                table: "Adresser",
                column: "Anvid",
                unique: true);
        }
    }
}
