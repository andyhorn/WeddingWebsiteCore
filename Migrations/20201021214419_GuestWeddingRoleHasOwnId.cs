using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WeddingWebsiteCore.Migrations
{
    public partial class GuestWeddingRoleHasOwnId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GuestWeddingRole",
                table: "GuestWeddingRole");

            migrationBuilder.AddColumn<int>(
                name: "GuestWeddingRoleId",
                table: "GuestWeddingRole",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GuestWeddingRole",
                table: "GuestWeddingRole",
                column: "GuestWeddingRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_GuestWeddingRole_GuestId",
                table: "GuestWeddingRole",
                column: "GuestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GuestWeddingRole",
                table: "GuestWeddingRole");

            migrationBuilder.DropIndex(
                name: "IX_GuestWeddingRole_GuestId",
                table: "GuestWeddingRole");

            migrationBuilder.DropColumn(
                name: "GuestWeddingRoleId",
                table: "GuestWeddingRole");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GuestWeddingRole",
                table: "GuestWeddingRole",
                columns: new[] { "GuestId", "WeddingRoleId" });
        }
    }
}
