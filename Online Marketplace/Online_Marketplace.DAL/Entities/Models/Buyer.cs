using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Marketplace.DAL.Entities.Models
{
    public class Buyer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public User User { get; set; }

    }
}
