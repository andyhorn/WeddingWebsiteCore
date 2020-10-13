using Microsoft.EntityFrameworkCore.Migrations;

namespace WeddingWebsiteCore.Migrations
{
    public partial class FullyQualifiedVendorModelAndAddedAddressProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Url",
                table: "vendors",
                newName: "url");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "vendors",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "vendors",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "ContactPhone",
                table: "vendors",
                newName: "contactPhone");

            migrationBuilder.RenameColumn(
                name: "ContactEmail",
                table: "vendors",
                newName: "contactEmail");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "vendors",
                newName: "FK_addressId");

            migrationBuilder.CreateIndex(
                name: "IX_vendors_FK_addressId",
                table: "vendors",
                column: "FK_addressId");

            migrationBuilder.AddForeignKey(
                name: "FK_vendors_addresses_FK_addressId",
                table: "vendors",
                column: "FK_addressId",
                principalTable: "addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vendors_addresses_FK_addressId",
                table: "vendors");

            migrationBuilder.DropIndex(
                name: "IX_vendors_FK_addressId",
                table: "vendors");

            migrationBuilder.RenameColumn(
                name: "url",
                table: "vendors",
                newName: "Url");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "vendors",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "vendors",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "contactPhone",
                table: "vendors",
                newName: "ContactPhone");

            migrationBuilder.RenameColumn(
                name: "contactEmail",
                table: "vendors",
                newName: "ContactEmail");

            migrationBuilder.RenameColumn(
                name: "FK_addressId",
                table: "vendors",
                newName: "AddressId");
        }
    }
}
