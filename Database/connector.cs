using Microsoft.EntityFrameworkCore;
using FlagApi.Models;
namespace FlagApi.Database
{
    public class connector : DbContext {
        public connector()
        {
        }
        // Entities        
        public DbSet<User> Users { get; set; }
    }
}