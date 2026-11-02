using DemoCICD.Contract.Shared;
using MediatR;
using FluentValidation;

namespace DemoCICD.Application.Behaviors
{
    public class ValidationPipeLineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Result
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationPipeLineBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if(!_validators.Any())
            {
                return await next();
            }

            Error[] errors = _validators
                .Select(v => v.Validate(request))
                .SelectMany(validationResult => validationResult.Errors)
                .Where(validationFailure => validationFailure != null)
                .Select(failure => new Error(failure.PropertyName, failure.ErrorMessage))
                .Distinct()
                .ToArray();

            if(errors.Length > 0)
            {
                return CreateValidationResult<TResponse>(errors);
            }

            return await next();
        }

        private static TResult CreateValidationResult<TResult>(Error[] errors)
        {
            if(typeof(TResult) == typeof(Result))
            {
                return (TResult)(object)ValidationResultT<Result>.WithErrors(errors);
            }

            object validationResult = typeof(ValidationResultT<>)
                .GetGenericTypeDefinition()
                .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
                .GetMethod(nameof(ValidationResultT<object>.WithErrors))!
                .Invoke(null, new object?[] { errors })!;

            return (TResult)validationResult;
        }
    }
}
