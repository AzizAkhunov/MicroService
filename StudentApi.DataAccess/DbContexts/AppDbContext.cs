using Microsoft.EntityFrameworkCore;
using StudentApi.Domain.Models;


namespace StudentApi.DataAccess.DbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Student> Students { get; set; }
    }
}
