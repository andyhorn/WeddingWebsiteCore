using Microsoft.EntityFrameworkCore.Migrations;

namespace WeddingWebsiteCore.Migrations
{
    public partial class TestingAddingAddressBackToFamily : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_families_AddressId",
                table: "families",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_families_addresses_AddressId",
                table: "families",
                column: "AddressId",
                principalTable: "addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_families_addresses_AddressId",
                table: "families");

            migrationBuilder.DropIndex(
                name: "IX_families_AddressId",
                table: "families");
        }
    }
}
