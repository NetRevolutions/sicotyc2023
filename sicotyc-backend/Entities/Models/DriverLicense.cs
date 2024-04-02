using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("DRIVER_LICENSE", Schema ="SCT")]
    public class DriverLicense : TrackingBase
    {
        [ForeignKey(nameof(Driver))]
        public Guid DriverId { get; set; }
        public Driver? Driver { get; set; }

        public string? LicenseNumber { get; set; }
        public string? LicenseType { get; set; }
        public DateTime? LicenseExpiration { get; set; }
    }
}
