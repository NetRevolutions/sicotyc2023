using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("LOOKUP_CODE", Schema = "SCT")]
    public class LookupCode : TrackingBase
    {
        [Column("LookupCodeId")]
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage ="El valor del LookupCode es requerido")]
        public string? LookupCodeValue { get; set; }

        [Required(ErrorMessage = "El nombre del LookupCode es requerido")]
        public string? LookupCodeName { get; set; }

        [Required(ErrorMessage = "El orden del LookupCode es requerido y es unico")]
        public int LookupCodeOrder { get; set; }

        [ForeignKey(nameof(LookupCodeGroup))] // Navigational Properties
        public Guid LookupCodeGroupId { get; set; }
        public LookupCodeGroup? LookupCodeGroup { get; set; }

    }
}
