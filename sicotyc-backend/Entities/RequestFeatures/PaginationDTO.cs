namespace Entities.RequestFeatures
{
    public class PaginationDTO
    {
        public int Page { get; set; } = 1;
        public int recordsPerPage = 10;
        private readonly int countMaxRecordsPerPage = 50;

        public int RecordsPerPage
        { 
            get
            {
                return recordsPerPage;
            }
            set
            {
                recordsPerPage = (value > countMaxRecordsPerPage) ? countMaxRecordsPerPage : value;
            }
        }
    }
}
