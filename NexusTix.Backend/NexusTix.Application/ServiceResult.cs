using System.Net;

namespace NexusTix.Application
{
    public record ServiceResult
    {
        public List<string> ErrorMessages { get; init; } = [];
        public HttpStatusCode Status { get; init; } = HttpStatusCode.OK;

        public bool IsSuccess => ErrorMessages.Count == 0;
        public bool IsFail => !IsSuccess;

        public static ServiceResult Success(HttpStatusCode status = HttpStatusCode.OK)
            => new() { Status = status };

        public static ServiceResult Fail(string errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
            => new() { ErrorMessages = [errorMessage], Status = status };

        public static ServiceResult Fail(List<string> errorMessages, HttpStatusCode status = HttpStatusCode.BadRequest)
            => new() { ErrorMessages = errorMessages, Status = status };
    }

    public record ServiceResult<T> : ServiceResult
    {
        public T? Data { get; init; }
        public string? UrlAsCreated { get; init; }

        public static ServiceResult<T> Success(T data, HttpStatusCode status = HttpStatusCode.OK)
            => new() { Data = data, Status = status };

        public static ServiceResult<T> SuccessAsCreated(T data, string urlAsCreated)
            => new() { Data = data, Status = HttpStatusCode.Created, UrlAsCreated = urlAsCreated};

        public new static ServiceResult<T> Fail(string errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
            => new() { ErrorMessages = [errorMessage], Status = status };

        public new static ServiceResult<T> Fail(List<string> errorMessages, HttpStatusCode status = HttpStatusCode.BadRequest)
            => new() { ErrorMessages = errorMessages, Status = status };
    }
}
