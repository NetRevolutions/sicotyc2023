namespace Entities.RequestFeatures
{
    public class UserParameters : RequestParameters
    {
        public UserParameters()
        {
            OrderBy = "FirstName";
        }

        public string SearchTerm { get; set; }
    }
}
