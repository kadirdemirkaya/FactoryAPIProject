using FactoryAPIProject.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FactoryAPIProject.Validations
{
    public class RegisterValidator : AbstractValidator<RegisterModel>
    {
        public RegisterValidator()
        {
            RuleFor(r => r.Username).NotNull().WithMessage("Username is not null").NotEmpty().WithMessage("Username is not empty").MinimumLength(3).WithMessage("Username must have at least three characters").MaximumLength(100).WithMessage("Username must be no more than 100 characters");

            RuleFor(r => r.Password).NotNull().WithMessage("Password is not null").NotEmpty().WithMessage("Password is not empty").MinimumLength(3).WithMessage("Password must have at least three characters").Matches("^.{3,}$").WithMessage("Password must at least 3 character");

            RuleFor(r => r.Email).NotNull().WithMessage("Email is not null").NotEmpty().WithMessage("Email is not empty").MinimumLength(3).WithMessage("Email must have at least three characters").Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$").WithMessage("Email must be in a valid format.");

            RuleFor(r => r.ConfirmPassword).NotNull().WithMessage("Confirm Password is not null").NotEmpty().WithMessage("Confirm Password is not null").Equal(r => r.Password).WithMessage("Password and Confirm Password must Equal");
        }
    }
}
