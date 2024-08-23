using System.ComponentModel.DataAnnotations;

namespace TaskApi.Core.Entities;

public class MyTask
{
    // [Key]
    public Guid Id { get; set; }
    public required string Title { get; set; } 
    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}