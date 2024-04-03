using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class ChangePassword
    {
        [Required(ErrorMessage = "El Id del Usuario es requerido")]
        public string Id { get; set; } = string.Empty;
        [Required(ErrorMessage = "El actual password es requerido")]
        public string OldPassword { get; set; } = string.Empty;
        [Required(ErrorMessage = "El nuevo password es requerido")]
        public string NewPassword { get; set; } = string.Empty;
    }
}
