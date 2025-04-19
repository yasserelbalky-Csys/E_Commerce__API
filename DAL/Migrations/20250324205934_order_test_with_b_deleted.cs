using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
	/// <inheritdoc />
	public partial class order_test_with_b_deleted : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.AddColumn<bool>(
				name: "b_deleted",
				table: "OrderMaster",
				type: "bit",
				nullable: false,
				defaultValue: false);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropColumn(
				name: "b_deleted",
				table: "OrderMaster");
		}
	}
}
