using System.Net;

namespace NexusTix.Domain.Exceptions
{
    public class BusinessException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public BusinessException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base(message)
        {
            StatusCode = statusCode;
        }

        public BusinessException(string message, Exception innerException, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}
