using HiveAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HiveAPI.Controllers
{
    [ApiController]
    [Route("/list")]
    public class ListsController : ControllerBase
    {
        private readonly APIDbContext _context;

        public ListsController(APIDbContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetLists")]
        public async Task<IActionResult> GetLists()
        {
            var lists = await _context.Set<List>()
                                      .Include(l => l.WorkSpace)
                                      .Select(l => new {
                                          l.ListId,
                                          l.ListName,
                                          WorkSpace = new
                                          {
                                              l.WorkSpace.WId,
                                              l.WorkSpace.WorkspaceName,
                                              l.WorkSpace.WorkspaceDescription
                                          }
                                      })
                                      .ToListAsync();

            return Ok(lists);
        }

        [HttpGet("{id}", Name = "GetListById")]
        public async Task<IActionResult> GetListById(int id)
        {
            var list = await _context.Set<List>()
                                     .Include(l => l.WorkSpace)
                                     .Select(l => new {
                                         l.ListId,
                                         l.ListName,
                                         WorkSpace = new
                                         {
                                             l.WorkSpace.WId,
                                             l.WorkSpace.WorkspaceName,
                                             l.WorkSpace.WorkspaceDescription
                                         }
                                     })
                                     .FirstOrDefaultAsync(x => x.ListId == id);

            if (list == null)
            {
                return NotFound();
            }

            return Ok(list);
        }

        [HttpPost(Name = "CreateList")]
        public async Task<IActionResult> CreateList([FromBody] List list)
        {
            if (list == null)
            {
                return BadRequest("List object is null");
            }

            _context.Set<List>().Add(list);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetListById), new { id = list.ListId }, list);
        }

        [HttpPut("{id}", Name = "UpdateList")]
        public async Task<IActionResult> UpdateList(int id, [FromBody] List list)
        {
            if (list == null || list.ListId != id)
            {
                return BadRequest("Invalid list object");
            }

            var existingList = await _context.Set<List>().FirstOrDefaultAsync(x => x.ListId == id);
            if (existingList == null)
            {
                return NotFound("List not found");
            }

            existingList.ListName = list.ListName;
            // Update other properties as needed

            _context.Set<List>().Update(existingList);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteList")]
        public async Task<IActionResult> DeleteList(int id)
        {
            var list = await _context.Set<List>().FindAsync(id);
            if (list == null)
            {
                return NotFound();
            }

            _context.Set<List>().Remove(list);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
