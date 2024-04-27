using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HiveAPI.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize]
        public async Task<IActionResult> GetCollaborators()
        {
            var collaborators = await _context.Collaborators.ToListAsync();
            return Ok(collaborators);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetCollaboratorById(int id)
        {
            var collaborator = await _context.Collaborators.FindAsync(id);
            if (collaborator == null)
                return NotFound();

            return Ok(collaborator);
        }

        [HttpGet("by-email/{userEmail}")]
        [Authorize]
        public async Task<IActionResult> GetCollaboratorsByEmail(string userEmail)
        {

            var collaborators = await _context.Collaborators
                .Include(c => c.WorkSpace)
                .Where(c => c.UserEmail == userEmail)
                .ToListAsync();

            if (collaborators.Count == 0)
                return NotFound();

            return Ok(collaborators);
        }

        [HttpGet("by-workspaceId/{workspaceId}")]
        [Authorize]
        public async Task<IActionResult> GetCollaboratorsByWorkspaceId(int workspaceId)
        {
            var collaborators = await _context.Collaborators
                .Where(c => c.WorkSpaceId == workspaceId)
                .ToListAsync();

            if (collaborators.Count == 0)
                return NotFound();

            return Ok(collaborators);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateCollaborator([FromBody] Collaborator collaborator)
        {
            var workspace = await _context.WorkSpaces.FirstOrDefaultAsync(ws => ws.WId == collaborator.WorkSpaceId);

            if (workspace == null)
            {
                return BadRequest("Workspace not found");
            }

            var existingCollaborator = await _context.Collaborators.FirstOrDefaultAsync(c => c.UserEmail == collaborator.UserEmail && c.WorkSpaceId == collaborator.WorkSpaceId);
            if (existingCollaborator != null)
            {
                return Conflict("The Collaborator already exists");
            }

            collaborator.WorkSpace = workspace;

            _context.Collaborators.Add(collaborator);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCollaboratorById), new { id = collaborator.CollaboratorId }, collaborator);
        }

        [HttpPut("{id}")]
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> DeleteCollaborator(int id)
        {
            var collaborator = await _context.Collaborators.FindAsync(id);
            if (collaborator == null)
                return NotFound();

            _context.Collaborators.Remove(collaborator);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
