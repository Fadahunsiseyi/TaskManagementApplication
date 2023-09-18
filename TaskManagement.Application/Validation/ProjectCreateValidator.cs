﻿using FluentValidation;
using TaskManagement.Common.Dtos.Project;

namespace TaskManagement.Application.Validation;

public class ProjectCreateValidator : AbstractValidator<ProjectCreate>
{
    public ProjectCreateValidator()
    {
        RuleFor(projectCreate => projectCreate.Name).NotEmpty().WithMessage("Name is required").MinimumLength(3).WithMessage("Title must be at least 3 characters long").MaximumLength(30).WithMessage("Name must be at most 30 characters long");
        RuleFor(projectCreate => projectCreate.Description).NotEmpty().WithMessage("Description is required").MinimumLength(10).WithMessage("Description must be at least 10 characters long").MaximumLength(100).WithMessage("Description must be at most 100 characters long");
    }
}
