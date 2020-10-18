using Microsoft.EntityFrameworkCore.Migrations;

namespace WeddingWebsiteCore.Migrations
{
    public partial class SetNullWhenFamilyDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_guests_families_FamilyId",
                table: "guests");

            migrationBuilder.AddForeignKey(
                name: "FK_guests_families_FamilyId",
                table: "guests",
                column: "FamilyId",
                principalTable: "families",
                principalColumn: "FamilyId",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_guests_families_FamilyId",
                table: "guests");

            migrationBuilder.AddForeignKey(
                name: "FK_guests_families_FamilyId",
                table: "guests",
                column: "FamilyId",
                principalTable: "families",
                principalColumn: "FamilyId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
