using HiveAPI.Models;
using HiveAPI.Services.TaskCollabServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace HiveAPI.Controllers
{
    [ApiController]
    [Route("/taskCollab")]
    public class TaskCollabController : Controller
    {
        private readonly ITaskCollabService _taskCollabService;

        public TaskCollabController(ITaskCollabService taskCollabService)
        {
            _taskCollabService = taskCollabService;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<TaskCollab>> GetTaskCollabById(int id)
        {
           
                var taskCollab = await _taskCollabService.GetTaskCollabById(id);
                if (taskCollab == null)
                {
                    return NotFound();
                }
                return Ok(taskCollab);
           
        }

       

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateTaskCollab(TaskCollab taskCollab)
        {
            
                var taskId = await _taskCollabService.AddTaskCollab(taskCollab);
                return Ok(taskId);
           
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> UpdateTaskCollab(int id, TaskCollab taskCollab)
        {
           
                await _taskCollabService.UpdateTaskCollab(id, taskCollab);
                return NoContent();
           
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteTaskCollab(int id)
        {

            await _taskCollabService.DeleteTaskCollab(id);
            return NoContent();
        }
    }
}
