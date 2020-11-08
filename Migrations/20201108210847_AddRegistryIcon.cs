using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WeddingWebsiteCore.Migrations
{
    public partial class AddRegistryIcon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IconId",
                table: "registries",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "registry_icons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(nullable: true),
                    Data = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_registry_icons", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_registries_IconId",
                table: "registries",
                column: "IconId");

            migrationBuilder.AddForeignKey(
                name: "FK_registries_registry_icons_IconId",
                table: "registries",
                column: "IconId",
                principalTable: "registry_icons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_registries_registry_icons_IconId",
                table: "registries");

            migrationBuilder.DropTable(
                name: "registry_icons");

            migrationBuilder.DropIndex(
                name: "IX_registries_IconId",
                table: "registries");

            migrationBuilder.DropColumn(
                name: "IconId",
                table: "registries");
        }
    }
}
