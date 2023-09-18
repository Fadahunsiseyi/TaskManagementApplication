using FluentValidation;
using TaskManagement.Common.Dtos.Project;

namespace TaskManagement.Application.Validation;

public class ProjectUpdateValidator : AbstractValidator<ProjectUpdate>
{
    public ProjectUpdateValidator()
    {
        RuleFor(projectUpdate => projectUpdate.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(projectUpdate => projectUpdate.Description).NotEmpty().WithMessage("Description is required").MinimumLength(10).WithMessage("Description must be at least 10 characters long").MaximumLength(100).WithMessage("Description must be at most 100 characters long");
    }
}
