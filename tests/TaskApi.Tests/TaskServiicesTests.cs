using Moq;
using TaskApi.Application.Services;
using TaskApi.Core.Entities;
using TaskApi.Core.Interfaces;

namespace TaskApi.Tests.UnitTests;


public class TaskServiceTests
{

    public readonly Mock<ITaskRepository> _MockTaskRepositoryMock;
    public readonly ITaskService _TaskService;

    public TaskServiceTests()
    {
        _MockTaskRepositoryMock = new Mock<ITaskRepository>();
        _TaskService = new TaskService(_MockTaskRepositoryMock.Object);
    }


    [Fact]
    public async Task GetTasksAsync_ShouldReturnAllTasks()
    {
        // Arrange
        var tasks = new List<MyTask>
        {
            new MyTask { Id = new Guid(), Title = "Task 1", Description = "Task 1 Description" },
            new MyTask { Id = new Guid(), Title = "Task 2", Description = "Task 2 Description" },
            new MyTask { Id = new Guid(), Title = "Task 3", Description = "Task 3 Description" }
        };

        _MockTaskRepositoryMock.Setup(x => x.GetTasksAsync()).ReturnsAsync(tasks);

        // Act
        var result = await _TaskService.GetTasksAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Count());
    }


//  [Fact]
//         public async Task AddTaskAsync_ShouldAddTask()
//         {
//             // Arrange
//             var task = new MyTask { Id = 1, Title = "Task 1", Description = "Task 1 Description", IsCompleted = false, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };

//             _MockTaskRepositoryMock.Setup(x => x.AddTaskAsync(task)).Returns(Task.CompletedTask);

//             // Act
//             await _TaskService.AddTaskAsync(task);

//             // Assert
//             _MockTaskRepositoryMock.Verify(x => x.AddTaskAsync(task), Times.Once);
//         }

        // [Fact]
        // public async Task UpdateTaskAsync_ShouldUpdateTask()
        // {
        //     // Arrange
        //     var task = new MyTask { Id = 1, Title = "Task 1", Description = "Task 1 Description", IsCompleted = false, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };

        //     _MockTaskRepositoryMock.Setup(x => x.UpdateTaskAsync(task)).Returns(Task.CompletedTask);

        //     // Act
        //     await _TaskService.UpdateTaskAsync(task);

        //     // Assert
        //     _MockTaskRepositoryMock.Verify(x => x.UpdateTaskAsync(task), Times.Once);
        // }

        // [Fact]
        // public async Task DeleteTaskAsync_ShouldDeleteTask()
        // {
        //     // Arrange
        //     var taskId = 1;

        //     _MockTaskRepositoryMock.Setup(x => x.DeleteTaskAsync(taskId)).Returns()

        //     // Act
        //     await _TaskService.DeleteTaskAsync(taskId);

        //     // Assert
        //     _MockTaskRepositoryMock.Verify(x => x.DeleteTaskAsync(taskId), Times.Once);
        // }
}