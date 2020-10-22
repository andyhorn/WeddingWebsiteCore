﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WeddingWebsiteCore.DataAccess;

namespace WeddingWebsiteCore.Migrations
{
    [DbContext(typeof(WeddingContext))]
    [Migration("20201022154345_ReformingGuestRoleRelationship")]
    partial class ReformingGuestRoleRelationship
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("WeddingWebsiteCore.Models.Accommodation", b =>
                {
                    b.Property<int>("AccommodationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("AddressId")
                        .HasColumnType("integer");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("AccommodationId");

                    b.HasIndex("AddressId");

                    b.HasIndex("CategoryId");

                    b.ToTable("accommodations");
                });

            modelBuilder.Entity("WeddingWebsiteCore.Models.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnName("city")
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnName("country")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnName("postalCode")
                        .HasColumnType("text");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnName("state")
                        .HasColumnType("text");

                    b.Property<string>("StreetDetail")
                        .HasColumnName("streetDetail")
                        .HasColumnType("text");

                    b.Property<string>("StreetName")
                        .IsRequired()
                        .HasColumnName("streetName")
                        .HasColumnType("text");

                    b.Property<string>("StreetNumber")
                        .IsRequired()
                        .HasColumnName("streetNumber")
                        .HasColumnType("text");

                    b.HasKey("AddressId");

                    b.ToTable("addresses");
                });

