namespace Entities.Models
{
    public class RenewToken
    {
        public string? Token { get; set; }
        public User? User { get; set; }

        public List<string>? Roles { get; set; }
    }
}
