using Microsoft.EntityFrameworkCore.Migrations;

namespace WeddingWebsiteCore.Migrations
{
    public partial class FamilylessGuests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_guests_families_FamilyId",
                table: "guests");

            migrationBuilder.AlterColumn<int>(
                name: "HeadMemberId",
                table: "families",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_guests_families_FamilyId",
                table: "guests",
                column: "FamilyId",
                principalTable: "families",
                principalColumn: "FamilyId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_guests_families_FamilyId",
                table: "guests");

            migrationBuilder.AlterColumn<int>(
                name: "HeadMemberId",
                table: "families",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_guests_families_FamilyId",
                table: "guests",
                column: "FamilyId",
                principalTable: "families",
                principalColumn: "FamilyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
