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
    public class MarketplaceItemController : ControllerBase
    {
        private readonly MarketplaceItemContext _context;

        public MarketplaceItemController(MarketplaceItemContext context)
        {
            _context = context;

            if (_context.MarketplaceItems.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.MarketplaceItems.Add(new MarketplaceItem { ID = 0,  ITEM_NAME = "Stuhl", ITEM_DEC = "Es ist ein Stuhl", USER_ID = "1", BORROWER = "Lukas", DATE_FROM =  "2019-04-04", DATE_TO = "2019-04-04" });
                _context.SaveChanges();
            }
        }
        // GET: api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarketplaceItem>>> GetMarketplaceItemss()
        {
            return await _context.MarketplaceItems.ToListAsync();
        }

        // GET: api/Todo/5
        [HttpGet("{ID}")]
        public async Task<ActionResult<MarketplaceItem>> GetMarketplaceItem(int id)
        {
            var MarketplaceItem = await _context.MarketplaceItems.FindAsync(id);

            if (MarketplaceItem == null)
            {
                return NotFound();
            }

            return MarketplaceItem;
        }
        [HttpPost]
        public async Task<ActionResult<MarketplaceItem>> PostMarketplaceItem(MarketplaceItem marketplaceItem)
        {
            _context.MarketplaceItems.Add(marketplaceItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMarketplaceItem), new { id = marketplaceItem.ID }, marketplaceItem);
        }
        [HttpPut("{ID}")]
        public async Task<IActionResult> PutMarketplaceItem(long id, MarketplaceItem marketplaceItem)
        {
            if (id != marketplaceItem.ID)
            {
                return BadRequest();
            }

            _context.Entry(marketplaceItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{ID}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var marketplaceItem = await _context.MarketplaceItems.FindAsync(id);

            if (marketplaceItem == null)
            {
                return NotFound();
            }

            _context.MarketplaceItems.Remove(marketplaceItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}