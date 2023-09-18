namespace TaskManagement.Common.Dtos.User;

public record UserGet(Guid Id, string Name, string Email, DateTime Created);
