using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokMVC.Contexts;

namespace PustokMVC.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            using PustokDBContext pustokDBContext = new PustokDBContext();
            var sliders = await pustokDBContext.Sliders.ToListAsync();
            return View(sliders);
        }
    }
}
