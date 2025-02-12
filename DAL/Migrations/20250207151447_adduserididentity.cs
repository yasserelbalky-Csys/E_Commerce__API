using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class adduserididentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0c91adda-2e2b-491c-a132-cb5dd0c8635b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "335bc83c-8034-4966-9505-aa2dcbeaa688");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "05e65be6-3a43-430b-b24f-bf62b5c64316", null, "User", "USER" },
                    { "95050b1d-be80-4c3a-a8a5-e143b71401e3", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "05e65be6-3a43-430b-b24f-bf62b5c64316");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "95050b1d-be80-4c3a-a8a5-e143b71401e3");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0c91adda-2e2b-491c-a132-cb5dd0c8635b", null, "Admin", "ADMIN" },
                    { "335bc83c-8034-4966-9505-aa2dcbeaa688", null, "User", "USER" }
                });
        }
    }
}
