using Microsoft.EntityFrameworkCore;
using NGAPI.Models;

namespace NGAPI.Data
{
    public class DataContext : DbContext
    {
       public DataContext(DbContextOptions options) : base (options) { }

       public DbSet<AppUser> Users { get; set; }
    }
}
