using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class first_test_of_discount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "OrderDetails");

            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "OrderMaster",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "OrderMaster");

            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "OrderDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
