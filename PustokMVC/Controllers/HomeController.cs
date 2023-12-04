using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokMVC.Contexts;
using PustokMVC.ViewModels;

namespace PustokMVC.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            Class class1 = new Class();
            using PustokDBContext pustokDBContext = new PustokDBContext();
            var sliders = await pustokDBContext.Sliders.ToListAsync();
            var products = await pustokDBContext.Products.ToListAsync();
            class1.sliders = sliders;
            class1.products = products;
            return View(class1);
        }
    }
}
