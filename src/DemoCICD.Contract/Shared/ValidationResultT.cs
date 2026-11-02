using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCICD.Contract.Shared
{
    public sealed class ValidationResult<TValue> : Result<TValue>, IValidationResult
    {
        public Error[] Errors { get; }
        private ValidationResult(Error[] errors) : base(default, false, IValidationResult.ValidationError)
        {
            Errors = errors;
        }

        public static ValidationResult<TValue> WithErrors(Error[] errors) => new(errors);
    }
}
