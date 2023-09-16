using TaskManagement.Common.Enums;

namespace TaskManagement.Domain.Dtos.Task;

public record TaskCreate(string Title, string Description, string Priority, string Status, Guid ProjectId, Guid UserId);