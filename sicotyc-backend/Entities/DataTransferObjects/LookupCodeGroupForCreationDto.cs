using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class LookupCodeGroupForCreationDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage ="LookupCodeGroupName es requerido")]
        public string? LookupCodeGroupName { get; set; }

        [Required(ErrorMessage = "Usuario de Creacion es requerido")]
        public string? CreatedBy { get; set; }

        public IEnumerable<LookupCodeForCreationDto>? LookupCodes { get; set; }
    }
}
