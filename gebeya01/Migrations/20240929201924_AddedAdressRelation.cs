using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gebeya01.Migrations
{
    /// <inheritdoc />
    public partial class AddedAdressRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Addresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_UserID",
                table: "Addresses",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Persons_UserID",
                table: "Addresses",
                column: "UserID",
                principalTable: "Persons",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Persons_UserID",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_UserID",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Addresses");
        }
    }
}
