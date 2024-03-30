using Entities.Models;

namespace Entities.DataTransferObjects
{
    public class UserDto
    {
        public string Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Img { get; set; }
        public string? PhoneNumber { get; set; }
        public IList<string>? Roles { get; set; }
        public string? Ruc { get; set; }
        public UserDetail? UserDetail { get; set; }
    }
}
