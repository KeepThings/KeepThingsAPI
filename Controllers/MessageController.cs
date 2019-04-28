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
    public class MessageController : ControllerBase
    {
        private readonly KTDBContext _context;
        private SqlConnectionController sql = new SqlConnectionController();

        public MessageController(KTDBContext context)
        {
            _context = context;

            //if (_context.Messages.Count() == 0)
            //{
            //    _context.SaveChanges();
            //}
        }
        [HttpGet("{id}")]
        public string GetMessage(int id)
        {
            return sql.Message_getMessages(id);
        }
        [HttpPost]
        public string PostMessage(Message message)
        {
            return sql.Message_postMessages(message);
        }
        [HttpDelete("{id}")]
        public string DeleteMessage(int id)
        {
            return sql.Message_deleteMessage(id);
        }
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Message>>> GetMessages()
        //{
        //    return await _context.Messages.ToListAsync();
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Message>> GetMessage(int id)
        //{
        //    var Message = await _context.Messages.FindAsync(id);

        //    if (Message == null)
        //    {
        //        return NotFound();
        //    }

        //    return Message;
        //}
        //[HttpPost]
        //public async Task<ActionResult<Message>> PostMessage(Message message)
        //{
        //    _context.Messages.Add(message);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(nameof(GetMessage), new { id = message.id }, message);
        //}
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutMessage(int id, Message message)
        //{
        //    if (id != message.id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(message).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTodoItem(int id)
        //{
        //    var message = await _context.Messages.FindAsync(id);

        //    if (message == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Messages.Remove(message);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}
    }
}