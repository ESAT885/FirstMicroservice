using FirstMicroService.Categories.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstMicroService.Categories.WebAPI.Context
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
    }
}
