using backend.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public UserController(ApiDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }

        // Akcja POST, która przyjmuje nowego użytkownika
        [HttpPost]
        public IActionResult AddUser([FromBody] string user)
        {
            // Logika dodania użytkownika (tu jest tylko przykład)
            return Ok($"User {user} added successfully.");
        }
    }
}
