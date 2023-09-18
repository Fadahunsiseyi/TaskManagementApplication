using FluentValidation;
using TaskManagement.Common.Dtos.Task;

namespace TaskManagement.Application.Validation;

public class TaskCreateValidator : AbstractValidator<TaskCreate>
{
    public TaskCreateValidator()
    {
        RuleFor(taskCreate => taskCreate.Title).NotEmpty().WithMessage("Title is required").MinimumLength(3).WithMessage("Title must be at least 3 characters long").MaximumLength(40).WithMessage("Title must be at most 40 characters long");
        RuleFor(taskCreate => taskCreate.Description).NotEmpty().WithMessage("Description is required").MinimumLength(10).WithMessage("Description must be at least 10 characters long").MaximumLength(100).WithMessage("Description must be at most 100 characters long");
        RuleFor(taskCreate => taskCreate.Priority).NotEmpty().WithMessage("Priority is required");
        RuleFor(taskCreate => taskCreate.Status).NotEmpty().WithMessage("Status is required");
    }
}
