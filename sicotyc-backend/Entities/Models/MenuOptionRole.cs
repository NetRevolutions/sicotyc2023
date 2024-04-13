using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("OPTIONS_ROLE", Schema = "SCT")]
    public class MenuOptionRole
    {
        [Required]
        public string? RoleId { get; set; }
        [Required]
        public Guid OptionId { get; set; }
    }
}
