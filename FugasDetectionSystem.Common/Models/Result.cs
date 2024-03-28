namespace FugasDetectionSystem.Common.Models
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T Value { get; }
        public string ErrorMessage { get; }
        public Exception Exception { get; }

        protected Result(T value, bool isSuccess, string errorMessage, Exception exception)
        {
            Value = value;
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
            Exception = exception;
        }

        public static Result<T> Success(T value) => new(value, true, null, null);
        public static Result<T> Failure(string errorMessage, Exception exception = null) => new(default, false, errorMessage, exception);
    }
}
