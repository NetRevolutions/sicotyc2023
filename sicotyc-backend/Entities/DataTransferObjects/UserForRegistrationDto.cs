using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class UserForRegistrationDto
    {
        [Required(ErrorMessage = "El Nombre es requerido")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "El apellido es requerido")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Nombre de usuario es requerido")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Contraseña es requerido")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "El correo electronico es requerido")]
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        //[Required(ErrorMessage = "El usuario debe de tener el menos un rol")]
        public IEnumerable<string>? Roles { get; set; }
    }
}
