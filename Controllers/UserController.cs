using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeepThingsAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;

namespace KeepThingsAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly KTDBContext _context;
        //private SqlConnectionController sql = new SqlConnectionController();

        public UserController(KTDBContext context)
        {
            _context = context;

            //if (_context.Users.Count() == 0)
            //{
                
            //    _context.SaveChanges();
            //}
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet("{Auth_id}")]
        public async Task<ActionResult<User>> GetUser(string Auth_id)
        {
            //var user = await _context.Users.FindAsync(Auth_id);
            List<User> user = _context.Users.ToList();
            if (user == null)
            {
                return NotFound();
            }

            return user.Find(item => item.Auth0_id == Auth_id);
        }
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            var x = user.id;
            return CreatedAtAction(nameof(GetUser), new { Auth_id = user.Auth0_id }, user);
        }
        [HttpPut("{Auth_id}")]
        public async Task<IActionResult> PutUser(string Auth_id, User user)
        {
            if (!Auth_id.Equals(user.Auth0_id))
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent(); // TODO return the User and correct http status(200)
        }
        [HttpDelete("{Auth_id}")]
        public async Task<IActionResult> DeleteTodoItem(string Auth_id)
        {
            //var user = await _context.Users.FindAsync(Auth_id);
            List<User> user = _context.Users.ToList();

            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user.Find(item => item.Auth0_id == Auth_id));
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}