namespace TaskManagement.Domain.Entities;

public class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime Created { get; set; }
    //public byte[]? RowVersion { get; set; }
}
