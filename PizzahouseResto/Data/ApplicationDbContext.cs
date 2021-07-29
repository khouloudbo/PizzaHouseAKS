using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PizzahouseResto.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzahouseResto.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Meal> Meals { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderMeal> OrderMeal { get; set; }




        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().ToTable("Users", "security");
            builder.Entity<IdentityRole>().ToTable("Roles", "security");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "security");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "security");

            builder.Entity<Meal>().HasMany(x => x.Orders)
               .WithMany(x => x.Meals)
               .UsingEntity<OrderMeal>(
                   x => x.HasOne(x => x.Order)
                   .WithMany().HasForeignKey(x => x.OrderId),
                   x => x.HasOne(x => x.Meal)
                  .WithMany().HasForeignKey(x => x.MealId));
        }
    }
}
