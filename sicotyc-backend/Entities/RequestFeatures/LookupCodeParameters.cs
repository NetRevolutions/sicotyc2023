namespace Entities.RequestFeatures
{
    public class LookupCodeParameters : RequestParameters
    {
        public LookupCodeParameters()
        {
            OrderBy = "lookupCodeOrder ASC";
        }
        public string SearchTerm { get; set; }
    }
}
