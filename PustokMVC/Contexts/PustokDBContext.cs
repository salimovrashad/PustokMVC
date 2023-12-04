using Microsoft.EntityFrameworkCore;
using PustokMVC.Models;

namespace PustokMVC.Contexts
{
    public class PustokDBContext : DbContext
    {
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public PustokDBContext(DbContextOptions opt) : base(opt){}
    }
}
