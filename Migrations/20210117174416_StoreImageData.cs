using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WeddingWebsiteCore.Migrations
{
    public partial class StoreImageData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "images");

            migrationBuilder.AddColumn<byte[]>(
                name: "Data",
                table: "images",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "images");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "images",
                type: "text",
                nullable: true);
        }
    }
}
