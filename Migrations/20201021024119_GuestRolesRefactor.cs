using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WeddingWebsiteCore.Migrations
{
    public partial class GuestRolesRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "wedding_member_roles");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "guests");

            migrationBuilder.CreateTable(
                name: "GuestWeddingRole",
                columns: table => new
                {
                    GuestId = table.Column<int>(nullable: false),
                    WeddingRoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestWeddingRole", x => new { x.GuestId, x.WeddingRoleId });
                    table.ForeignKey(
                        name: "FK_GuestWeddingRole_guests_GuestId",
                        column: x => x.GuestId,
                        principalTable: "guests",
                        principalColumn: "GuestId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GuestWeddingRole_wedding_roles_WeddingRoleId",
                        column: x => x.WeddingRoleId,
                        principalTable: "wedding_roles",
                        principalColumn: "WeddingRoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GuestWeddingRole_WeddingRoleId",
                table: "GuestWeddingRole",
                column: "WeddingRoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuestWeddingRole");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "guests",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "wedding_member_roles",
                columns: table => new
                {
                    WeddingMemberRoleId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WeddingMemberId = table.Column<int>(type: "integer", nullable: false),
                    WeddingRoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wedding_member_roles", x => x.WeddingMemberRoleId);
                    table.ForeignKey(
                        name: "FK_wedding_member_roles_wedding_roles_WeddingMemberId",
                        column: x => x.WeddingMemberId,
                        principalTable: "wedding_roles",
                        principalColumn: "WeddingRoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_wedding_member_roles_guests_WeddingRoleId",
                        column: x => x.WeddingRoleId,
                        principalTable: "guests",
                        principalColumn: "GuestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_wedding_member_roles_WeddingMemberId",
                table: "wedding_member_roles",
                column: "WeddingMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_wedding_member_roles_WeddingRoleId",
                table: "wedding_member_roles",
                column: "WeddingRoleId");
        }
    }
}
