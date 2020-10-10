using Microsoft.EntityFrameworkCore.Migrations;

namespace WeddingWebsiteCore.Migrations
{
    public partial class RemoveAddressPropertyFromEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_events_addresses_AddressId",
                table: "events");

            migrationBuilder.DropIndex(
                name: "IX_events_AddressId",
                table: "events");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_events_AddressId",
                table: "events",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_events_addresses_AddressId",
                table: "events",
                column: "AddressId",
                principalTable: "addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
