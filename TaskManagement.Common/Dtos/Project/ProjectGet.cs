namespace TaskManagement.Common.Dtos.Project;

public record ProjectGet(Guid Id, string Name, string Description, DateTime Created);
