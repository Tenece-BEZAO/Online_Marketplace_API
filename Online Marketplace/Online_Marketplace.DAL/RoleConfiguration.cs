using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Online_Marketplace.DAL.Entities.Models;

namespace Online_Marketplace.DAL
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "Seller",
                    NormalizedName = "SELLER"
                },
                new IdentityRole
                {
                    Name = "Buyer",
                    NormalizedName = "BUYER"
                }
            );
        }

        public void Seed(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                };

                var result = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Seller").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Seller",
                    NormalizedName = "SELLER"
                };

                var result = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Buyer").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Buyer",
                    NormalizedName = "BUYER"
                };

                var result = roleManager.CreateAsync(role).Result;
            }
        }



    }
}
