using FirstMicroService.Todos.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstMicroService.Todos.Context
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options):base(options)
        {
            
        }
        public DbSet<Todo> Todos { get; set; }
    }
}
