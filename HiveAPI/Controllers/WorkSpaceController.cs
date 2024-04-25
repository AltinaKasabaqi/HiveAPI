using HiveAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HiveAPI.Controllers
{
    [ApiController]
    [Route("/workspace")]
   
    public class WorkSpacesController : ControllerBase
    {

        private readonly APIDbContext _context;

        public WorkSpacesController(APIDbContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetWorkSpaces")]
        public async Task<IActionResult> GetWorkSpaces()
        {
            var WorkSpaces = await _context.WorkSpaces
                                                .Include(u => u.User)
                                                .Select(ws => new {
                                                    ws.WId,
                                                    ws.WorkspaceName,
                                                    ws.WorkspaceDescription,
                                                    User = new
                                                    {
                                                        ws.User.UserId,
                                                        ws.User.name,
                                                        ws.User.email
                                                    }
                                                })
                                                .ToListAsync();

            return Ok(WorkSpaces);
        }

        [HttpGet("{id}", Name = "GetWorkSpaceById")]
        [Authorize]
        public async Task<IActionResult> GetWorkSpaceById(int id)
        {
            var WorkSpace = await _context.WorkSpaces
                                                .Include(u => u.User)
                                                .Select(ws => new {
                                                    ws.WId,
                                                    ws.WorkspaceName,
                                                    ws.WorkspaceDescription,
                                                    User = new
                                                    {
                                                        ws.User.UserId,
                                                        ws.User.name,
                                                        ws.User.email
                                                    }
                                                })
                                                .FirstOrDefaultAsync(x => x.WId == id);

            if (WorkSpace == null)
            {
                return NotFound();
            }

            return Ok(WorkSpace);
        }

        [HttpGet("user/{userId}", Name = "GetWorkSpacesByUserId")]
        [Authorize]
        public async Task<IActionResult> GetWorkSpacesByUserId(int userId)
        {
            var workSpaces = await _context.WorkSpaces
                                            .Include(w => w.User)
                                            .Where(w => w.UserId == userId)
                                            .Select(ws => new {
                                                ws.WId,
                                                ws.WorkspaceName,
                                                ws.WorkspaceDescription,
                                                User = new
                                                {
                                                    ws.User.UserId,
                                                    ws.User.name,
                                                    ws.User.email
                                                }
                                            })
                                            .ToListAsync();

            if (workSpaces == null || workSpaces.Count == 0)
            {
                return NotFound("No workspaces found for the specified user ID.");
            }

            return Ok(workSpaces);
        }

        [HttpPost(Name = "AddWorkSpace")]
        [Authorize]
        public async Task<IActionResult> AddWorkSpace(WorkSpace workSpace)
        {

            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == workSpace.UserId);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            workSpace.User = user;

            var addedWorkSpace = await _context.WorkSpaces.AddAsync(workSpace);

            await _context.SaveChangesAsync();

            return Ok(addedWorkSpace.Entity.WId);
        }

        [HttpPut("{id}", Name = "UpdateWorkSpace")]
        [Authorize]
        public async Task<IActionResult> UpdateWorkSpace(int id, WorkSpace workSpace)
        {
            var WorkSpace = await _context.WorkSpaces.FirstOrDefaultAsync(x => x.WId == id);

            if (WorkSpace == null)
            {
                return NotFound();
            }

            _context.Entry(WorkSpace).CurrentValues.SetValues(workSpace);

            try
            {
                await _context.SaveChangesAsync();
                return Ok(workSpace);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

        }

        [HttpDelete("{id}", Name = "DeleteWorkSpace")]
        [Authorize]
        public async Task<IActionResult> DeleteWorkSpace(int id)
        {
            var WorkSpace = await _context.WorkSpaces.FirstOrDefaultAsync(x => x.WId == id);

            if (WorkSpace == null)
            {
                return NotFound();
            }

            _context.WorkSpaces.Remove(WorkSpace);
            await _context.SaveChangesAsync();

            return Ok(WorkSpace);
        }


    }
}