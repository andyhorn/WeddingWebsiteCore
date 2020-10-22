using Microsoft.EntityFrameworkCore.Migrations;

namespace WeddingWebsiteCore.Migrations
{
    public partial class GuestWeddingRolesAddedToContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GuestWeddingRole_guests_GuestId",
                table: "GuestWeddingRole");

            migrationBuilder.DropForeignKey(
                name: "FK_GuestWeddingRole_wedding_roles_WeddingRoleId",
                table: "GuestWeddingRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GuestWeddingRole",
                table: "GuestWeddingRole");

            migrationBuilder.RenameTable(
                name: "GuestWeddingRole",
                newName: "GuestWeddingRoles");

            migrationBuilder.RenameIndex(
                name: "IX_GuestWeddingRole_WeddingRoleId",
                table: "GuestWeddingRoles",
                newName: "IX_GuestWeddingRoles_WeddingRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_GuestWeddingRole_GuestId",
                table: "GuestWeddingRoles",
                newName: "IX_GuestWeddingRoles_GuestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GuestWeddingRoles",
                table: "GuestWeddingRoles",
                column: "GuestWeddingRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_GuestWeddingRoles_guests_GuestId",
                table: "GuestWeddingRoles",
                column: "GuestId",
                principalTable: "guests",
                principalColumn: "GuestId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GuestWeddingRoles_wedding_roles_WeddingRoleId",
                table: "GuestWeddingRoles",
                column: "WeddingRoleId",
                principalTable: "wedding_roles",
                principalColumn: "WeddingRoleId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GuestWeddingRoles_guests_GuestId",
                table: "GuestWeddingRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_GuestWeddingRoles_wedding_roles_WeddingRoleId",
                table: "GuestWeddingRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GuestWeddingRoles",
                table: "GuestWeddingRoles");

            migrationBuilder.RenameTable(
                name: "GuestWeddingRoles",
                newName: "GuestWeddingRole");

            migrationBuilder.RenameIndex(
                name: "IX_GuestWeddingRoles_WeddingRoleId",
                table: "GuestWeddingRole",
                newName: "IX_GuestWeddingRole_WeddingRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_GuestWeddingRoles_GuestId",
                table: "GuestWeddingRole",
                newName: "IX_GuestWeddingRole_GuestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GuestWeddingRole",
                table: "GuestWeddingRole",
                column: "GuestWeddingRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_GuestWeddingRole_guests_GuestId",
                table: "GuestWeddingRole",
                column: "GuestId",
                principalTable: "guests",
                principalColumn: "GuestId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GuestWeddingRole_wedding_roles_WeddingRoleId",
                table: "GuestWeddingRole",
                column: "WeddingRoleId",
                principalTable: "wedding_roles",
                principalColumn: "WeddingRoleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
