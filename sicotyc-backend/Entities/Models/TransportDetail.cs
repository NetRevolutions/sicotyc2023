using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("TRANSPORT_DETAIL", Schema ="SCT")]
    public class TransportDetail
    {
        [Key]
        public Guid TransportDetailId { get; set; }
        public string? Category { get; set; }
        public string? Model { get; set; }
        public string? Color { get; set; }
        public string? SerialNumber { get; set; }
        public string? EngineNumber { get; set; }
        public string? BodyWork { get; set; }
        public string? Large { get; set; }
        public string? Width { get; set; }
        public string? Height { get; set; }
        public string? Axles { get; set; }
    }
}
