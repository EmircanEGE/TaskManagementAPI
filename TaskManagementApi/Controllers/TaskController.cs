using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.Core.Model;
using TaskManagementApi.Data;

namespace TaskManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly Context _context;

        public TaskController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetTasks() {
            return Ok(_context.Task.ToList());
        }

        [HttpPost]
        public IActionResult CreateTask(TaskItem task)
        {
            _context.Task.Add(task);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetTasks), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, TaskItem updatedTask)
        {
            var task = _context.Task.Find(id);
            if (task == null) return NotFound();

            task.Title = updatedTask.Title;
            task.Description = updatedTask.Description;
            task.IsCompleted = updatedTask.IsCompleted;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var task = _context.Task.Find(id);
            if (task == null) return NotFound();

            _context.Task.Remove(task);
            _context.SaveChanges();
            return NoContent();
        }
    }
} 
