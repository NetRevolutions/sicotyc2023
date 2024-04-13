using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class OptionByRole
    {
        [Key]
        public Guid OptionId { get; set; }
        
        public string? Title { get; set; }
        
        public string? Icon { get; set; }
        
        public string? Url { get; set; }
        public int OptionOrder { get; set; }
        public int OptionLevel { get; set; }
        public Guid? OptionParentId { get; set; }
        public bool IsEnabled { get; set; }
    }
}
