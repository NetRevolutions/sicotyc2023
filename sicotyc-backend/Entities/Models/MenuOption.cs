using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("OPTIONS", Schema ="SCT")]
    public class MenuOption : TrackingBase
    {
        [Key]
        public Guid OptionId { get; set; }
        [MaxLength(100)]
        public string? Title { get; set; }
        [MaxLength(100)]
        public string? Icon { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string? Url { get; set; }
        public int OptionOrder { get; set; }
        public int OptionLevel { get; set; }
        public Guid? OptionParentId { get; set; }
    }
}
