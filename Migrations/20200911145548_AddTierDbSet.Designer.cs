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
    [Migration("20200911145548_AddTierDbSet")]
    partial class AddTierDbSet
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("WeddingWebsiteCore.Models.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("StreetDetail")
                        .HasColumnType("text");

                    b.Property<string>("StreetName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("StreetNumber")
                        .IsRequired()
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

            modelBuilder.Entity("WeddingWebsiteCore.Models.Event", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("AddressId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("EventId");

                    b.ToTable("events");
                });

            modelBuilder.Entity("WeddingWebsiteCore.Models.Family", b =>
                {
                    b.Property<int>("FamilyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AdditionalGuests")
                        .HasColumnType("integer");

                    b.Property<int?>("AddressId")
                        .HasColumnType("integer");

                    b.Property<int?>("HeadMemberId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int?>("TierId")
                        .HasColumnType("integer");

                    b.HasKey("FamilyId");

                    b.HasIndex("TierId");

                    b.ToTable("families");
                });

            modelBuilder.Entity("WeddingWebsiteCore.Models.Guest", b =>
                {
                    b.Property<int>("GuestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<int?>("FamilyId")
                        .HasColumnType("integer");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsChild")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsWeddingMember")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.Property<int?>("RsvpId")
                        .HasColumnType("integer");

                    b.HasKey("GuestId");

                    b.HasIndex("FamilyId");

                    b.HasIndex("RsvpId")
                        .IsUnique();

                    b.ToTable("guests");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Guest");
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
                        .HasColumnType("integer");

                    b.Property<string>("ContactEmail")
                        .HasColumnType("text");

                    b.Property<string>("ContactPhone")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.HasKey("VendorId");

                    b.ToTable("vendors");
                });

            modelBuilder.Entity("WeddingWebsiteCore.Models.WeddingMemberRole", b =>
                {
                    b.Property<int>("WeddingMemberRoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("WeddingMemberId")
                        .HasColumnType("integer");

                    b.Property<int>("WeddingRoleId")
                        .HasColumnType("integer");

                    b.HasKey("WeddingMemberRoleId");

                    b.HasIndex("WeddingMemberId");

                    b.HasIndex("WeddingRoleId");

                    b.ToTable("wedding_member_roles");
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
                            WeddingRoleId = 1,
                            Name = "Bride"
                        },
                        new
                        {
                            WeddingRoleId = 2,
                            Name = "Groom"
                        },
                        new
                        {
                            WeddingRoleId = 3,
                            Name = "Maid of Honor"
                        },
                        new
                        {
                            WeddingRoleId = 4,
                            Name = "Best Man"
                        },
                        new
                        {
                            WeddingRoleId = 5,
                            Name = "Bridesmaid"
                        },
                        new
                        {
                            WeddingRoleId = 6,
                            Name = "Groomsman"
                        },
                        new
                        {
                            WeddingRoleId = 7,
                            Name = "Flower Girl"
                        },
                        new
                        {
                            WeddingRoleId = 8,
                            Name = "Ring Bearer"
                        },
                        new
                        {
                            WeddingRoleId = 9,
                            Name = "Officiant"
                        },
                        new
                        {
                            WeddingRoleId = 10,
                            Name = "Father of the Bride"
                        },
                        new
                        {
                            WeddingRoleId = 11,
                            Name = "Mother of the Bride"
                        },
                        new
                        {
                            WeddingRoleId = 12,
                            Name = "Father of the Groom"
                        },
                        new
                        {
                            WeddingRoleId = 13,
                            Name = "Mother of the Groom"
                        },
                        new
                        {
                            WeddingRoleId = 14,
                            Name = "Grandmother of the Bride"
                        },
                        new
                        {
                            WeddingRoleId = 15,
                            Name = "Grandfather of the Bride"
                        },
                        new
                        {
                            WeddingRoleId = 16,
                            Name = "Grandmother of the Groom"
                        },
                        new
                        {
                            WeddingRoleId = 17,
                            Name = "Grandfather of the Groom"
                        },
                        new
                        {
                            WeddingRoleId = 18,
                            Name = "Junior Bridesmaid"
                        },
                        new
                        {
                            WeddingRoleId = 19,
                            Name = "Usher"
                        });
                });

            modelBuilder.Entity("WeddingWebsiteCore.Models.WeddingMember", b =>
                {
                    b.HasBaseType("WeddingWebsiteCore.Models.Guest");

                    b.ToTable("wedding_members");

                    b.HasDiscriminator().HasValue("WeddingMember");
                });

            modelBuilder.Entity("WeddingWebsiteCore.Models.Family", b =>
                {
                    b.HasOne("WeddingWebsiteCore.Models.Tier", "Tier")
                        .WithMany("Families")
                        .HasForeignKey("TierId");
                });

            modelBuilder.Entity("WeddingWebsiteCore.Models.Guest", b =>
                {
                    b.HasOne("WeddingWebsiteCore.Models.Family", "Family")
                        .WithMany("Members")
                        .HasForeignKey("FamilyId");

                    b.HasOne("WeddingWebsiteCore.Models.Rsvp", "Rsvp")
                        .WithOne("Guest")
                        .HasForeignKey("WeddingWebsiteCore.Models.Guest", "RsvpId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WeddingWebsiteCore.Models.Rsvp", b =>
                {
                    b.HasOne("WeddingWebsiteCore.Models.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WeddingWebsiteCore.Models.WeddingMemberRole", b =>
                {
                    b.HasOne("WeddingWebsiteCore.Models.WeddingRole", "WeddingRole")
                        .WithMany("WeddingMemberRoles")
                        .HasForeignKey("WeddingMemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WeddingWebsiteCore.Models.WeddingMember", "WeddingMember")
                        .WithMany("WeddingMemberRoles")
                        .HasForeignKey("WeddingRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
