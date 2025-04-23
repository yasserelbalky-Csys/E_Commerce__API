using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class add_roles_in_dbcontext_b_canceled : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "b_order_cancel",
                table: "CurrentProductBalance",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "64ce2ffc-1b98-40aa-bfd2-a9fa7ab4e26e", "admin-role", "Admin", "ADMIN" },
                    { "7a6c7350-b5db-4e15-a6de-c9e68e4d9d7f", "user-role", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "64ce2ffc-1b98-40aa-bfd2-a9fa7ab4e26e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7a6c7350-b5db-4e15-a6de-c9e68e4d9d7f");

            migrationBuilder.DropColumn(
                name: "b_order_cancel",
                table: "CurrentProductBalance");
        }
    }
}
