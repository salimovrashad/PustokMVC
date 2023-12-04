using Microsoft.EntityFrameworkCore;
using PustokMVC.Models;

namespace PustokMVC.Contexts
{
    public class PustokDBContext : DbContext
    {
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Product> Products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-J8RQVMH\\SQLEXPRESS;Database = PustokMVC1;Trusted_Connection=true");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
