using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class UserDetailForCreationDto
    {
        public Guid UserDetailId { get; set; } = Guid.NewGuid();

        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? DocumentType { get; set; }
        public string? DocumentNumber { get; set; }
        public string? Id { get; set; }
        public string? CreatedBy { get; set; }
    }
}
