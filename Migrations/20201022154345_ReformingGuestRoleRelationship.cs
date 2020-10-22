using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WeddingWebsiteCore.Migrations
{
    public partial class ReformingGuestRoleRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuestWeddingRoles");

            migrationBuilder.AddColumn<int>(
                name: "WeddingRoleId",
                table: "guests",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "GuestWeddingRoles",
                columns: table => new
                {
                    GuestWeddingRoleId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GuestId = table.Column<int>(type: "integer", nullable: false),
                    WeddingRoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestWeddingRoles", x => x.GuestWeddingRoleId);
                    table.ForeignKey(
                        name: "FK_GuestWeddingRoles_guests_GuestId",
                        column: x => x.GuestId,
                        principalTable: "guests",
                        principalColumn: "GuestId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GuestWeddingRoles_wedding_roles_WeddingRoleId",
                        column: x => x.WeddingRoleId,
                        principalTable: "wedding_roles",
                        principalColumn: "WeddingRoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GuestWeddingRoles_GuestId",
                table: "GuestWeddingRoles",
                column: "GuestId");

            migrationBuilder.CreateIndex(
                name: "IX_GuestWeddingRoles_WeddingRoleId",
                table: "GuestWeddingRoles",
                column: "WeddingRoleId");
        }
    }
}
