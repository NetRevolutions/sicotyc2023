namespace Entities.Models
{
    public class MenuItem
    {
        public string? Title { get; set; }
        public string? Icon { get; set; } = "mdi mdi-gauge";
        public List<SubMenuItem>? submenu { get; set; }
    }
}
