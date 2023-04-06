using Online_Marketplace.DAL.Entities.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Marketplace.DAL.Entities
{
    public class AdminProfile : BaseUserProfile
    {
        public string Address { get; set; }
        public string Role { get; set; } = "Admin";

        [ForeignKey(nameof(Admin))]
        public int AdminIdentity { get; set; }


        public Admin Admin { get; set; }
    }
}