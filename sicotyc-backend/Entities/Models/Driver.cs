using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("DRIVER", Schema ="SCT")]
    public class Driver: TrackingBase
    {
        [Key]
        public Guid DriverId { get; set; }
        public string? FirsName { get; set; }
        public string? LastName { get; set; }
        public string? Img { get; set; }
        public string? DocumentType { get; set; }
        public string? DocumentNumber { get; set; }
        public DateTime? DocumentExpiration {  get; set; }
        //public string? LicenseNumber { get; set; }
        //public string? LicenseType { get; set; }        
        //public DateTime? LicenseExpiration { get; set; }
        public bool EnableIMO { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public DateTime? AntecedentePolicialesExpiration { get; set; }
        public DateTime? AntecedentesPenalesExpiration { get; set; }
        //public List<WhareHouse>? WhareHousesEnabled { get; set; }
    }
}
