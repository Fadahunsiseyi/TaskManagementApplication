namespace TaskManagement.API.Models;

public class Project : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Tasks> Tasks { get; set; }
}