//using TaskManagement.Common.Enums;

namespace TaskManagement.Common.Dtos.Task;

public record TaskCreate(string Title, string Description, string Priority, string Status, Guid ProjectId, Guid UserId);