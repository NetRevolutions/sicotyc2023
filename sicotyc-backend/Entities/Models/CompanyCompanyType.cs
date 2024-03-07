using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("COMPANY_COMPANY_TYPE", Schema = "SCT")]
    public class CompanyCompanyType
    {        
        [ForeignKey(nameof(Company))]
        public Guid CompanyId { get; set; }
        public Company? Company { get; set; }
        [ForeignKey(nameof(CompanyType))]
        public Guid CompanyTypeId { get; set; }
        public CompanyType? CompanyType { get; set; }
    }
}
