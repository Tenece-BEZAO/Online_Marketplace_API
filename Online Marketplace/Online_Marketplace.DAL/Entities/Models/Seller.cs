using System.ComponentModel.DataAnnotations;

namespace Online_Marketplace.DAL.Entities.Models
{
    public class Seller
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BusinessName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserId { get; set; }

        public User User { get; set; }
    }
}
