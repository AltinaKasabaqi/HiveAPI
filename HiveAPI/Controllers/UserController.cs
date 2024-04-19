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

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User user)
        {
            var existingUser = _context.Users.Find(id);

            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.name = user.name ?? existingUser.name;
            existingUser.email = user.email ?? existingUser.email;
            var hashedPass = BCrypt.Net.BCrypt.HashPassword(user.password);

            user.password = hashedPass;

            _context.Users.Update(existingUser);
            _context.SaveChanges();

            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            return Ok();
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
