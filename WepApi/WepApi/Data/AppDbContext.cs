using Domain;
using Microsoft.EntityFrameworkCore;

namespace WepApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Entity> Entities { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }
    }
}
