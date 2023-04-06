using Microsoft.AspNetCore.Identity;

namespace Online_Marketplace.DAL.Entities.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
