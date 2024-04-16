using HiveAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HiveAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
            var WorkSpaces = await _context.WorkSpaces.ToListAsync();

            return Ok(WorkSpaces);
        }

        [HttpGet("{id}", Name = "GetWorkSpaceById")]
        public async Task<IActionResult> GetWorkSpaceById(int id)
        {
            var WorkSpace = await _context.WorkSpaces.FirstOrDefaultAsync(x => x.WId == id);

            if (WorkSpace == null)
            {
                return NotFound();
            }

            return Ok(WorkSpace);
        }

        [HttpPost(Name = "AddWorkSpace")]
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