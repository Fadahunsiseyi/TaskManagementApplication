using FluentValidation;
using TaskManagement.Common.Dtos.User;

namespace TaskManagement.Application.Validation;

public class UserCreateValidator : AbstractValidator<UserCreate>
{
    public UserCreateValidator()
    {
        RuleFor(userCreate => userCreate.Name).NotEmpty().WithMessage("Name is required").MinimumLength(3).WithMessage("Name must be at least 3 characters long").MaximumLength(30).WithMessage("Name must be at most 30 characters long");
        RuleFor(userCreate => userCreate.Email).NotEmpty().EmailAddress().MaximumLength(30).WithMessage("Email is required");
    }
}
