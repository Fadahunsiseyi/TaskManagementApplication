using TaskManagement.Common.Dtos.Project;
using TaskManagement.Common.Dtos.User;

namespace TaskManagement.Common.Dtos.Task;

public record TaskDetails(Guid Id, string Title, string Description, UserGet User, ProjectGet Project);
