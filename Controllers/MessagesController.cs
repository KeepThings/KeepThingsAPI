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
    public class MessagesController : ControllerBase
    {
        private readonly MessagesContext _context;

        public MessagesController(MessagesContext context)
        {
            _context = context;

            if (_context.Messages.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.Messages.Add(new Messages { MESSAGE_ID = 0, SENDER_ID = 0, RECEIVER_ID = 1, HEADER = "Super wichtige Nachricht", MESSAGE = "Wie geht es ihnen den heute ?", TIMESTAMP = "12:12:12" });
                _context.SaveChanges();
            }
        }
        // GET: api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Messages>>> GetMessages()
        {
            return await _context.Messages.ToListAsync();
        }

        // GET: api/Todo/5
        [HttpGet("{MESSAGE_ID}")]
        public async Task<ActionResult<Messages>> GetMessage(int id)
        {
            var Message = await _context.Messages.FindAsync(id);

            if (Message == null)
            {
                return NotFound();
            }

            return Message;
        }
    }
}