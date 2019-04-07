using Microsoft.EntityFrameworkCore;

namespace KeepThingsAPI.Models
{
    public class UserItemContext : DbContext
    {
        public UserItemContext(DbContextOptions<UserItemContext> options)
            : base(options)
        {
        }

        public DbSet<UserItem> UserItems { get; set; }
       
    }
}