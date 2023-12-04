using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class LookupCodeForCreationDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage ="LookupCodeGroupValue es requerido")]
        public string? LookupCodeValue { get; set; }
        [Required(ErrorMessage ="LookupCodeName es requerido")]
        public string? LookupCodeName { get; set; }
        //[Range(0, int.MaxValue, ErrorMessage = "LookupCodeOrder es requerido y no puede ser menor que 0")]
        public int? LookupCodeOrder { get; set; }
        [Required(ErrorMessage ="LookupCodeGroupId es requerido")]
        public Guid LookupCodeGroupId { get; set; }
        [Required(ErrorMessage ="Usuario de Creacion es requerido")]
        public string? CreatedBy { get; set; }
    }
}
