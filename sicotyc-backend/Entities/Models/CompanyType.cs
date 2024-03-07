using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("COMPANY_TYPE", Schema = "SCT")]
    public class CompanyType : TrackingBase
    {
        [Key]
        public Guid CompanyTypeId { get; set; }
        [Required(ErrorMessage = "El tipo de empresa es requerido")]
        public string? CompanyTypeName { get; set; }
    }
}
