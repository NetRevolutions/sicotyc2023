namespace Entities.DataTransferObjects
{
    public class LookupCodeGroupForCreationDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? LookupCodeGroupName { get; set; }
        public string? CreatedBy { get; set; }

        public IEnumerable<LookupCodeForCreationDto>? LookupCodes { get; set; }
    }
}
