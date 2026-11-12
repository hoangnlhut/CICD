using FluentValidation;
using static DemoCICD.Contract.Services.V1.Product.Command;

namespace DemoCICD.Contract.Services.V1.Product.Validators
{
    public class DeleteProductValidator : AbstractValidator<DeleteProduct>
    {
        public DeleteProductValidator()
        {
            this.RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Product ID is required.");
        }
    }
}