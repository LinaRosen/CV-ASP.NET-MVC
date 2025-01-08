using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CV_ASP.NET.Migrations
{
    /// <inheritdoc />
    public partial class updKomp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KompetensNamn",
                table: "Kompetenser");

            migrationBuilder.AddColumn<string>(
                name: "KompetensNamn",
                table: "CV_Kompetenser",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KompetensNamn",
                table: "CV_Kompetenser");

            migrationBuilder.AddColumn<string>(
                name: "KompetensNamn",
                table: "Kompetenser",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
