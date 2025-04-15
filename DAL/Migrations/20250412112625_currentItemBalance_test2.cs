using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class currentItemBalance_test2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "b_order_Pending",
                table: "CurrentProductBalance",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "b_order_Pending",
                table: "CurrentProductBalance",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
