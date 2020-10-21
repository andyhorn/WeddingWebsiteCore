using Microsoft.EntityFrameworkCore.Migrations;

namespace WeddingWebsiteCore.Migrations
{
    public partial class SeedDataUsingNegativeIntegers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "wedding_roles",
                keyColumn: "WeddingRoleId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "wedding_roles",
                keyColumn: "WeddingRoleId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "wedding_roles",
                keyColumn: "WeddingRoleId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "wedding_roles",
                keyColumn: "WeddingRoleId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "wedding_roles",
                keyColumn: "WeddingRoleId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "wedding_roles",
                keyColumn: "WeddingRoleId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "wedding_roles",
                keyColumn: "WeddingRoleId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "wedding_roles",
                keyColumn: "WeddingRoleId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "wedding_roles",
                keyColumn: "WeddingRoleId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "wedding_roles",
                keyColumn: "WeddingRoleId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "wedding_roles",
                keyColumn: "WeddingRoleId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "wedding_roles",
                keyColumn: "WeddingRoleId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "wedding_roles",
                keyColumn: "WeddingRoleId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "wedding_roles",
                keyColumn: "WeddingRoleId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "wedding_roles",
                keyColumn: "WeddingRoleId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "wedding_roles",
                keyColumn: "WeddingRoleId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "wedding_roles",
                keyColumn: "WeddingRoleId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "wedding_roles",
                keyColumn: "WeddingRoleId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "wedding_roles",
                keyColumn: "WeddingRoleId",
                keyValue: 19);

            migrationBuilder.InsertData(
                table: "wedding_roles",
                columns: new[] { "WeddingRoleId", "Description", "Name" },
                values: new object[,]
                {
                    { -1, null, "Bride" },
                    { -17, null, "Grandfather of the Groom" },
                    { -16, null, "Grandmother of the Groom" },
                    { -15, null, "Grandfather of the Bride" },
                    { -14, null, "Grandmother of the Bride" },
                    { -13, null, "Mother of the Groom" },
                    { -12, null, "Father of the Groom" },
                    { -11, null, "Mother of the Bride" },
                    { -18, null, "Junior Bridesmaid" },
                    { -10, null, "Father of the Bride" },
                    { -8, null, "Ring Bearer" },
                    { -7, null, "Flower Girl" },
                    { -6, null, "Groomsman" },
                    { -5, null, "Bridesmaid" },
                    { -4, null, "Best Man" },
                    { -3, null, "Maid of Honor" },
                    { -2, null, "Groom" },
                    { -9, null, "Officiant" },
                    { -19, null, "Usher" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "wedding_roles",
                keyColumn: "WeddingRoleId",
                keyValue: -19);

            migrationBuilder.DeleteData(
                table: "wedding_roles",
                keyColumn: "WeddingRoleId",
                keyValue: -18);

            migrationBuilder.DeleteData(
                table: "wedding_roles",
                keyColumn: "WeddingRoleId",
                keyValue: -17);

            migrationBuilder.DeleteData(
                table: "wedding_roles",
                keyColumn: "WeddingRoleId",
                keyValue: -16);

            migrationBuilder.DeleteData(
                table: "wedding_roles",
                keyColumn: "WeddingRoleId",
                keyValue: -15);

            migrationBuilder.DeleteData(
                table: "wedding_roles",
                keyColumn: "WeddingRoleId",
                keyValue: -14);

            migrationBuilder.DeleteData(
                table: "wedding_roles",
                keyColumn: "WeddingRoleId",
                keyValue: -13);

            migrationBuilder.DeleteData(
                table: "wedding_roles",
                keyColumn: "WeddingRoleId",
                keyValue: -12);

            migrationBuilder.DeleteData(
                table: "wedding_roles",
                keyColumn: "WeddingRoleId",
                keyValue: -11);

            migrationBuilder.DeleteData(
                table: "wedding_roles",
                keyColumn: "WeddingRoleId",
                keyValue: -10);

            migrationBuilder.DeleteData(
                table: "wedding_roles",
                keyColumn: "WeddingRoleId",
                keyValue: -9);

            migrationBuilder.DeleteData(
                table: "wedding_roles",
                keyColumn: "WeddingRoleId",
                keyValue: -8);

            migrationBuilder.DeleteData(
                table: "wedding_roles",
                keyColumn: "WeddingRoleId",
                keyValue: -7);

            migrationBuilder.DeleteData(
                table: "wedding_roles",
                keyColumn: "WeddingRoleId",
                keyValue: -6);

            migrationBuilder.DeleteData(
                table: "wedding_roles",
                keyColumn: "WeddingRoleId",
                keyValue: -5);

            migrationBuilder.DeleteData(
                table: "wedding_roles",
                keyColumn: "WeddingRoleId",
                keyValue: -4);

            migrationBuilder.DeleteData(
                table: "wedding_roles",
                keyColumn: "WeddingRoleId",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "wedding_roles",
                keyColumn: "WeddingRoleId",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "wedding_roles",
                keyColumn: "WeddingRoleId",
                keyValue: -1);

            migrationBuilder.InsertData(
                table: "wedding_roles",
                columns: new[] { "WeddingRoleId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, null, "Bride" },
                    { 17, null, "Grandfather of the Groom" },
                    { 16, null, "Grandmother of the Groom" },
                    { 15, null, "Grandfather of the Bride" },
                    { 14, null, "Grandmother of the Bride" },
                    { 13, null, "Mother of the Groom" },
                    { 12, null, "Father of the Groom" },
                    { 11, null, "Mother of the Bride" },
                    { 18, null, "Junior Bridesmaid" },
                    { 10, null, "Father of the Bride" },
                    { 8, null, "Ring Bearer" },
                    { 7, null, "Flower Girl" },
                    { 6, null, "Groomsman" },
                    { 5, null, "Bridesmaid" },
                    { 4, null, "Best Man" },
                    { 3, null, "Maid of Honor" },
                    { 2, null, "Groom" },
                    { 9, null, "Officiant" },
                    { 19, null, "Usher" }
                });
        }
    }
}
