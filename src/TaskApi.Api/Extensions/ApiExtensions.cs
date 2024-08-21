using TaskApi.Api.ServiceDtos;
using TaskApi.Core.Entities;

namespace TaskApi.Api;


public static class Extensions
{    public static TaskDto AsDto(this MyTask task)
    {
        return new TaskDto(task.Id, task.Title, task.Description, task.CreatedAt, task.UpdatedAt);
    }

    public static MyTask AsEntity(this CreateTaskDto task)
    {
        return new MyTask
        {
            Id = Guid.NewGuid(),
            Title = task.Title,
            Description = task.Description,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public static MyTask AsEntity(this UpdateTaskDto task)
    {
        return new MyTask
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            UpdatedAt = DateTime.UtcNow
        };
    }
}