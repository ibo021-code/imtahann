using Microsoft.EntityFrameworkCore;
using Strategy.Models;

namespace Strategy.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Employee> employees { get; set; }
       
    }
    
}
