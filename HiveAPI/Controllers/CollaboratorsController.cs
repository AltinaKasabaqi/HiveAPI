using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HiveAPI.Models;
using System.Threading.Tasks;

namespace HiveAPI.Controllers
{
    [ApiController]
    [Route("api/collaborators")]
    public class CollaboratorsController : ControllerBase
    {
        private readonly APIDbContext _context;

        public CollaboratorsController(APIDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCollaborators()
        {
            var collaborators = await _context.Collaborators.ToListAsync();
            return Ok(collaborators);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCollaboratorById(int id)
        {
            var collaborator = await _context.Collaborators.FindAsync(id);
            if (collaborator == null)
                return NotFound();

            return Ok(collaborator);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCollaborator(Collaborator collaborator)
        {
            _context.Collaborators.Add(collaborator);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCollaboratorById), new { id = collaborator.CollaboratorId }, collaborator);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCollaborator(int id, Collaborator updatedCollaborator)
        {
            var existingCollaborator = await _context.Collaborators.FindAsync(id);
            if (existingCollaborator == null)
                return NotFound();

            existingCollaborator.UserEmail = updatedCollaborator.UserEmail;
            existingCollaborator.WorkSpaceId = updatedCollaborator.WorkSpaceId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCollaborator(int id)
        {
            var collaborator = await _context.Collaborators.FindAsync(id);
            if (collaborator == null)
                return NotFound();

            _context.Collaborators.Remove(collaborator);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpGet("by-email")]
        public async Task<IActionResult> GetCollaboratorByEmail(string email)
        {
            var collaborator = await _context.Collaborators.FirstOrDefaultAsync(c => c.UserEmail == email);
            if (collaborator == null)
                return NotFound();

            return Ok(collaborator);
        }

    }
}
