namespace TaskManagement.Domain.Dtos.User;

public record UserGet(Guid Id, string Name, string Email, DateTime Created);