            modelBuilder.Entity("WeddingWebsiteCore.Models.ApplicationUser", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("users");
                });

            modelBuilder.Entity("WeddingWebsiteCore.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("ParentId")
                        .HasColumnType("integer");

                    b.HasKey("CategoryId");

                    b.HasIndex("ParentId");

                    b.ToTable("categories");
                });

            modelBuilder.Entity("WeddingWebsiteCore.Models.Event", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("eventId")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("AddressId")
                        .HasColumnName("FK_addressId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("EndTime")
                        .HasColumnName("endTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("StartTime")
                        .HasColumnName("startTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("EventId");

                    b.HasIndex("AddressId");

                    b.ToTable("events");
                });

            modelBuilder.Entity("WeddingWebsiteCore.Models.Family", b =>
                {
                    b.Property<int>("FamilyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AdditionalGuests")
                        .HasColumnName("additionalGuests")
                        .HasColumnType("integer");

                    b.Property<int?>("AddressId")
                        .HasColumnName("FK_addressId")
                        .HasColumnType("integer");

                    b.Property<int?>("HeadMemberId")
                        .HasColumnName("FK_headMemberId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.Property<int?>("TierId")
                        .HasColumnName("FK_tierId")
                        .HasColumnType("integer");

                    b.HasKey("FamilyId");

                    b.HasIndex("AddressId");

                    b.HasIndex("TierId");

                    b.ToTable("families");
                });

            modelBuilder.Entity("WeddingWebsiteCore.Models.Guest", b =>
                {
                    b.Property<int>("GuestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<int?>("FamilyId")
                        .HasColumnType("integer");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("InviteCode")
                        .HasColumnType("text");

                    b.Property<bool>("IsChild")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsUnderTen")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsWeddingMember")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("ParentId")
                        .HasColumnType("integer");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.Property<int?>("WeddingRoleId")
                        .HasColumnType("integer");

                    b.HasKey("GuestId");

                    b.HasIndex("FamilyId");

                    b.HasIndex("ParentId");

                    b.HasIndex("WeddingRoleId");

                    b.ToTable("guests");
                });

            modelBuilder.Entity("WeddingWebsiteCore.Models.Image", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.HasKey("ImageId");

                    b.ToTable("images");
                });

            modelBuilder.Entity("WeddingWebsiteCore.Models.Registry", b =>
                {
                    b.Property<int>("RegistryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.HasKey("RegistryId");

                    b.ToTable("registries");
                });

            modelBuilder.Entity("WeddingWebsiteCore.Models.Rsvp", b =>
                {
                    b.Property<int>("RsvpId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("EventId")
                        .HasColumnType("integer");

                    b.Property<int>("GuestId")
                        .HasColumnType("integer");

                    b.Property<bool>("HasResponded")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsAttending")
                        .HasColumnType("boolean");

                    b.HasKey("RsvpId");

                    b.HasIndex("EventId");

                    b.HasIndex("GuestId");

                    b.ToTable("rsvps");
                });

            modelBuilder.Entity("WeddingWebsiteCore.Models.Tier", b =>
                {
                    b.Property<int>("TierId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("TierId");

                    b.ToTable("tiers");
                });

            modelBuilder.Entity("WeddingWebsiteCore.Models.Vendor", b =>
                {
                    b.Property<int>("VendorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("AddressId")
                        .HasColumnName("FK_addressId")
                        .HasColumnType("integer");

                    b.Property<string>("ContactEmail")
                        .HasColumnName("contactEmail")
                        .HasColumnType("text");

                    b.Property<string>("ContactPhone")
                        .HasColumnName("contactPhone")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .HasColumnName("url")
                        .HasColumnType("text");

                    b.HasKey("VendorId");

                    b.HasIndex("AddressId");

                    b.ToTable("vendors");
                });

            modelBuilder.Entity("WeddingWebsiteCore.Models.WeddingRole", b =>
                {
                    b.Property<int>("WeddingRoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("WeddingRoleId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("wedding_roles");

                    b.HasData(
                        new
                        {
                            WeddingRoleId = -1,
                            Name = "Bride"
                        },
                        new
                        {
                            WeddingRoleId = -2,
                            Name = "Groom"
                        },
                        new
                        {
                            WeddingRoleId = -3,
                            Name = "Maid of Honor"
                        },
                        new
                        {
                            WeddingRoleId = -4,
                            Name = "Best Man"
                        },
                        new
                        {
                            WeddingRoleId = -5,
                            Name = "Bridesmaid"
                        },
                        new
                        {
                            WeddingRoleId = -6,
                            Name = "Groomsman"
                        },
                        new
                        {
                            WeddingRoleId = -7,
                            Name = "Flower Girl"
                        },
                        new
                        {
                            WeddingRoleId = -8,
                            Name = "Ring Bearer"
                        },
                        new
                        {
                            WeddingRoleId = -9,
                            Name = "Officiant"
                        },
                        new
                        {
                            WeddingRoleId = -10,
                            Name = "Father of the Bride"
                        },
                        new
                        {
                            WeddingRoleId = -11,
                            Name = "Mother of the Bride"
                        },
                        new
                        {
                            WeddingRoleId = -12,
                            Name = "Father of the Groom"
                        },
                        new
                        {
                            WeddingRoleId = -13,
                            Name = "Mother of the Groom"
                        },
                        new
                        {
                            WeddingRoleId = -14,
                            Name = "Grandmother of the Bride"
                        },
                        new
                        {
                            WeddingRoleId = -15,
                            Name = "Grandfather of the Bride"
                        },
                        new
                        {
                            WeddingRoleId = -16,
                            Name = "Grandmother of the Groom"
                        },
                        new
                        {
                            WeddingRoleId = -17,
                            Name = "Grandfather of the Groom"
                        },
                        new
                        {
                            WeddingRoleId = -18,
                            Name = "Junior Bridesmaid"
                        },
                        new
                        {
                            WeddingRoleId = -19,
                            Name = "Usher"
                        });
                });

            modelBuilder.Entity("WeddingWebsiteCore.Models.Accommodation", b =>
                {
                    b.HasOne("WeddingWebsiteCore.Models.Address", "Location")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("WeddingWebsiteCore.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("WeddingWebsiteCore.Models.Category", b =>
                {
                    b.HasOne("WeddingWebsiteCore.Models.Category", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WeddingWebsiteCore.Models.Event", b =>
                {
                    b.HasOne("WeddingWebsiteCore.Models.Address", "Address")
                        .WithMany("Events")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("WeddingWebsiteCore.Models.Family", b =>
                {
                    b.HasOne("WeddingWebsiteCore.Models.Address", "Address")
                        .WithMany("Families")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("WeddingWebsiteCore.Models.Tier", "Tier")
                        .WithMany("Families")
                        .HasForeignKey("TierId");
                });

            modelBuilder.Entity("WeddingWebsiteCore.Models.Guest", b =>
                {
                    b.HasOne("WeddingWebsiteCore.Models.Family", "Family")
                        .WithMany("Members")
                        .HasForeignKey("FamilyId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("WeddingWebsiteCore.Models.Guest", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("WeddingWebsiteCore.Models.WeddingRole", null)
                        .WithMany("Guests")
                        .HasForeignKey("WeddingRoleId");
                });

            modelBuilder.Entity("WeddingWebsiteCore.Models.Rsvp", b =>
                {
                    b.HasOne("WeddingWebsiteCore.Models.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WeddingWebsiteCore.Models.Guest", "Guest")
                        .WithMany("RSVPs")
                        .HasForeignKey("GuestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WeddingWebsiteCore.Models.Vendor", b =>
                {
                    b.HasOne("WeddingWebsiteCore.Models.Address", "Address")
                        .WithMany("Vendors")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.SetNull);
                });
#pragma warning restore 612, 618
        }
    }
}
