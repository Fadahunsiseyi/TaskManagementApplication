using TaskManagement.Common.Enums;

namespace TaskManagement.Domain.Entities;

public class Task : BaseEntity
{
    public  string Title { get; set; }
    public  string Description { get; set; }
    public  DateTime DueDate { get; set; }
    public TasksPriority Priority { get; set; }
    public TasksStatus Status { get; set; }
    public Guid ProjectId { get; set; }
    public Guid UserId { get; set; }


    public virtual Project Project { get; set; }
    public virtual User User { get; set; }
}
