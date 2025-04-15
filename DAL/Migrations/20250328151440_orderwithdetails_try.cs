using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
	/// <inheritdoc />
	public partial class orderwithdetails_try : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropPrimaryKey(
				name: "PK_OrderDetails",
				table: "OrderDetails");

			migrationBuilder.AlterColumn<int>(
				name: "LineNo",
				table: "OrderDetails",
				type: "int",
				nullable: false,
				oldClrType: typeof(int),
				oldType: "int");


			migrationBuilder.AddPrimaryKey(
				name: "PK_OrderDetails",
				table: "OrderDetails",
				column: "LineNo");

			migrationBuilder.CreateIndex(
				name: "IX_OrderDetails_OrderNo",
				table: "OrderDetails",
				column: "OrderNo");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropPrimaryKey(
				name: "PK_OrderDetails",
				table: "OrderDetails");

			migrationBuilder.DropIndex(
				name: "IX_OrderDetails_OrderNo",
				table: "OrderDetails");

			migrationBuilder.AlterColumn<int>(
				name: "LineNo",
				table: "OrderDetails",
				type: "int",
				nullable: false,
				oldClrType: typeof(int),
				oldType: "int")
				.OldAnnotation("SqlServer:Identity", "1, 1");

			migrationBuilder.AddPrimaryKey(
				name: "PK_OrderDetails",
				table: "OrderDetails",
				columns: new[] { "OrderNo", "LineNo", "ProductId" });
		}
	}
}
