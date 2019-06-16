using Microsoft.EntityFrameworkCore;

namespace KeepThingsAPI.Models
{
    public class KTDBContext : DbContext
    {
        public KTDBContext(DbContextOptions<KTDBContext> options)
            : base(options)
        {
        }

        public DbSet<MarketplaceItem> Marketplace_Items { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserItem> User_Items { get; set; }
        public DbSet<Chat> Chat { get; set; }

    }
}