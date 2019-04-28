using Microsoft.EntityFrameworkCore;

namespace KeepThingsAPI.Models
{
    public class KTDBContext : DbContext
    {
        public KTDBContext(DbContextOptions<KTDBContext> options)
            : base(options)
        {
        }

        public DbSet<MarketplaceItem> MarketplaceItems { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserItem> UserItems { get; set; }
        public DbSet<Chat> Chat { get; set; }

    }
}