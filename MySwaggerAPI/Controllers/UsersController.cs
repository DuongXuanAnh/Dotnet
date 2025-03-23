using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySwaggerAPI.Data;

namespace MySwaggerAPI.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public UsersController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/users/older-than-50
        [HttpGet("older-than-50")]
        public async Task<IActionResult> GetUsersOlderThan50()
        {
            var users = await _context.Users
                .Where(u => u.Age > 50)
                .ToListAsync();

            return Ok(users);
        }
    }
}