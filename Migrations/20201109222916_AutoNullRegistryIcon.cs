using Microsoft.EntityFrameworkCore.Migrations;

namespace WeddingWebsiteCore.Migrations
{
    public partial class AutoNullRegistryIcon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_registries_registry_icons_IconId",
                table: "registries");

            migrationBuilder.AddForeignKey(
                name: "FK_registries_registry_icons_IconId",
                table: "registries",
                column: "IconId",
                principalTable: "registry_icons",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_registries_registry_icons_IconId",
                table: "registries");

            migrationBuilder.AddForeignKey(
                name: "FK_registries_registry_icons_IconId",
                table: "registries",
                column: "IconId",
                principalTable: "registry_icons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
