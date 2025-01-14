using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CV_ASP.NET.Migrations
{
    /// <inheritdoc />
    public partial class meddelanden : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avsandare",
                table: "Meddelande");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Avsandare",
                table: "Meddelande",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
