using Microsoft.EntityFrameworkCore.Migrations;

namespace WeddingWebsiteCore.Migrations
{
    public partial class ChangeToRsvpCollectionOnGuestModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_guests_rsvps_RsvpId",
                table: "guests");

            migrationBuilder.DropIndex(
                name: "IX_guests_RsvpId",
                table: "guests");

            migrationBuilder.DropColumn(
                name: "RsvpId",
                table: "guests");

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "guests",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_rsvps_GuestId",
                table: "rsvps",
                column: "GuestId");

            migrationBuilder.AddForeignKey(
                name: "FK_rsvps_guests_GuestId",
                table: "rsvps",
                column: "GuestId",
                principalTable: "guests",
                principalColumn: "GuestId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_rsvps_guests_GuestId",
                table: "rsvps");

            migrationBuilder.DropIndex(
                name: "IX_rsvps_GuestId",
                table: "rsvps");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "guests");

            migrationBuilder.AddColumn<int>(
                name: "RsvpId",
                table: "guests",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_guests_RsvpId",
                table: "guests",
                column: "RsvpId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_guests_rsvps_RsvpId",
                table: "guests",
                column: "RsvpId",
                principalTable: "rsvps",
                principalColumn: "RsvpId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
