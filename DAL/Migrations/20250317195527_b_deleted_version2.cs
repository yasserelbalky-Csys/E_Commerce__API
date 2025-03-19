using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class b_deleted_version2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "b_deleted",
                table: "subCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "b_deleted",
                table: "stores",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "b_deleted",
                table: "products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "b_deleted",
                table: "brands",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "b_deleted",
                table: "subCategories");

            migrationBuilder.DropColumn(
                name: "b_deleted",
                table: "stores");

            migrationBuilder.DropColumn(
                name: "b_deleted",
                table: "products");

            migrationBuilder.DropColumn(
                name: "b_deleted",
                table: "brands");
        }
    }
}
