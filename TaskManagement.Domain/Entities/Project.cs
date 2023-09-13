namespace TaskManagement.Domain.Entities;

public class Project : BaseEntity
{
    public  string Name { get; set; }
    public  string Description { get; set; }

    public virtual ICollection<Domain.Entities.Task> Tasks { get; set; } = new HashSet<Domain.Entities.Task>();
}