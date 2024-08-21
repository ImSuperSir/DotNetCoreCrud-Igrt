using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TaskApi.Api.ServiceDtos;
using TaskApi.Core.Entities;
using TaskApi.Core.Interfaces;

namespace TaskApi.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ILogger<TaskController> _Logger;
        private readonly ITaskService _TaskService;

        public TaskController(ILogger<TaskController> logger, ITaskService taskService)
        {
            _Logger = logger;
            _TaskService = taskService;
        }

        [HttpGet(Name = "GetTasks")]
        public async Task<IActionResult> GetTasks() //  IEnumerable<TaskDto>> Get()
        {
            var tasks = await _TaskService.GetTasksAsync();
            return Ok(tasks.Select(task => task.AsDto()));
        }

        [HttpGet("{id}", Name = "GetTask")]
        public async Task<IActionResult> GetTask(Guid id)
        {
            var task = await _TaskService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            return Ok(task.AsDto());
        }

        [HttpPost(Name = "CreateTask")]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto task)
        {
            var lTask = task.AsEntity();
            await _TaskService.AddTaskAsync(lTask);
            return CreatedAtAction(nameof(GetTask), new { id = lTask.Id }, task);
        }

        [HttpPut("{id}", Name = "UpdateTask")]
        public async Task<IActionResult> UpdateTask(Guid id, [FromBody] UpdateTaskDto task)
        {

            if(id != task.Id)
            {
                return BadRequest();
            }

            var lTask = await _TaskService.GetTaskByIdAsync(id);
            
            if (lTask == null)
            {
                return NotFound();
            }

            lTask.Title = task.Title;
            lTask.Description = task.Description;
            lTask.UpdatedAt = DateTime.UtcNow;
            
            await _TaskService.UpdateTaskAsync(lTask);
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteTask")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            var lTask = await _TaskService.GetTaskByIdAsync(id);
            if (lTask == null)
            {
                return NotFound();
            }
            await _TaskService.DeleteTaskAsync(id);
            return NoContent();
        }

        [HttpPatch]
        public async Task<IActionResult> PatchTask(Guid id, [FromBody] JsonPatchDocument<MyTask> patch)
        {
            if (patch == null)
            {
                return BadRequest();
            }

            var task = await _TaskService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            patch.ApplyTo(task);

            if (!TryValidateModel(task))
            {
                return ValidationProblem(ModelState);
            }

            task.UpdatedAt = DateTime.UtcNow;
            await _TaskService.UpdateTaskAsync(task);
            return NoContent();
        }
    }
}