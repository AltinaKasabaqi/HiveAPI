using HiveAPI.Modals;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;

namespace HiveAPI.Controllers
{
    [ApiController]
    [Route("/users")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly APIDbContext _context;

        public UserController(ILogger<UserController> logger, APIDbContext _dbcontext)
        {
            _logger = logger;
            _context = _dbcontext;
           
        }

        [HttpPost]
        public IActionResult Create([FromBody] User user)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_context.Users.Any(u => u.email == user.email))
            {
                return Conflict("Ky mejl është tashmë i regjistruar.");
            }

            var hashedPass = BCrypt.Net.BCrypt.HashPassword(user.password);

            user.password = hashedPass;

            _context.Users.Add(user);
            _context.SaveChanges();

            // Kthejë një përgjigje të suksesshme
            return Ok();
        }
    }
}
