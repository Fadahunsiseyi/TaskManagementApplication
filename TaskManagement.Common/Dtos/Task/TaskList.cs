namespace TaskManagement.Common.Dtos.Task;

public record TaskList(Guid Id, string Title, string Description, string Priority, string Status, DateTime DueDate);
