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
                _context.UserItems.Add(new UserItem { ITEM_ID = 0,  ITEM_NAME = "Stuhl", ITEM_DEC = "Es ist ein Stuhl", USER_ID = "1", BORROWER = "Lukas", DATE_FROM =  "12:12:12", DATE_TO = "12:12:12"});
                _context.SaveChanges();
            }
        }
        // GET: api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserItem>>> GetUserItems()
        {
            return await _context.UserItems.ToListAsync();
        }

        // GET: api/Todo/5
        [HttpGet("{ITEM_ID}")]
        public async Task<ActionResult<UserItem>> GetUserItem(int id)
        {
            var userItem = await _context.UserItems.FindAsync(id);

            if (userItem == null)
            {
                return NotFound();
            }

            return userItem;
        }
    }
}