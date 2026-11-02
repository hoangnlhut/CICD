namespace DemoCICD.Contract.Shared
{
    public class Result
    {
        protected internal Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None)
            {
                throw new InvalidOperationException();
            }

            if (!isSuccess && error == Error.None)
            {
                throw new InvalidOperationException();
            }

            this.IsSuccess = isSuccess;
            this.Error = error;
        }

        public bool IsSuccess { get; }

        public bool IsFailure => !this.IsSuccess;

        public Error Error { get; }

        public static Result Success() => new Result(true, Error.None);

        public static ResultT<TValue> Success<TValue>(TValue value) => new ResultT<TValue>(value, true, Error.None);

        public static Result Failure(Error error) => new Result(false, error);

        public static ResultT<TValue> Failure<TValue>(Error error) => new ResultT<TValue>(default(TValue), false, error);

        public static ResultT<TValue> Create<TValue>(TValue? value) => value is not null ? Success(value) : Failure<TValue>(Error.NullValue);
    }
}
