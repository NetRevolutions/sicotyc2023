using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("LOOKUP_CODE_GROUP", Schema ="SCT")]    
    public class LookupCodeGroup : TrackingBase
    {
        [Column("LookupCodeGroupId")]
        [Key]
        public Guid Id { get; set; }

        [Column("LookupCodeGroupName")]
        [Required(ErrorMessage ="Nombre de LookupCodeGroup es requerido")]
        [MaxLength(60, ErrorMessage =" La longitud maxima del nombre del LookupCodeGroup es de 60 caracteres")]
        public string? Name { get; set; }

        public ICollection<LookupCode>? LookupCodes { get; set; } // Navigational properties
    }
}
