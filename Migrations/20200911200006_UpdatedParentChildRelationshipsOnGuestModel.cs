using Microsoft.EntityFrameworkCore.Migrations;

namespace WeddingWebsiteCore.Migrations
{
    public partial class UpdatedParentChildRelationshipsOnGuestModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_guests_ParentId",
                table: "guests",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_guests_guests_ParentId",
                table: "guests",
                column: "ParentId",
                principalTable: "guests",
                principalColumn: "GuestId",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_guests_guests_ParentId",
                table: "guests");

            migrationBuilder.DropIndex(
                name: "IX_guests_ParentId",
                table: "guests");
        }
    }
}
