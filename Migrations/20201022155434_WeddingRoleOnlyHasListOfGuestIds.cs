using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WeddingWebsiteCore.Migrations
{
    public partial class WeddingRoleOnlyHasListOfGuestIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_guests_wedding_roles_WeddingRoleId",
                table: "guests");

            migrationBuilder.DropIndex(
                name: "IX_guests_WeddingRoleId",
                table: "guests");

            migrationBuilder.DropColumn(
                name: "WeddingRoleId",
                table: "guests");

            migrationBuilder.AddColumn<List<int>>(
                name: "GuestIds",
                table: "wedding_roles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuestIds",
                table: "wedding_roles");

            migrationBuilder.AddColumn<int>(
                name: "WeddingRoleId",
                table: "guests",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_guests_WeddingRoleId",
                table: "guests",
                column: "WeddingRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_guests_wedding_roles_WeddingRoleId",
                table: "guests",
                column: "WeddingRoleId",
                principalTable: "wedding_roles",
                principalColumn: "WeddingRoleId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
