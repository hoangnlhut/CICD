namespace DemoCICD.Contract.Shared
{
    public sealed class ValidationResultT<TResult> : ResultT<TResult>, IValidationResult
    {
        public Error[] Errors { get; }
        private ValidationResultT(Error[] errors) : base(default, false, IValidationResult.ValidationError)
        {
            Errors = errors;
        }

        public static ValidationResultT<TResult> WithErrors(Error[] errors) => new(errors);
    }
}