using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
	/// <inheritdoc />
	public partial class test_first_b_deleted : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder) {


			migrationBuilder.AddColumn<bool>(
				name: "b_deleted",
				table: "Categories",
				type: "bit",
				nullable: false,
				defaultValue: false);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropColumn(
				name: "b_deleted",
				table: "Categories");


		}
	}
}
