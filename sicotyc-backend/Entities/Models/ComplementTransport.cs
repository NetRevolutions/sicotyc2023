using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("COMPLEMENT_TRANSPORT", Schema ="SCT")]
    public class ComplementTransport: TrackingBase
    {
        [Key]
        public Guid ComplementTransportId { get; set; }
        [ForeignKey(nameof(TransportDetail))]
        public Guid TransportDetailId { get; set; }
        public TransportDetail? TransportDetail { get; set; }
        public string? Plate { get; set; }
        public string? Brand { get; set; }
        public DateTime? FabricationYear { get; set; }
        public DateTime? ModelYear { get; set; }
        public DateTime? TechnicalReviewExpiredDate { get; set; }
        public string? Size { get; set; }
        public string? AdditinalNotes { get; set; }
        public string? VehicleQualificationNumber { get; set; }
        public DateTime? VehicleQualificationExpirationDate { get; set; }
        public string? VehicleConfiguration { get; set; }
        public string? Img { get; set; }
    }
}
