using Microsoft.EntityFrameworkCore;
using EZStay.Api.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using EZStay.Api.Utils.Core;
using EZStay.API.Utils.Helper;

namespace EZStay.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .Property(u => u.Roles)
                .HasConversion(new RolesConverter());

            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.Parse("B0A1E2F3-4D56-7890-1234-56789ABCDEF0"),
                    FullName = "Administrator",
                    Username = "admin",
                    Email = "admin@gmail.com",
                    PasswordHash = "$2a$12$FB8ZwL2s9RcwDAwpthyWve/.4Q.Dc88SwsbbCQ9doaQjI4xpWk0YG",
                    Roles = new List<string> { Roles.Admin, Roles.Manager, Roles.AccountManager, Roles.ContentManager, Roles.Owner, Roles.User }
                },
                new User
                {
                    Id = Guid.Parse("D0A2E3F4-5D67-8901-2345-67890ABCDE01"),
                    FullName = "Manager User",
                    Username = "manager",
                    Email = "manager@gmail.com",
                    PasswordHash = "$2a$12$FB8ZwL2s9RcwDAwpthyWve/.4Q.Dc88SwsbbCQ9doaQjI4xpWk0YG",
                    Roles = new List<string> { Roles.Manager, Roles.User }
                },
                new User
                {
                    Id = Guid.Parse("E1A3E4F5-6D78-9012-3456-78901BCDEF23"),
                    FullName = "Content Manager User",
                    Username = "contentmanager",
                    Email = "contentmanager@gmail.com",
                    PasswordHash = "$2a$12$FB8ZwL2s9RcwDAwpthyWve/.4Q.Dc88SwsbbCQ9doaQjI4xpWk0YG",
                    Roles = new List<string> { Roles.ContentManager, Roles.User }
                },
                new User
                {
                    Id = Guid.Parse("F2A4E5F6-7D89-0123-4567-89012CDE2345"),
                    FullName = "Account Manager User",
                    Username = "accountmanager",
                    Email = "accountmanager@gmail.com",
                    PasswordHash = "$2a$12$FB8ZwL2s9RcwDAwpthyWve/.4Q.Dc88SwsbbCQ9doaQjI4xpWk0YG",
                    Roles = new List<string> { Roles.AccountManager, Roles.User }
                }
            );

            // Unique constraints
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            // One Property has many Images (cascade on delete)
            modelBuilder.Entity<Image>()
                .HasOne(i => i.Property)
                .WithMany(p => p.Images)
                .HasForeignKey(i => i.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);

            // Precision for price field
            modelBuilder.Entity<Property>()
                .Property(p => p.PricePerNight)
                .HasPrecision(18, 2);
        }
    }
}
