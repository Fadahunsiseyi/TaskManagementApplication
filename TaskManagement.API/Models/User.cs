namespace TaskManagement.API.Models;

public class User : BaseEntity
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required List<Tasks> Tasks { get; set; }
}