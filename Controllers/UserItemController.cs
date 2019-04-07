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
    public class UserItemController : ControllerBase
    {
        private readonly UserItemContext _context;

        public UserItemController(UserItemContext context)
        {
            _context = context;

            if (_context.UserItems.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.UserItems.Add(new UserItem { ID = 0,  ITEM_NAME = "Stuhl", ITEM_DEC = "Es ist ein Stuhl", USER_ID = "1", BORROWER = "Lukas", DATE_FROM =  "2019-04-04", DATE_TO = "2019-04-04"});
                _context.SaveChanges();
            }
        }
        // GET: api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserItem>>> GetUserItem()
        {
            return await _context.UserItems.ToListAsync();
        }

        // GET: api/Todo/5
        [HttpGet("{ID}")]
        public async Task<ActionResult<UserItem>> GetUserItem(int id)
        {
            var useritemItem = await _context.UserItems.FindAsync(id);

            if (useritemItem == null)
            {
                return NotFound();
            }

            return useritemItem;
        }
        [HttpPost]
        public async Task<ActionResult<UserItem>> PostUserItem(UserItem useritem)
        {
            _context.UserItems.Add(useritem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserItem), new { id = useritem.ID }, useritem);
        }
        [HttpPut("{ID}")]
        public async Task<IActionResult> PutUserItem(long id, UserItem useritem)
        {
            if (id != useritem.ID)
            {
                return BadRequest();
            }

            _context.Entry(useritem).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{ID}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var useritem = await _context.UserItems.FindAsync(id);

            if (useritem == null)
            {
                return NotFound();
            }

            _context.UserItems.Remove(useritem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
    }
}