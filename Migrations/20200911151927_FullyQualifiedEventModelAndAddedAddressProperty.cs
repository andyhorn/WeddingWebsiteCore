using Microsoft.EntityFrameworkCore.Migrations;

namespace WeddingWebsiteCore.Migrations
{
    public partial class FullyQualifiedEventModelAndAddedAddressProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "events",
                newName: "startTime");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "events",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                table: "events",
                newName: "endTime");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "events",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "events",
                newName: "eventId");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "events",
                newName: "FK_addressId");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "events",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_events_FK_addressId",
                table: "events",
                column: "FK_addressId");

            migrationBuilder.AddForeignKey(
                name: "FK_events_addresses_FK_addressId",
                table: "events",
                column: "FK_addressId",
                principalTable: "addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_events_addresses_FK_addressId",
                table: "events");

            migrationBuilder.DropIndex(
                name: "IX_events_FK_addressId",
                table: "events");

            migrationBuilder.RenameColumn(
                name: "startTime",
                table: "events",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "events",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "endTime",
                table: "events",
                newName: "EndTime");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "events",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "eventId",
                table: "events",
                newName: "EventId");

            migrationBuilder.RenameColumn(
                name: "FK_addressId",
                table: "events",
                newName: "AddressId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "events",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
