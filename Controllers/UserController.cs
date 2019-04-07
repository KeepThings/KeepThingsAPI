using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeepThingsAPI.Models;

namespace KeepThingsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext _context;

        public UserController(UserContext context)
        {
            _context = context;

            if (_context.Users.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                //_context.Users.Add(new User { ID = 0,  NAME = "Braun", FIRST_NAME = "Nils", PASSWORD = "", EMAIL = "", USERNAME =  "", TEL_NR = 12, VERIFIED = true, TYPE = ""});
                _context.Users.Add(new User { ID = 1, NAME = "Goebl", FIRST_NAME = "Lukas", PASSWORD = "", EMAIL = "", USERNAME = "", TEL_NR = 12, VERIFIED = true, TYPE = "" });
                _context.Users.Add(new User { ID = 2, NAME = "Nie", FIRST_NAME = "Nico", PASSWORD = "", EMAIL = "", USERNAME = "", TEL_NR = 12, VERIFIED = true, TYPE = "" });
                _context.Users.Add(new User { ID = 3, NAME = "Jchong", FIRST_NAME = "Ching", PASSWORD = "", EMAIL = "", USERNAME = "", TEL_NR = 12, VERIFIED = true, TYPE = "" });
                _context.SaveChanges();
            }
        }
        // GET: api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Todo/5
        [HttpGet("{ID}")]
        public async Task<ActionResult<User>> GetUsers(int ID)
        {
            var user = await _context.Users.FindAsync(ID);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsers), new { id = user.ID }, user);
        }
        [HttpPut("{ID}")]
        public async Task<IActionResult> PutUser(long id, User user)
        {
            if (id != user.ID)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{ID}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}