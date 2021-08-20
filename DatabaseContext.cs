using Microsoft.EntityFrameworkCore;
using FlagApi.Models;
namespace FlagApi
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {}
        protected override void OnModelCreating(ModelBuilder modelBuilder)  
        {  
            modelBuilder.Entity<User>().ToTable("users");  
            modelBuilder.Entity<Message>().ToTable("messages");  
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
    }   
}