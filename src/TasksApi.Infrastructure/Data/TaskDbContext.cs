using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskApi.Core.Entities; // Add a reference to the Microsoft.EntityFrameworkCore assembly

namespace TaskApi.Infrastructure.Data;

public class TaskDbContext : IdentityDbContext<ApplicationUser>
{
    public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
    {
    }

    override protected void OnModelCreating(ModelBuilder modelBuilder) 
    {

        base.OnModelCreating(modelBuilder);  //cause of identity or will not work
        modelBuilder.Entity<MyTask>().ToTable("Tasks");
        modelBuilder.Entity<MyTask>().HasKey(task => task.Id);
    }

    public DbSet<MyTask> Tasks { get; set; }
}