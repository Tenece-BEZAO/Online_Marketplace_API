using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
    }
}
