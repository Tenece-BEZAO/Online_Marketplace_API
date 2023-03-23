﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Online_Marketplace.DAL.Entities.Models;

namespace Online_Marketplace.DAL.Entities
{
    public class MarketPlaceDBContext : IdentityDbContext<User>
    {
        public MarketPlaceDBContext(DbContextOptions<MarketPlaceDBContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasPrecision(18, 2);


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products { get; set; }

    }
}