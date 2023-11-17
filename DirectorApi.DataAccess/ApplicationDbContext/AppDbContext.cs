using DirectorApi.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DirectorApi.DataAccess.ApplicationDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Director> Directors { get; set; }
    }
}
