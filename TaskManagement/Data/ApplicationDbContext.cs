using Microsoft.EntityFrameworkCore;
using TaskManagement.Model.Entity;

namespace TaskManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<TaskManage> Tasks { get; set; }
    }
}
