using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gebeya01.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUserIdRedundant2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishlistItems_Persons_PersonUserID",
                table: "WishlistItems");

            migrationBuilder.AlterColumn<int>(
                name: "PersonUserID",
                table: "WishlistItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_WishlistItems_Persons_PersonUserID",
                table: "WishlistItems",
                column: "PersonUserID",
                principalTable: "Persons",
                principalColumn: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishlistItems_Persons_PersonUserID",
                table: "WishlistItems");

            migrationBuilder.AlterColumn<int>(
                name: "PersonUserID",
                table: "WishlistItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WishlistItems_Persons_PersonUserID",
                table: "WishlistItems",
                column: "PersonUserID",
                principalTable: "Persons",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
