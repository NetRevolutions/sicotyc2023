namespace Entities.DataTransferObjects
{
    public class LookupCodeGroupForUpdateDto
    {
        public string? LookupCodeGroupName { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; } = DateTime.UtcNow;

        public IEnumerable<LookupCodeForCreationDto>? LookupCodes { get; set; }
    }
}
