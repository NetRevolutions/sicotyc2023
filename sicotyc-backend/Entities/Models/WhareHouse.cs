using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("WHAREHOUSE", Schema ="SCT")]
    public class WhareHouse : TrackingBase
    {
        [Key]
        public Guid WhareHouseId { get; set; }
        [ForeignKey(nameof(Company))]
        public Guid CompanyId { get; set; }
        public Company? Company { get; set; }

        public string? AliasName { get; set; }
        public string? AddressDetails { get; set; }
        public string? AditionalDetails { get; set; }
    }
}
