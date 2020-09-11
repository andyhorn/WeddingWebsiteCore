using Microsoft.EntityFrameworkCore.Migrations;

namespace WeddingWebsiteCore.Migrations
{
    public partial class FullyQualifiedAddressModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StreetNumber",
                table: "addresses",
                newName: "streetNumber");

            migrationBuilder.RenameColumn(
                name: "StreetName",
                table: "addresses",
                newName: "streetName");

            migrationBuilder.RenameColumn(
                name: "StreetDetail",
                table: "addresses",
                newName: "streetDetail");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "addresses",
                newName: "state");

            migrationBuilder.RenameColumn(
                name: "PostalCode",
                table: "addresses",
                newName: "postalCode");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "addresses",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "addresses",
                newName: "country");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "addresses",
                newName: "city");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "streetNumber",
                table: "addresses",
                newName: "StreetNumber");

            migrationBuilder.RenameColumn(
                name: "streetName",
                table: "addresses",
                newName: "StreetName");

            migrationBuilder.RenameColumn(
                name: "streetDetail",
                table: "addresses",
                newName: "StreetDetail");

            migrationBuilder.RenameColumn(
                name: "state",
                table: "addresses",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "postalCode",
                table: "addresses",
                newName: "PostalCode");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "addresses",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "country",
                table: "addresses",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "city",
                table: "addresses",
                newName: "City");
        }
    }
}
