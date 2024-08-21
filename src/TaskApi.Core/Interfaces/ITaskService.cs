using TaskApi.Core.Entities;

namespace TaskApi.Core.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<MyTask>> GetTasksAsync();
        Task<MyTask> GetTaskByIdAsync(Guid id);
        Task AddTaskAsync(MyTask task);
        Task UpdateTaskAsync(MyTask task);
        Task DeleteTaskAsync(Guid id);
    }
}