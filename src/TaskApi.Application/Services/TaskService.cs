using TaskApi.Core.Entities;
using TaskApi.Core.Interfaces;

namespace TaskApi.Application.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _TaskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        this._TaskRepository = taskRepository;
    }



    public async Task<IEnumerable<MyTask>> GetTasksAsync()
    {
        return await _TaskRepository.GetTasksAsync();
    }

    public async Task<MyTask> GetTaskByIdAsync(Guid id)
    {
        return await _TaskRepository.GetTaskByIdAsync(id);
    }



    public async Task AddTaskAsync(MyTask task)
    {
        await _TaskRepository.AddTaskAsync(task);
    }

    public async Task UpdateTaskAsync(MyTask task)
    {
        await _TaskRepository.UpdateTaskAsync(task);
    }

    public async Task DeleteTaskAsync(Guid id)
    {
        await _TaskRepository.DeleteTaskAsync(id);
    }





}
