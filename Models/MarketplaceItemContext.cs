using Microsoft.EntityFrameworkCore;

namespace KeepThingsAPI.Models
{
    public class MarketplaceItemContext : DbContext
    {
        public MarketplaceItemContext(DbContextOptions<MarketplaceItemContext> options)
            : base(options)
        {
        }

        public DbSet<MarketplaceItem> MarketplaceItems { get; set; }
    }
}