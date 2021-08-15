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
        }
        public DbSet<User> Users { get; set; }
    }   
}