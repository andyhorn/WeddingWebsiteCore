using Microsoft.EntityFrameworkCore.Migrations;

namespace WeddingWebsiteCore.Migrations
{
    public partial class FullyQualifiedFamilyModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_families_addresses_AddressId",
                table: "families");

            migrationBuilder.DropForeignKey(
                name: "FK_families_tiers_TierId",
                table: "families");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "families",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "AdditionalGuests",
                table: "families",
                newName: "additionalGuests");

            migrationBuilder.RenameColumn(
                name: "TierId",
                table: "families",
                newName: "FK_tierId");

            migrationBuilder.RenameColumn(
                name: "HeadMemberId",
                table: "families",
                newName: "FK_headMemberId");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "families",
                newName: "FK_addressId");

            migrationBuilder.RenameIndex(
                name: "IX_families_TierId",
                table: "families",
                newName: "IX_families_FK_tierId");

            migrationBuilder.RenameIndex(
                name: "IX_families_AddressId",
                table: "families",
                newName: "IX_families_FK_addressId");

            migrationBuilder.AddForeignKey(
                name: "FK_families_addresses_FK_addressId",
                table: "families",
                column: "FK_addressId",
                principalTable: "addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_families_tiers_FK_tierId",
                table: "families",
                column: "FK_tierId",
                principalTable: "tiers",
                principalColumn: "TierId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_families_addresses_FK_addressId",
                table: "families");

            migrationBuilder.DropForeignKey(
                name: "FK_families_tiers_FK_tierId",
                table: "families");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "families",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "additionalGuests",
                table: "families",
                newName: "AdditionalGuests");

            migrationBuilder.RenameColumn(
                name: "FK_tierId",
                table: "families",
                newName: "TierId");

            migrationBuilder.RenameColumn(
                name: "FK_headMemberId",
                table: "families",
                newName: "HeadMemberId");

            migrationBuilder.RenameColumn(
                name: "FK_addressId",
                table: "families",
                newName: "AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_families_FK_tierId",
                table: "families",
                newName: "IX_families_TierId");

            migrationBuilder.RenameIndex(
                name: "IX_families_FK_addressId",
                table: "families",
                newName: "IX_families_AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_families_addresses_AddressId",
                table: "families",
                column: "AddressId",
                principalTable: "addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_families_tiers_TierId",
                table: "families",
                column: "TierId",
                principalTable: "tiers",
                principalColumn: "TierId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
