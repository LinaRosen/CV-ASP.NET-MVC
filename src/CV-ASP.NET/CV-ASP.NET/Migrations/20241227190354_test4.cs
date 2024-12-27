using Microsoft.EntityFrameworkCore.Migrations;



#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CV_ASP.NET.Migrations
{
    /// <inheritdoc />
    public partial class test4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Anvandare",
                columns: new[] { "Id", "AccessFailedCount", "Anvandarnamn", "ConcurrencyStamp", "Efternamn", "Email", "EmailConfirmed", "Fornamn", "ListadStartsida", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PrivatProfil", "Profilbild", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1", 0, "LinaRos", "b29d929a-4114-403b-8783-d4c7b5615a8b", "Rosén", "lina@example.com", false, "Lina", false, false, null, "LINA@EXAMPLE.COM", "LINAROS", null, null, false, false, null, "5968e922-29f1-4b51-af68-de02baad00b5", false, "LinaRos" },
                    { "2", 0, "NoraBolin", "50bd6cf8-ac91-4d2d-8714-acd293fec102", "Bolin", "nora@example.com", false, "Nora", false, false, null, "NORA@EXAMPLE.COM", "NORABOLIN", null, null, false, false, null, "de9c93ea-d3e4-4326-af8e-a8a96125e02c", false, "NoraBolin" },
                    { "3", 0, "AmandaPerbro", "a444b5e0-5cbb-4cad-804e-172753d3c2f4", "Perbro", "amanda@example.com", false, "Amanda", false, false, null, "AMANDA@EXAMPLE.COM", "AMANDAPERBRO", null, null, false, false, null, "ee32c93e-3f3d-411e-9547-3b7aac6c5f57", false, "AmandaPerbro" }
                });
        }
    }
}
