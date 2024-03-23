namespace Entities.DataTransferObjects
{
    /**
     * Nota: No recomiendo usar record porque dificulta devolverlo en otro formato diferente de JSON 
     * tal como XML
     */

    //public record LookupCodeGroupDto(Guid Id, string Name);
    public class LookupCodeGroupDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }
}
