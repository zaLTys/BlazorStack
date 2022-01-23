using BlazorStack.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorStack.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) :base(options)
        {

        }

        public DbSet<Unit> Units { get; set; }
    }
}
