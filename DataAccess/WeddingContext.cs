using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        public DbSet<WeddingRole> WeddingRoles { get; set; }
        public DbSet<Tier> Tiers { get; set; }
        public DbSet<Accommodation> Accommodations { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<GuestWeddingRole> GuestWeddingRoles { get; set; }

        public WeddingContext(DbContextOptions options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accommodation>()
                .HasOne(x => x.Category)
                .WithMany()
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Accommodation>()
                .HasOne(x => x.Location)
                .WithMany()
                .HasForeignKey(x => x.AddressId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Category>()
                .HasOne(x => x.Parent)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);
                
            modelBuilder.Entity<Family>()
                .HasMany(family => family.Members)
                .WithOne(member => member.Family)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Family>()
                .HasOne(family => family.Tier)
                .WithMany(tier => tier.Families);
            modelBuilder.Entity<Family>()
                .HasOne(family => family.Address)
                .WithMany(address => address.Families)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Event>()
                .HasOne(@event => @event.Address)
                .WithMany(address => address.Events)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Vendor>()
                .HasOne(vendor => vendor.Address)
                .WithMany(address => address.Vendors)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Guest>()
                .HasMany(guest => guest.RSVPs)
                .WithOne(rsvp => rsvp.Guest)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Guest>()
                .HasOne(guest => guest.Parent)
                .WithMany(parent => parent.Children)
                .OnDelete(DeleteBehavior.SetNull);
            
            modelBuilder.Entity<GuestWeddingRole>()
                .HasOne(x => x.Guest)
                .WithMany(y => y.GuestWeddingRoles)
                .HasForeignKey(x => x.GuestId);
            modelBuilder.Entity<GuestWeddingRole>()
                .HasOne(x => x.WeddingRole)
                .WithMany(y => y.GuestWeddingRoles)
                .HasForeignKey(x => x.WeddingRoleId);

            modelBuilder.Entity<WeddingRole>()
                .HasIndex(weddingRole => weddingRole.Name)
                .IsUnique();
            modelBuilder.Entity<WeddingRole>()
                .HasData(new List<WeddingRole>
                {
                    new WeddingRole
                    {
                        WeddingRoleId = -1,
                        Name = "Bride"
                    },
                    new WeddingRole
                    {
                        WeddingRoleId = -2,
                        Name = "Groom"
                    },
                    new WeddingRole
                    {
                        WeddingRoleId = -3,
                        Name = "Maid of Honor"
                    },
                    new WeddingRole
                    {
                        WeddingRoleId = -4,
                        Name = "Best Man"
                    },
                    new WeddingRole
                    {
                        WeddingRoleId = -5,
                        Name = "Bridesmaid"
                    },
                    new WeddingRole
                    {
                        WeddingRoleId = -6,
                        Name = "Groomsman"
                    },
                    new WeddingRole
                    {
                        WeddingRoleId = -7,
                        Name = "Flower Girl"
                    },
                    new WeddingRole
                    {
                        WeddingRoleId = -8,
                        Name = "Ring Bearer"
                    },
                    new WeddingRole
                    {
                        WeddingRoleId = -9,
                        Name = "Officiant"
                    },
                    new WeddingRole
                    {
                        WeddingRoleId = -10,
                        Name = "Father of the Bride"
                    },
                    new WeddingRole
                    {
                        WeddingRoleId = -11,
                        Name = "Mother of the Bride"
                    },
                    new WeddingRole
                    {
                        WeddingRoleId = -12,
                        Name = "Father of the Groom"
                    },
                    new WeddingRole
                    {
                        WeddingRoleId = -13,
                        Name = "Mother of the Groom"
                    },
                    new WeddingRole
                    {
                        WeddingRoleId = -14,
                        Name = "Grandmother of the Bride"
                    },
                    new WeddingRole
                    {
                        WeddingRoleId = -15,
                        Name = "Grandfather of the Bride"
                    },
                    new WeddingRole
                    {
                        WeddingRoleId = -16,
                        Name = "Grandmother of the Groom"
                    },
                    new WeddingRole
                    {
                        WeddingRoleId = -17,
                        Name = "Grandfather of the Groom"
                    },
                    new WeddingRole
                    {
                        WeddingRoleId = -18,
                        Name = "Junior Bridesmaid"
                    },
                    new WeddingRole
                    {
                        WeddingRoleId = -19,
                        Name = "Usher"
                    }
                });
        }
    }
}
