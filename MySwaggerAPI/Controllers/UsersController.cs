using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySwaggerAPI.Data;
using MySwaggerAPI.Models; 

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

        // GET: /users/older-than-50
        [HttpGet("older-than-50")]
        public async Task<IActionResult> GetUsersOlderThan50()
        {
            var users = await _context.Users
                .Where(u => u.Age > 50)
                .ToListAsync();

            return Ok(users);
        }

        // POST /users/create
    //    [HttpPost("create")]
    //     public async Task<IActionResult> CreateUser([FromBody] User user)
    //     {
    //         await _context.Users.AddAsync(user);
    //         await _context.SaveChangesAsync();

    //         return Ok(user);
    //     }
    }
}