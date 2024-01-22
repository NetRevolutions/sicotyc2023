using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class UserForAuthenticationDto
    {
        [Required(ErrorMessage = "User name is required")] 
        public string UserName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password name is required")]
        public string Password { get; set; } = string.Empty;
    }
}
