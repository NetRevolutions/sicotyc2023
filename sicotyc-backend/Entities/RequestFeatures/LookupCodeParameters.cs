namespace Entities.RequestFeatures
{
    public class LookupCodeParameters : RequestParameters
    {
        public LookupCodeParameters()
        {
            OrderBy = "lookupCodeName";
        }
        public string SearchTerm { get; set; }
    }
}
