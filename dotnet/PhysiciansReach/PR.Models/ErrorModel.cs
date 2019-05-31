using System.Net;

namespace PR.Models
{
    public class ErrorModel
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorCode { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
    }
}
