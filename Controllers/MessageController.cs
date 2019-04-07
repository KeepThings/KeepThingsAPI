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
    public class MessageController : ControllerBase
    {
        private readonly MessageContext _context;

        public MessageController(MessageContext context)
        {
            _context = context;

            if (_context.Messages.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.Messages.Add(new Message { ID = 0, SENDER_ID = 0, RECEIVER_ID = 1, HEADER = "Super wichtige Nachricht", MESSAGE = "Wie geht es ihnen den heute ?", TIMESTAMP = "12:12:12" });
                _context.SaveChanges();
            }
        }
        // GET: api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages()
        {
            return await _context.Messages.ToListAsync();
        }

        // GET: api/Todo/5
        [HttpGet("{ID}")]
        public async Task<ActionResult<Message>> GetMessage(int id)
        {
            var Message = await _context.Messages.FindAsync(id);

            if (Message == null)
            {
                return NotFound();
            }

            return Message;
        }
        [HttpPost]
        public async Task<ActionResult<Message>> PostMessage(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMessage), new { id = message.ID }, message);
        }
        [HttpPut("{ID}")]
        public async Task<IActionResult> PutMessage(long id, Message message)
        {
            if (id != message.ID)
            {
                return BadRequest();
            }

            _context.Entry(message).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{ID}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var message = await _context.Messages.FindAsync(id);

            if (message == null)
            {
                return NotFound();
            }

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}