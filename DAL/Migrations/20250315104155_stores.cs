using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
	/// <inheritdoc />
	public partial class stores : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.DeleteData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "7f5dce48-3bbd-4645-ab8c-ac35013f89a5");

			migrationBuilder.DeleteData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "cb938f3a-49c7-4bf7-9f93-0eea6120cd8d");

			migrationBuilder.CreateTable(
				name: "stores",
				columns: table => new {
					StoreId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					StoreName = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_stores", x => x.StoreId);
				});

			migrationBuilder.InsertData(
				table: "AspNetRoles",
				columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
				values: new object[,]
				{
					{ "61f5e81d-513a-4887-8735-f965fd4738c0", null, "Admin", "ADMIN" },
					{ "df878e11-093e-4980-852c-9f0ef3098013", null, "User", "USER" }
				});
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropTable(
				name: "stores");

			migrationBuilder.DeleteData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "61f5e81d-513a-4887-8735-f965fd4738c0");

			migrationBuilder.DeleteData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "df878e11-093e-4980-852c-9f0ef3098013");

			migrationBuilder.InsertData(
				table: "AspNetRoles",
				columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
				values: new object[,]
				{
					{ "7f5dce48-3bbd-4645-ab8c-ac35013f89a5", null, "User", "USER" },
					{ "cb938f3a-49c7-4bf7-9f93-0eea6120cd8d", null, "Admin", "ADMIN" }
				});
		}
	}
}
