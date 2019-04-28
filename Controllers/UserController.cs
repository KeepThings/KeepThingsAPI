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
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly KTDBContext _context;
        private SqlConnectionController sql = new SqlConnectionController();

        public UserController(KTDBContext context)
        {
            _context = context;

            //if (_context.Users.Count() == 0)
            //{
                
            //    _context.SaveChanges();
            //}
        }
        
        [HttpGet]
        public string GetUsers()
        {
            return sql.User_getUsers();
        }
        [HttpGet("{id}")]
        public string GetUser(int id)
        {
            return sql.User_getUser(id);
        }
        [HttpPost]
        public string PostUser(User user)
        {
            return sql.User_postUser(user);
        }
        [HttpDelete("{id}")]
        public string DeleteUser(int id)
        {
            return sql.User_deleteUser(id);
        }
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        //{
        //    return await _context.Users.ToListAsync();
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<User>> GetUser(int id)
        //{
        //    var user = await _context.Users.FindAsync(id);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return user;
        //}
        //[HttpPost]
        //public async Task<ActionResult<User>> PostUser(User user)
        //{

        //    _context.Users.Add(user);
        //    await _context.SaveChangesAsync();
        //    var x = user.id;
        //    return CreatedAtAction(nameof(GetUser), new { id = user.id }, user);
        //}
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutUser(int id, User user)
        //{
        //    if (id != user.id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(user).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTodoItem(int id)
        //{
        //    var user = await _context.Users.FindAsync(id);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Users.Remove(user);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}
    }
}