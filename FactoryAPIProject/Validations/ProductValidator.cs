using FactoryAPIProject.Models;
using FluentValidation;

namespace FactoryAPIProject.Validations
{
    public class ProductValidator : AbstractValidator<ProductModel>
    {
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).NotNull().WithMessage("Product Name is not null").NotEmpty().WithMessage("Product Name is not empty").MinimumLength(3).WithMessage("Product Name must have at least three characters").MaximumLength(100).WithMessage("Product Name must be no more than 100 characters");
            RuleFor(p => p.Price).NotNull().WithMessage("Price is not null").NotEmpty().WithMessage("Price is not empty");
        }
    }
}
