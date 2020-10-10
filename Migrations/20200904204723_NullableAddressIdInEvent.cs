using Microsoft.EntityFrameworkCore.Migrations;

namespace WeddingWebsiteCore.Migrations
{
    public partial class NullableAddressIdInEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_events_addresses_AddressId",
                table: "events");

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "events",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_events_addresses_AddressId",
                table: "events",
                column: "AddressId",
                principalTable: "addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_events_addresses_AddressId",
                table: "events");

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "events",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_events_addresses_AddressId",
                table: "events",
                column: "AddressId",
                principalTable: "addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
