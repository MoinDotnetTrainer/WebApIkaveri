using Microsoft.EntityFrameworkCore;

namespace WebApIkaveri.Models
{
    public class AppDb : DbContext
    {
        public AppDb(DbContextOptions<AppDb> options) : base(options)
        {
        }
        public DbSet<Products> Products { get; set; }    
        
        public DbSet<Users> Users { get; set; }     
    }
}
