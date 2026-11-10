using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DemoCICD.Contract.Services.Product.Command;

namespace DemoCICD.Contract.Services.Product.Validators
{
    public class GetProductByIdQueryValidator : AbstractValidator<Query.GetProductByIdQuery>
    {
        public GetProductByIdQueryValidator()
        {
            this.RuleFor(x => x.Id)
               .NotEmpty().WithMessage("Product ID is required.");
        }
    }
}
