using TaskManagement.API.Common;

namespace TaskManagement.API.Models;

public class Tasks : BaseEntity
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTime? DueDate { get; set; }
    public TasksPriority Priority { get; set; }

    public TasksStatus Status { get; set; }

    public int ProjectId { get; set; }
    public Project Project { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
}
