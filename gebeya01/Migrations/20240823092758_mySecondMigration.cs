using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gebeya01.Migrations
{
    /// <inheritdoc />
    public partial class mySecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CartID",
                table: "CartItems");

            migrationBuilder.AddColumn<bool>(
                name: "IsInCart",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsInWishList",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInCart",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsInWishList",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "CartID",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
