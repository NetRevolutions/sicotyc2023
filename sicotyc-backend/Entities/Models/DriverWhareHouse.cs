using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("DRIVER_WHAREHOUSE", Schema ="SCT")]
    public class DriverWhareHouse
    {
        [ForeignKey(nameof(Driver))]
        public Guid DriverId { get; set; }
        public Driver? Driver { get; set; }

        [ForeignKey(nameof(WhareHouse))]
        public Guid WhareHouseId { get; set; }
        public WhareHouse? WhareHouse { get; set; }
    }
}
