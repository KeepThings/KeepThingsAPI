using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeepThingsAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace KeepThingsAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserItemController : ControllerBase
    {
        private readonly KTDBContext _context;
        private SqlConnectionController sql = new SqlConnectionController();

        public UserItemController(KTDBContext context)
        {
            _context = context;

            //if (_context.UserItems.Count() == 0)
            //{   
            //    _context.SaveChanges();
            //}
        }

        [HttpGet]
        public string GetUserItems()
        {
            return sql.UserItem_getUserItems();
        }
        [HttpGet("{id}")]
        public string GetUserItem(int id)
        {
            return sql.UserItem_getUserItem(id);
        }
        [HttpPost]
        public string PostUserItem(UserItem item)
        {
            return sql.UserItem_postUserItem(item);
        }
        [HttpDelete("{id}")]
        public string DeleteUserItem(int id)
        {
            return sql.UserItem_deleteUserItem(id);
        }
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<UserItem>>> GetUserItem()
        //{
        //    return await _context.UserItems.ToListAsync();
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<UserItem>> GetUserItem(int id)
        //{
        //    var useritemItem = await _context.UserItems.FindAsync(id);

        //    if (useritemItem == null)
        //    {
        //        return NotFound();
        //    }

        //    return useritemItem;
        //}
        //[HttpPost]
        //public async Task<ActionResult<UserItem>> PostUserItem(UserItem useritem)
        //{
        //    _context.UserItems.Add(useritem);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(nameof(GetUserItem), new { id = useritem.id }, useritem);
        //}
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutUserItem(int id, UserItem useritem)
        //{
        //    if (id != useritem.id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(useritem).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTodoItem(int id)
        //{
        //    var useritem = await _context.UserItems.FindAsync(id);

        //    if (useritem == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.UserItems.Remove(useritem);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

    }
}