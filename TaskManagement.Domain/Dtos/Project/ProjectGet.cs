namespace TaskManagement.Domain.Dtos.Project;

public record ProjectGet(Guid Id, string Name, string Description, DateTime Created);
