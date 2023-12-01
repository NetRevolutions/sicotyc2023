using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class LookupCodeForCreationDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? LookupCodeValue { get; set; }
        public string? LookupCodeName { get; set; }
        public int? LookupCodeOrder { get; set; }
        public Guid LookupCodeGroupId { get; set; }
        public string? CreatedBy { get; set; }
    }
}
