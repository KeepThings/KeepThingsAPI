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
    public class ChatController : Controller
    {
        private readonly KTDBContext _context;
        //private SqlConnectionController sql = new SqlConnectionController();
        public ChatController(KTDBContext context)
        {
            _context = context;

            //if (_context.Chat.Count() == 0)
            //{
            //    _context.SaveChanges();
            //}
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chat>>> GetChats()
        {
            return await _context.Chat.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Chat>>> GetChat(int id)
        {
            //var chat = await _context.Chat.FindAsync(id);
            List<Chat> chat = _context.Chat.ToList();
            List<Chat> result = new List<Chat>();
            if (chat == null)
            {
                return NotFound();
            }
            foreach(Chat tempChat in chat)
            {
                if (tempChat.receiver_id == id || tempChat.sender_id == id) result.Add(tempChat);
            }


            return result;
        }
        [HttpPost]
        public async Task<ActionResult<Chat>> PostChat(Chat chat)
        {

            _context.Chat.Add(chat);
            await _context.SaveChangesAsync();
            var x = chat.id;
            return CreatedAtAction(nameof(GetChat), new { id = chat.id }, chat);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChat(int id, Chat chat)
        {
            if (id != chat.id)
            {
                return BadRequest();
            }

            _context.Entry(chat).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            var chat = await _context.Chat.FindAsync(id);

            if (chat == null)
            {
                return NotFound();
            }

            _context.Chat.Remove(chat);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
