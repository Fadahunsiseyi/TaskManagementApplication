namespace TaskManagement.API.Models;

public class Project : BaseEntity
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required List<Tasks> Tasks { get; set; }
}