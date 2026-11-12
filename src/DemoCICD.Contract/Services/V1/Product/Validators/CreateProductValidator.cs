using FluentValidation;
using static DemoCICD.Contract.Services.V1.Product.Command;

namespace DemoCICD.Contract.Services.V1.Product.Validators
{
    public class CreateProductValidator : AbstractValidator<CreateProduct>
    {
        public CreateProductValidator()
        {
            this.RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.");

            this.RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Product price is required.")
                .GreaterThan(0).WithMessage("Product price must be a positive value.");

            this.RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Product description is required.")
                .MaximumLength(250).WithMessage("Product description must not exceed 250 characters.");
        }
    }
}