using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCICD.Contract.Shared
{
    public interface IValidationResult
    {
        public static readonly Error ValidationError = new Error("ValidationError", "A Validation problem occurred.");

        Error[] Errors { get; }
    }
}
