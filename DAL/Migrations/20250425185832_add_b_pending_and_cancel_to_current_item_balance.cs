using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class add_b_pending_and_cancel_to_current_item_balance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "b_cancel",
                table: "CurrentProductBalance",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "b_pending",
                table: "CurrentProductBalance",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "b_cancel",
                table: "CurrentProductBalance");

            migrationBuilder.DropColumn(
                name: "b_pending",
                table: "CurrentProductBalance");
        }
    }
}
