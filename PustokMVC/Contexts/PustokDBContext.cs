using Microsoft.EntityFrameworkCore;
using PustokMVC.Models;

namespace PustokMVC.Contexts
{
    public class PustokDBContext : DbContext
    {
        public DbSet<Slider> Sliders { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-NR4KVRV\\SQLEXPRESS;Database = PustokMVC;Trusted_Connection=true");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
