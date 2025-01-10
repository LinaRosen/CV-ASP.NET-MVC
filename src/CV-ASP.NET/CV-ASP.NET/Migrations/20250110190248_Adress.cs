using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CV_ASP.NET.Migrations
{
    /// <inheritdoc />
    public partial class Adress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adresser_AspNetUsers_Anvid",
                table: "Adresser");

            migrationBuilder.DropIndex(
                name: "IX_Adresser_Anvid",
                table: "Adresser");

            migrationBuilder.DropColumn(
                name: "Anvid",
                table: "Adresser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Anvid",
                table: "Adresser",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Adresser_Anvid",
                table: "Adresser",
                column: "Anvid");

            migrationBuilder.AddForeignKey(
                name: "FK_Adresser_AspNetUsers_Anvid",
                table: "Adresser",
                column: "Anvid",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
