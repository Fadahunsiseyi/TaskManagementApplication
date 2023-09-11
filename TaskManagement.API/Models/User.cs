namespace TaskManagement.API.Models;

public class User : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public List<Tasks> Tasks { get; set; }
}