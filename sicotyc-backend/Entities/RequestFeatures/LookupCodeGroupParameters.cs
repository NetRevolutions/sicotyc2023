namespace Entities.RequestFeatures
{
    public class LookupCodeGroupParameters: RequestParameters
    {
        public LookupCodeGroupParameters()
        {
            OrderBy = "lookupCodeGroupName";
        }

        public string SearchTerm { get; set; }
    }
}
