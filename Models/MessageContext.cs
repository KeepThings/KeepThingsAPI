using Microsoft.EntityFrameworkCore;

namespace KeepThingsAPI.Models
{
    public class MessageContext : DbContext
    {
        public MessageContext(DbContextOptions<MessageContext> options)
            : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Message>().HasKey(x => x.MESSAGE_ID);
        base.OnModelCreating(modelBuilder);
    }
    }
}