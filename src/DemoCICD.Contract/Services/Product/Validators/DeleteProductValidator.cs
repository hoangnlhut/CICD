using FluentValidation;
using static DemoCICD.Contract.Services.Product.Command;

namespace DemoCICD.Contract.Services.Product.Validators
{
    public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductValidator()
        {
            this.RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Product ID is required.");
        }
    }
}