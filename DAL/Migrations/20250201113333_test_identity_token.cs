using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class test_identity_token : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "34382af2-360d-4e8a-a812-44e8550d5e3d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8ffb88df-9313-470b-9eff-75774fe0df9b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "764ce13a-9d07-4adf-8eb4-610ed390a370", null, "Admin", "ADMIN" },
                    { "c43b32b4-8a00-4878-ab0b-0503e70f0025", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "764ce13a-9d07-4adf-8eb4-610ed390a370");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c43b32b4-8a00-4878-ab0b-0503e70f0025");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "34382af2-360d-4e8a-a812-44e8550d5e3d", null, "Admin", "ADMIN" },
                    { "8ffb88df-9313-470b-9eff-75774fe0df9b", null, "User", "USER" }
                });
        }
    }
}
