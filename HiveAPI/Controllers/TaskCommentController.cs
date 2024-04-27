using HiveAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HiveAPI.Controllers
{
    [ApiController]
    [Route("/taskcomment")]
    public class TaskCommentController : Controller
    {
        private readonly APIDbContext _context;

        public TaskCommentController(APIDbContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetTaskComments")]
        [Authorize]
        public async Task<IActionResult> GetTaskComments()
        {
            var taskComments = await _context.TaskComments
                                                    .Include(tc => tc.Task)
                                                    .ToListAsync();
            return Ok(taskComments);
        }

        [HttpGet("{id}", Name = "GetTaskCommentById")]
        [Authorize]
        public async Task<IActionResult> GetTaskCommentById(int id)
        {
            var taskComment = await _context.TaskComments
                                                    .Include(tc => tc.Task)
                                                    .FirstOrDefaultAsync(tc => tc.TaskCommentsId == id);

            if (taskComment == null)
            {
                return NotFound();
            }

            return Ok(taskComment);
        }

        [HttpPost(Name = "AddTaskComment")]
        [Authorize]
        public async Task<IActionResult> AddTaskComment([FromBody] TaskComment taskComment)
        {

            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.TaskId == taskComment.TaskId);

            if (task == null)
            {
                return BadRequest("Task not found");
            }

            taskComment.Task = task;

            if (taskComment == null)
            {
                return BadRequest("TaskComment object is null");
            }

            var addedTaskComment = await _context.TaskComments.AddAsync(taskComment);
            await _context.SaveChangesAsync();
            return Ok(addedTaskComment.Entity.TaskCommentsId);
        }

        [HttpPut("{id}", Name = "UpdateTaskComment")]
        [Authorize]
        public async Task<IActionResult> UpdateTaskComment(int id, [FromBody] TaskComment taskComment)
        {
            var existingTaskComment = await _context.TaskComments.FindAsync(id);

            if (existingTaskComment == null)
            {
                return NotFound();
            }

            _context.Entry(existingTaskComment).CurrentValues.SetValues(taskComment);

            try
            {
                await _context.SaveChangesAsync();
                return Ok(taskComment);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        [HttpDelete("{id}", Name = "DeleteTaskComment")]
        [Authorize]
        public async Task<IActionResult> DeleteTaskComment(int id)
        {
            var taskComment = await _context.TaskComments.FindAsync(id);

            if (taskComment == null)
            {
                return NotFound();
            }

            _context.TaskComments.Remove(taskComment);
            await _context.SaveChangesAsync();

            return Ok(taskComment);
        }

        [HttpGet("task/{TaskId}", Name = "GetTaskCommentsByTaskId")]
        [Authorize]
        public async Task<IActionResult> GetTaskByListId(int TaskId)
        {
            var taskComments = await _context.Set<TaskComment>()
                                        .Include(t => t.Task)
                                        .Where(t => t.TaskId == TaskId)
                                        .ToListAsync();

            if (taskComments == null)
            {
                return NotFound();
            }

            return Ok(taskComments);
        }
    }
}
