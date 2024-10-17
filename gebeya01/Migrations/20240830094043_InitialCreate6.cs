using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gebeya01.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishlistItems_Products_ProductID",
                table: "WishlistItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Wishlists_Persons_PersonUserID",
                table: "Wishlists");

            migrationBuilder.DropTable(
                name: "PersonAddress");

            migrationBuilder.DropIndex(
                name: "IX_Wishlists_PersonUserID",
                table: "Wishlists");

            migrationBuilder.DropColumn(
                name: "PersonUserID",
                table: "Wishlists");

            migrationBuilder.AddColumn<int>(
                name: "ProductID1",
                table: "WishlistItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_UserID",
                table: "Wishlists",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistItems_ProductID1",
                table: "WishlistItems",
                column: "ProductID1");

            migrationBuilder.AddForeignKey(
                name: "FK_WishlistItems_Products_ProductID",
                table: "WishlistItems",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WishlistItems_Products_ProductID1",
                table: "WishlistItems",
                column: "ProductID1",
                principalTable: "Products",
                principalColumn: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlists_Persons_UserID",
                table: "Wishlists",
                column: "UserID",
                principalTable: "Persons",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishlistItems_Products_ProductID",
                table: "WishlistItems");

            migrationBuilder.DropForeignKey(
                name: "FK_WishlistItems_Products_ProductID1",
                table: "WishlistItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Wishlists_Persons_UserID",
                table: "Wishlists");

            migrationBuilder.DropIndex(
                name: "IX_Wishlists_UserID",
                table: "Wishlists");

            migrationBuilder.DropIndex(
                name: "IX_WishlistItems_ProductID1",
                table: "WishlistItems");

            migrationBuilder.DropColumn(
                name: "ProductID1",
                table: "WishlistItems");

            migrationBuilder.AddColumn<int>(
                name: "PersonUserID",
                table: "Wishlists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PersonAddress",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonAddress", x => new { x.UserId, x.AddressId });
                    table.ForeignKey(
                        name: "FK_PersonAddress_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonAddress_Persons_UserId",
                        column: x => x.UserId,
                        principalTable: "Persons",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_PersonUserID",
                table: "Wishlists",
                column: "PersonUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonAddress_AddressId",
                table: "PersonAddress",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_WishlistItems_Products_ProductID",
                table: "WishlistItems",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlists_Persons_PersonUserID",
                table: "Wishlists",
                column: "PersonUserID",
                principalTable: "Persons",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
