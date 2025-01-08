using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> GetUsers()
        {
            return new List<string>
            {
                "Alice",
                "Bob",
                "Charlie",
                "Jerry",
                "Morty",
                "Ziggy"
            };
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
