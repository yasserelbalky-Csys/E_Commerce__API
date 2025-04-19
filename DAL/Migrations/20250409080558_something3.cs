using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
	/// <inheritdoc />
	public partial class something3 : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropForeignKey(
				name: "FK_subCategories_categories_CategoryId",
				table: "subCategories");

			migrationBuilder.DropPrimaryKey(
				name: "PK_categories",
				table: "categories");

			migrationBuilder.RenameTable(
				name: "categories",
				newName: "Categories");

			migrationBuilder.AddPrimaryKey(
				name: "PK_Categories",
				table: "Categories",
				column: "CategoryId");

			migrationBuilder.AddForeignKey(
				name: "FK_subCategories_Categories_CategoryId",
				table: "subCategories",
				column: "CategoryId",
				principalTable: "Categories",
				principalColumn: "CategoryId",
				onDelete: ReferentialAction.Cascade);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropForeignKey(
				name: "FK_subCategories_Categories_CategoryId",
				table: "subCategories");

			migrationBuilder.DropPrimaryKey(
				name: "PK_Categories",
				table: "Categories");

			migrationBuilder.RenameTable(
				name: "Categories",
				newName: "categories");

			migrationBuilder.AddPrimaryKey(
				name: "PK_categories",
				table: "categories",
				column: "CategoryId");

			migrationBuilder.AddForeignKey(
				name: "FK_subCategories_categories_CategoryId",
				table: "subCategories",
				column: "CategoryId",
				principalTable: "categories",
				principalColumn: "CategoryId",
				onDelete: ReferentialAction.Cascade);
		}
	}
}
