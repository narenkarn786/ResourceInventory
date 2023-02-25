using Microsoft.EntityFrameworkCore;
using ResourceInventory.Model.CategoryModel;

namespace ResourceInventory.Model
{
    public class ApplicationDBContext:DbContext
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }
        public DbSet<Category> Categories { get; set; } 
       



    }
}
