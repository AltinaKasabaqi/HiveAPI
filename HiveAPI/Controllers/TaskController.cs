using HiveAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HiveAPI.Controllers
{
    [ApiController]
    [Route("/task")]
    public class TaskController : Controller
    {
        private readonly APIDbContext _context;

        public TaskController(APIDbContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetTasks")]
        [Authorize]
        public async Task<IActionResult> GetTasks()
        {
            var tasks = await _context.Tasks
                                    .Include(t => t.List)
                                    .ToListAsync();
            return Ok(tasks);
        }


        [HttpGet("{id}", Name = "GetTaskById")]
        [Authorize]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _context.Tasks
                                        .Include(t => t.List)
                                        .FirstOrDefaultAsync(t => t.TaskId == id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }


        [HttpPost(Name = "AddTask")]
        [Authorize]
        public async Task<IActionResult> AddTask([FromBody] Models.Task task)
        {
            var list = await _context.Lists.FirstOrDefaultAsync(l => l.ListId == task.ListId);

            if (list == null)
            {
                return BadRequest("List not found");
            }

            task.List = list;

            if (task == null)
            {
                return BadRequest("Task object is null");
            }

            var addedTask = await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return Ok(addedTask.Entity.TaskId);
        }

        [HttpPut("{id}", Name = "UpdateTask")]
        [Authorize]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] Models.Task task)
        {
            var existingTask = await _context.Tasks.FindAsync(id);

            if (existingTask == null)
            {
                return NotFound();
            }

            _context.Entry(existingTask).CurrentValues.SetValues(task);

            try
            {
                await _context.SaveChangesAsync();
                return Ok(task);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        [HttpPut("{id}/move")]
        [Authorize]
        public async Task<IActionResult> MoveTask(int id, [FromBody] int newListId)
        {
            var existingTask = await _context.Tasks.FindAsync(id);

            if (existingTask == null)
            {
                return NotFound();
            }

            existingTask.ListId = newListId; 

            try
            {
                await _context.SaveChangesAsync();
                return Ok(existingTask);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        [HttpDelete("{id}", Name = "DeleteTask")]
        [Authorize]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return Ok(task);
        }

        [HttpGet("list/{ListId}", Name = "GetTaskByListId")]
        [Authorize]
        public async Task<IActionResult> GetTaskByListId(int ListId)
        {
            var task = await _context.Set<Models.Task>()
                                        .Include(t => t.List)
                                        .Where(t => t.ListId == ListId)
                                        .ToListAsync();

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }
    }
}
