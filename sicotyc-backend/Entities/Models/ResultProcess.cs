using System.Net;

namespace Entities.Models
{
    public class ResultProcess
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public HttpStatusCode Status { get; set; }
    }
}
