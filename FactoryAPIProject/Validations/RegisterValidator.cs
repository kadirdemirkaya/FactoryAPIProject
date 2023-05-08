using FactoryAPIProject.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FactoryAPIProject.Validations
{
    public class RegisterValidator : AbstractValidator<RegisterModel>
    {
        public RegisterValidator()
        {
            RuleFor(r => r.Username).NotEmpty().WithMessage("Username is not null").NotEmpty().WithMessage("Username is not empty").MinimumLength(3).WithMessage("Username must have at least three characters").MaximumLength(100).WithMessage("Username must be no more than 100 characters");

            RuleFor(r => r.Password).NotEmpty().WithMessage("Password is not null").NotEmpty().WithMessage("Password is not empty").MinimumLength(3).WithMessage("Password must have at least three characters");

            RuleFor(r=>r.Email).NotEmpty().WithMessage("Email is not null").NotEmpty().WithMessage("Email is not empty").MinimumLength(3).WithMessage("Email must have at least three characters");
        }
    }
}
