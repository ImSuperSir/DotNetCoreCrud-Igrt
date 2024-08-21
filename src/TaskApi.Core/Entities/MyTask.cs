using System.ComponentModel.DataAnnotations;

namespace TaskApi.Core.Entities;

public class MyTask
{
    // [Key]
    public Guid Id { get; set; }
    public required string Title { get; set; } 
    public required string Description { get; set; } 
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}