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
                _context.MarketplaceItems.Add(new MarketplaceItem { ITEM_ID = 0,  ITEM_NAME = "Stuhl", ITEM_DEC = "Es ist ein Stuhl", USER_ID = "1", BORROWER = "Lukas", DATE_FROM =  "2019-04-04", DATE_TO = "2019-04-04" });
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
        [HttpGet("{ITEM_ID}")]
        public async Task<ActionResult<MarketplaceItem>> GetMarketplaceItem(int id)
        {
            var MarketplaceItem = await _context.MarketplaceItems.FindAsync(id);

            if (MarketplaceItem == null)
            {
                return NotFound();
            }

            return MarketplaceItem;
        }
    }
}