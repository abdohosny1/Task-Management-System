using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyTask_Management_System.Core.Repository;
using MyTask_Management_System.Dto;
using System.Linq;
using System.Threading.Tasks;

namespace MyTask_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TaskController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TaskController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Task
        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await _unitOfWork.TaskModels.GellAllAsync(); // Fetch all tasks
            if (tasks == null || !tasks.Any())
            {
                return NotFound("No tasks found.");
            }
            return Ok(tasks); // Return the list of tasks
        }

        // GET: api/Task/{id}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _unitOfWork.TaskModels.GetByIDAsync(id); // Fetch task by id
            if (task == null)
            {
                return NotFound($"Task with ID {id} not found.");
            }
            return Ok(task); // Return the found task
        }

        // POST: api/Task
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskModel task)
        {
            if (task == null)
            {
                return BadRequest("Task data is null.");
            }

            var createdTask = await _unitOfWork.TaskModels.AddAsync(task); 
            return CreatedAtAction(nameof(GetTaskById), new { id = createdTask.Id }, createdTask); 
        }

        // PUT: api/Task/{id}
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskDto task)
        {
            if (task == null)
            {
                return BadRequest("Task data is invalid.");
            }

            var existingTask = await _unitOfWork.TaskModels.GetByIDAsync(id);
            if (existingTask == null)
            {
                return NotFound($"Task with ID {id} not found.");
            }

            existingTask.DueDate = task.DueDate;
            existingTask.Status = task.Status;
            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            var updatedTask = await _unitOfWork.TaskModels.UpdateAsync(existingTask); 
            return Ok(updatedTask); 
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _unitOfWork.TaskModels.GetByIDAsync(id);
            if (task == null)
            {
                return NotFound($"Task with ID {id} not found.");
            }

            await _unitOfWork.TaskModels.DeleteAsync(task); 
            return NoContent(); 
        }
    }
}
