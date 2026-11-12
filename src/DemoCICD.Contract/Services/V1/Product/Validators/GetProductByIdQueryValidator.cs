using FluentValidation;

namespace DemoCICD.Contract.Services.V1.Product.Validators
{
    public class GetProductByIdQueryValidator : AbstractValidator<Query.GetProductByIdQuery>
    {
        public GetProductByIdQueryValidator()
        {
            this.RuleFor(x => x.id)
               .NotEmpty().WithMessage("Product ID is required.");
        }
    }
}
