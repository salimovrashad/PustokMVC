using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokMVC.Contexts;
using PustokMVC.ViewModels;

namespace PustokMVC.Controllers
{
    public class HomeController : Controller
    {
        PustokDBContext _db { get; }
        public HomeController(PustokDBContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            Class class1 = new Class();
            var sliders = await _db.Sliders.ToListAsync();
            var products = await _db.Products.ToListAsync();
            class1.sliders = sliders;
            class1.products = products;
            return View(class1);
        }
    }
}
