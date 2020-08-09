using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using WeddingWebsiteCore.Contracts;
using WeddingWebsiteCore.Models;

namespace WeddingWebsiteCore.DataAccess
{
    public class WeddingContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Family> Families { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Registry> Registries { get; set; }
        public DbSet<Rsvp> Rsvps { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<WeddingMember> WeddingMembers { get; set; }
        public DbSet<WeddingRole> WeddingRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .HasMany(address => address.Events)
                .WithOne(@event => @event.Address);
            modelBuilder.Entity<Address>()
                .HasMany(address => address.Families)
                .WithOne(family => family.Address);
            modelBuilder.Entity<Address>()
                .HasMany(address => address.Vendors)
                .WithOne(vendor => vendor.Address);

            modelBuilder.Entity<Family>()
                .HasMany(family => family.Members)
                .WithOne(member => member.Family)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Guest>()
                .HasOne(guest => guest.Rsvp)
                .WithOne(rsvp => rsvp.Guest)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WeddingMemberRole>()
                .HasOne(weddingMemberRole => weddingMemberRole.WeddingRole)
                .WithMany(weddingRole => weddingRole.WeddingMemberRoles)
                .HasForeignKey(weddingRole => weddingRole.WeddingMemberId);
            modelBuilder.Entity<WeddingMemberRole>()
                .HasOne(weddingMemberRole => weddingMemberRole.WeddingMember)
                .WithMany(weddingMember => weddingMember.WeddingMemberRoles)
                .HasForeignKey(weddingMember => weddingMember.WeddingRoleId);

            modelBuilder.Entity<WeddingRole>()
                .HasIndex(weddingRole => weddingRole.Name)
                .IsUnique();
            modelBuilder.Entity<WeddingRole>()
                .HasData(new List<WeddingRole>
                {
                    new WeddingRole
                    {
                        WeddingRoleId = 1,
                        Name = "Bride"
                    },
                    new WeddingRole
                    {
                        WeddingRoleId = 2,
                        Name = "Groom"
                    },
                    new WeddingRole
                    {
                        WeddingRoleId = 3,
                        Name = "Maid of Honor"
                    },
                    new WeddingRole
                    {
                        WeddingRoleId = 4,
                        Name = "Best Man"
                    },
                    new WeddingRole
                    {
                        WeddingRoleId = 5,
                        Name = "Bridesmaid"
                    },
                    new WeddingRole
                    {
                        WeddingRoleId = 6,
                        Name = "Groomsman"
                    },
                    new WeddingRole
                    {
                        WeddingRoleId = 7,
                        Name = "Flower Girl"
                    },
                    new WeddingRole
                    {
                        WeddingRoleId = 8,
                        Name = "Ring Bearer"
                    },
                    new WeddingRole
                    {
                        WeddingRoleId = 9,
                        Name = "Officiant"
                    },
                    new WeddingRole
                    {
                        WeddingRoleId = 10,
                        Name = "Father of the Bride"
                    },
                    new WeddingRole
                    {
                        WeddingRoleId = 11,
                        Name = "Mother of the Bride"
                    },
                    new WeddingRole
                    {
                        WeddingRoleId = 12,
                        Name = "Father of the Groom"
                    },
                    new WeddingRole
                    {
                        WeddingRoleId = 13,
                        Name = "Mother of the Groom"
                    },
                    new WeddingRole
                    {
                        WeddingRoleId = 14,
                        Name = "Grandmother of the Bride"
                    },
                    new WeddingRole
                    {
                        WeddingRoleId = 15,
                        Name = "Grandfather of the Bride"
                    },
                    new WeddingRole
                    {
                        WeddingRoleId = 16,
                        Name = "Grandmother of the Groom"
                    },
                    new WeddingRole
                    {
                        WeddingRoleId = 17,
                        Name = "Grandfather of the Groom"
                    },
                    new WeddingRole
                    {
                        WeddingRoleId = 18,
                        Name = "Junior Bridesmaid"
                    },
                    new WeddingRole
                    {
                        WeddingRoleId = 19,
                        Name = "Usher"
                    }
                });
        }
    }
}
