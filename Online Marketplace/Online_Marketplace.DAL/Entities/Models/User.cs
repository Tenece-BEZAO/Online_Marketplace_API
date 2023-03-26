using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Online_Marketplace.DAL.Entities.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsSeller { get; set; }
        
    }
}
