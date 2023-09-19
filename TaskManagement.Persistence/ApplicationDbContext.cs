using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Persistence;

public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Domain.Entities.Task> Tasks { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Project>().HasKey(p => p.Id);
        builder.Entity<Notification>().HasKey(p => p.Id);
        builder.Entity<Domain.Entities.Task>().HasKey(p => p.Id);
        builder.Entity<User>().HasKey(p => p.Id);


        builder.Entity<User>()
           .HasMany(user => user.Tasks)
           .WithOne(task => task.User)
           .HasForeignKey(task => task.UserId);


        builder.Entity<Project>()
            .HasMany(project => project.Tasks)
            .WithOne(task => task.Project)
            .HasForeignKey(task => task.ProjectId);



        builder.Entity<User>()
            .HasMany(user => user.Notifications)
            .WithOne(notification => notification.User)
            .HasForeignKey(notification => notification.UserId);


           base.OnModelCreating(builder);


    }
}