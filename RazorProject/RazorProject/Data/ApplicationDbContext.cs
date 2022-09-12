using Microsoft.EntityFrameworkCore;
using RazorProject.Model;

namespace RazorProject.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        public DbSet<Category> categories { get; set; }
    }
}
