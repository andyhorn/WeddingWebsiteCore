using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WeddingWebsiteCore.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "addresses",
                columns: table => new
                {
                    AddressId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    StreetNumber = table.Column<string>(nullable: true),
                    StreetName = table.Column<string>(nullable: true),
                    StreetDetail = table.Column<string>(nullable: true),
                    State = table.Column<string>(type: "char(2)", nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_addresses", x => x.AddressId);
                });

            migrationBuilder.CreateTable(
                name: "images",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "registries",
                columns: table => new
                {
                    RegistryId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_registries", x => x.RegistryId);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wedding_roles",
                columns: table => new
                {
                    WeddingRoleId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wedding_roles", x => x.WeddingRoleId);
                });

            migrationBuilder.CreateTable(
                name: "events",
                columns: table => new
                {
                    EventId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    AddressId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_events_addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "families",
                columns: table => new
                {
                    FamilyId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    AdditionalGuests = table.Column<int>(nullable: false),
                    AddressId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_families", x => x.FamilyId);
                    table.ForeignKey(
                        name: "FK_families_addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vendors",
                columns: table => new
                {
                    VendorId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ContactPhone = table.Column<string>(nullable: true),
                    ContactEmail = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    AddressId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vendors", x => x.VendorId);
                    table.ForeignKey(
                        name: "FK_vendors_addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rsvps",
                columns: table => new
                {
                    RsvpId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HasResponded = table.Column<bool>(nullable: false),
                    IsAttending = table.Column<bool>(nullable: false),
                    GuestId = table.Column<int>(nullable: false),
                    EventId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rsvps", x => x.RsvpId);
                    table.ForeignKey(
                        name: "FK_rsvps_events_EventId",
                        column: x => x.EventId,
                        principalTable: "events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "guests",
                columns: table => new
                {
                    GuestId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    IsWeddingMember = table.Column<bool>(nullable: false),
                    IsChild = table.Column<bool>(nullable: false),
                    RsvpId = table.Column<int>(nullable: false),
                    FamilyId = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    FamilyId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_guests", x => x.GuestId);
                    table.ForeignKey(
                        name: "FK_guests_families_FamilyId",
                        column: x => x.FamilyId,
                        principalTable: "families",
                        principalColumn: "FamilyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_guests_families_FamilyId1",
                        column: x => x.FamilyId1,
                        principalTable: "families",
                        principalColumn: "FamilyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_guests_rsvps_RsvpId",
                        column: x => x.RsvpId,
                        principalTable: "rsvps",
                        principalColumn: "RsvpId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "wedding_member_roles",
                columns: table => new
                {
                    WeddingMemberRoleId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WeddingMemberId = table.Column<int>(nullable: false),
                    WeddingRoleId = table.Column<int>(nullable: false)
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

            migrationBuilder.InsertData(
                table: "wedding_roles",
                columns: new[] { "WeddingRoleId", "Description", "Name" },
                values: new object[] { 1, null, "Bride" });

            migrationBuilder.InsertData(
                table: "wedding_roles",
                columns: new[] { "WeddingRoleId", "Description", "Name" },
                values: new object[] { 17, null, "Grandfather of the Groom" });

            migrationBuilder.InsertData(
                table: "wedding_roles",
                columns: new[] { "WeddingRoleId", "Description", "Name" },
                values: new object[] { 16, null, "Grandmother of the Groom" });

            migrationBuilder.InsertData(
                table: "wedding_roles",
                columns: new[] { "WeddingRoleId", "Description", "Name" },
                values: new object[] { 15, null, "Grandfather of the Bride" });

            migrationBuilder.InsertData(
                table: "wedding_roles",
                columns: new[] { "WeddingRoleId", "Description", "Name" },
                values: new object[] { 14, null, "Grandmother of the Bride" });

            migrationBuilder.InsertData(
                table: "wedding_roles",
                columns: new[] { "WeddingRoleId", "Description", "Name" },
                values: new object[] { 13, null, "Mother of the Groom" });

            migrationBuilder.InsertData(
                table: "wedding_roles",
                columns: new[] { "WeddingRoleId", "Description", "Name" },
                values: new object[] { 12, null, "Father of the Groom" });

            migrationBuilder.InsertData(
                table: "wedding_roles",
                columns: new[] { "WeddingRoleId", "Description", "Name" },
                values: new object[] { 11, null, "Mother of the Bride" });

            migrationBuilder.InsertData(
                table: "wedding_roles",
                columns: new[] { "WeddingRoleId", "Description", "Name" },
                values: new object[] { 18, null, "Junior Bridesmaid" });

            migrationBuilder.InsertData(
                table: "wedding_roles",
                columns: new[] { "WeddingRoleId", "Description", "Name" },
                values: new object[] { 10, null, "Father of the Bride" });

            migrationBuilder.InsertData(
                table: "wedding_roles",
                columns: new[] { "WeddingRoleId", "Description", "Name" },
                values: new object[] { 8, null, "Ring Bearer" });

            migrationBuilder.InsertData(
                table: "wedding_roles",
                columns: new[] { "WeddingRoleId", "Description", "Name" },
                values: new object[] { 7, null, "Flower Girl" });

            migrationBuilder.InsertData(
                table: "wedding_roles",
                columns: new[] { "WeddingRoleId", "Description", "Name" },
                values: new object[] { 6, null, "Groomsman" });

            migrationBuilder.InsertData(
                table: "wedding_roles",
                columns: new[] { "WeddingRoleId", "Description", "Name" },
                values: new object[] { 5, null, "Bridesmaid" });

            migrationBuilder.InsertData(
                table: "wedding_roles",
                columns: new[] { "WeddingRoleId", "Description", "Name" },
                values: new object[] { 4, null, "Best Man" });

            migrationBuilder.InsertData(
                table: "wedding_roles",
                columns: new[] { "WeddingRoleId", "Description", "Name" },
                values: new object[] { 3, null, "Maid of Honor" });

            migrationBuilder.InsertData(
                table: "wedding_roles",
                columns: new[] { "WeddingRoleId", "Description", "Name" },
                values: new object[] { 2, null, "Groom" });

            migrationBuilder.InsertData(
                table: "wedding_roles",
                columns: new[] { "WeddingRoleId", "Description", "Name" },
                values: new object[] { 9, null, "Officiant" });

            migrationBuilder.InsertData(
                table: "wedding_roles",
                columns: new[] { "WeddingRoleId", "Description", "Name" },
                values: new object[] { 19, null, "Usher" });

            migrationBuilder.CreateIndex(
                name: "IX_events_AddressId",
                table: "events",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_families_AddressId",
                table: "families",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_guests_FamilyId",
                table: "guests",
                column: "FamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_guests_FamilyId1",
                table: "guests",
                column: "FamilyId1");

            migrationBuilder.CreateIndex(
                name: "IX_guests_RsvpId",
                table: "guests",
                column: "RsvpId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_rsvps_EventId",
                table: "rsvps",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_vendors_AddressId",
                table: "vendors",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_wedding_member_roles_WeddingMemberId",
                table: "wedding_member_roles",
                column: "WeddingMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_wedding_member_roles_WeddingRoleId",
                table: "wedding_member_roles",
                column: "WeddingRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_wedding_roles_Name",
                table: "wedding_roles",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "images");

            migrationBuilder.DropTable(
                name: "registries");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "vendors");

            migrationBuilder.DropTable(
                name: "wedding_member_roles");

            migrationBuilder.DropTable(
                name: "wedding_roles");

            migrationBuilder.DropTable(
                name: "guests");

            migrationBuilder.DropTable(
                name: "families");

            migrationBuilder.DropTable(
                name: "rsvps");

            migrationBuilder.DropTable(
                name: "events");

            migrationBuilder.DropTable(
                name: "addresses");
        }
    }
}
