using Microsoft.EntityFrameworkCore.Migrations;

namespace WeddingWebsiteCore.Migrations
{
    public partial class RemoveAddressPropertyFromFamilyAndVendor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_families_addresses_AddressId",
                table: "families");

            migrationBuilder.DropForeignKey(
                name: "FK_vendors_addresses_AddressId",
                table: "vendors");

            migrationBuilder.DropIndex(
                name: "IX_vendors_AddressId",
                table: "vendors");

            migrationBuilder.DropIndex(
                name: "IX_families_AddressId",
                table: "families");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "vendors",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "vendors",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "vendors",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "vendors",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_vendors_AddressId",
                table: "vendors",
                column: "AddressId");

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
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_vendors_addresses_AddressId",
                table: "vendors",
                column: "AddressId",
                principalTable: "addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
