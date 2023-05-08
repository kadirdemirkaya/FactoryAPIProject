using FactoryAPIProject.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FactoryAPIProject.Validations
{
    public class LoginValidator : AbstractValidator<LoginModel>
    {
        public LoginValidator()
        {
            RuleFor(l => l.Username).NotNull().WithMessage("Username is not null").NotEmpty().WithMessage("Username is not empty").MinimumLength(3).WithMessage("Product Name must have at least three characters");

            RuleFor(l=>l.Password).NotNull().WithMessage("Password is not null").NotEmpty().WithMessage("Password is not empty").MinimumLength(3).WithMessage("Password must have at least three characters");
        }
    }
}
