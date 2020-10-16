using Microsoft.EntityFrameworkCore.Migrations;

namespace WeddingWebsiteCore.Migrations
{
    public partial class SetCategoryIdNullInAccommodationOnCategoryDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_accommodations_addresses_AddressId",
                table: "accommodations");

            migrationBuilder.DropForeignKey(
                name: "FK_accommodations_categories_CategoryId",
                table: "accommodations");

            migrationBuilder.AddForeignKey(
                name: "FK_accommodations_addresses_AddressId",
                table: "accommodations",
                column: "AddressId",
                principalTable: "addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_accommodations_categories_CategoryId",
                table: "accommodations",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_accommodations_addresses_AddressId",
                table: "accommodations");

            migrationBuilder.DropForeignKey(
                name: "FK_accommodations_categories_CategoryId",
                table: "accommodations");

            migrationBuilder.AddForeignKey(
                name: "FK_accommodations_addresses_AddressId",
                table: "accommodations",
                column: "AddressId",
                principalTable: "addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_accommodations_categories_CategoryId",
                table: "accommodations",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
