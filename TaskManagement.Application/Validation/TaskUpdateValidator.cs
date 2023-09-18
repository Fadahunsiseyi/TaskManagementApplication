using FluentValidation;
using TaskManagement.Common.Dtos.Task;

namespace TaskManagement.Application.Validation;

public class TaskUpdateValidator : AbstractValidator<TaskUpdate>
{
    public TaskUpdateValidator()
    {
        RuleFor(taskUpdate => taskUpdate.Title).NotEmpty().WithMessage("Title is required").MinimumLength(3).WithMessage("Title must be at least 3 characters long").MaximumLength(40).WithMessage("Title must be at most 40 characters long");
        RuleFor(taskUpdate => taskUpdate.Description).NotEmpty().WithMessage("Description is required").MinimumLength(10).WithMessage("Description must be at least 10 characters long").MaximumLength(100).WithMessage("Description must be at most 100 characters long");
        RuleFor(taskUpdate => taskUpdate.Priority).NotEmpty().WithMessage("Priority is required");
        RuleFor(taskUpdate => taskUpdate.Status).NotEmpty().WithMessage("Status is required");
    }
}
