namespace Online_Marketplace.DAL.Entities.Models
{
    public class BaseUserProfile
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }

    }
}