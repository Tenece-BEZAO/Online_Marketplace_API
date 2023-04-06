using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(p => p.Price)
                      .HasPrecision(18, 2);

            modelBuilder.Entity<CartItem>()
            .HasOne(ci => ci.Product)
            .WithMany()
            .HasForeignKey(ci => ci.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
            });


            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.HasOne(ci => ci.Product)
                      .WithMany()
                      .HasForeignKey(ci => ci.ProductId)
                      .OnDelete(DeleteBehavior.Restrict);
            });


            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.Property(p => p.Price)
                      .HasPrecision(18, 2);

                entity.HasOne(ci => ci.Product)
                      .WithMany()
                      .HasForeignKey(ci => ci.ProductId)
                      .OnDelete(DeleteBehavior.Restrict);
            });


            modelBuilder.Entity<ProductReviews>(entity =>
            {
                entity.HasOne(ci => ci.Product)
                .WithMany()
                .HasForeignKey(ci => ci.ProductIdentity)
                .OnDelete(DeleteBehavior.NoAction);
            });



            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(p => p.TotalAmount)
                      .HasPrecision(18, 2);
            });



            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RoleConfiguration());


        }


        public DbSet<Admin> Admins { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<AdminProfile> AdminProfiles { get; set; }
        public DbSet<SellerProfile> SellerProfiles { get; set; }
        public DbSet<BuyerProfile> BuyerProfiles { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<ProductReviews> ProductReviews { get; set; }

        public DbSet<Shipping> Shipping { get; set; }
        public DbSet<Wallet > Wallets { get; set; }

    }
}
