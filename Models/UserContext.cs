using Microsoft.EntityFrameworkCore;

namespace KeepThingsAPI.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(user => user.USER_ID);
            base.OnModelCreating(modelBuilder);
        }
    }
}