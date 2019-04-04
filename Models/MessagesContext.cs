using Microsoft.EntityFrameworkCore;

namespace KeepThingsAPI.Models
{
    public class MessagesContext : DbContext
    {
        public MessagesContext(DbContextOptions<MessagesContext> options)
            : base(options)
        {
        }

        public DbSet<Messages> Messages { get; set; }
    }
}