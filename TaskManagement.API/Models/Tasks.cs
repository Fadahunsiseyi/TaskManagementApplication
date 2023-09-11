using TaskManagement.API.Common;

namespace TaskManagement.API.Models;

public class Tasks : BaseEntity
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public DateTime? DueDate { get; set; }
    public TasksPriority Priority { get; set; }

    public TasksStatus Status { get; set; }

    public int ProjectId { get; set; }
    public Project? Project { get; set; }

    public int UserId { get; set; }
    public User? User { get; set; }
}
