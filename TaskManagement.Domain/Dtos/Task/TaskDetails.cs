using TaskManagement.Domain.Dtos.Project;
using TaskManagement.Domain.Dtos.User;

namespace TaskManagement.Domain.Dtos.Task;

public record TaskDetails(Guid Id, string Title, string Description, UserGet User, ProjectGet Project);
