using Microsoft.EntityFrameworkCore;
using TaskApi.Core.Entities;
using TaskApi.Core.Interfaces;
using TaskApi.Infrastructure.Data;

namespace TasksApi.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly TaskDbContext _Context;

    public TaskRepository(TaskDbContext context)
    {
        _Context = context;
    }


    public async Task<IEnumerable<MyTask>> GetTasksAsync()
    {
        return await _Context.Tasks.ToListAsync();
    }


    public async Task<MyTask> GetTaskByIdAsync(Guid id)
    {
        var lTask =  await _Context.Tasks.FindAsync(id);
        if (lTask == null)
        {
            throw new KeyNotFoundException($"Task with id {id} not found");
        }
        return lTask;
    }

    public async Task AddTaskAsync(MyTask task)
    {
        await _Context.Tasks.AddAsync(task);
        await _Context.SaveChangesAsync();
    }

    public async Task UpdateTaskAsync(MyTask task)
    {
        _Context.Tasks.Update(task);    
        await _Context.SaveChangesAsync();
    }

    public async Task DeleteTaskAsync(Guid id)
    {
        var task = await _Context.Tasks.FindAsync(id);
        if (task != null)
        {
            _Context.Tasks.Remove(task);
            await _Context.SaveChangesAsync();
        }
    }






}

   
   