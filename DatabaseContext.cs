using Microsoft.EntityFrameworkCore;
using FlagApi.Models;
namespace FlagApi
{
    public class DatabaseContext : DbContext
    {               
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {                             
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)  
        {            
            modelBuilder.Entity<User>().ToTable("users");  
            modelBuilder.Entity<Message>().ToTable("messages");            

            modelBuilder.Entity<Message>()
                .HasOne(x => x.Author)
                .WithMany(x => x.MessagesSent)
                .HasForeignKey(x => x.AuthorId);         

            modelBuilder.Entity<Message>()
                .HasOne(x => x.Recipient)
                .WithMany(x => x.MessagesReceived)
                .HasForeignKey(x => x.RecipientId);         
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
    }   
}