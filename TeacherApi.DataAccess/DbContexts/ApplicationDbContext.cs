using Microsoft.EntityFrameworkCore;
using TeacherApi.Domain.Models;

namespace TeacherApi.DataAccess.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Teacher> Teachers { get; set; }
    }
}
