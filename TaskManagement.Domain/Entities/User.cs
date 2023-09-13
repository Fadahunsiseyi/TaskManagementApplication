﻿namespace TaskManagement.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }


    public virtual ICollection<Domain.Entities.Task> Tasks { get; set; } = new HashSet<Domain.Entities.Task>();
    public virtual ICollection<Notification> Notifications { get; set; } = new HashSet<Notification>();
}