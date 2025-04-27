using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class add_id_to_current_product_balance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "b_order_cancel",
                table: "CurrentProductBalance");

            migrationBuilder.DropColumn(
                name: "b_order_done",
                table: "CurrentProductBalance");

            migrationBuilder.DropColumn(
                name: "b_order_pending",
                table: "CurrentProductBalance");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "CurrentProductBalance",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CurrentProductBalance",
                table: "CurrentProductBalance",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CurrentProductBalance",
                table: "CurrentProductBalance");

            migrationBuilder.DropColumn(
                name: "id",
                table: "CurrentProductBalance");

            migrationBuilder.AddColumn<bool>(
                name: "b_order_cancel",
                table: "CurrentProductBalance",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "b_order_done",
                table: "CurrentProductBalance",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "b_order_pending",
                table: "CurrentProductBalance",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
