using Microsoft.EntityFrameworkCore;
using MiddlewareApi.Models;

namespace MiddlewareApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Participant> Participants { get; set; }
    }
}
