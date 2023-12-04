using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokMVC.Contexts;
using PustokMVC.Models;
using PustokMVC.ViewModels.ProductVM;
using PustokMVC.ViewModels.SliderVM;

namespace PustokMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        public async Task<IActionResult> Index()
        {
            using PustokDBContext pustokDBContext = new PustokDBContext();
            var items = await pustokDBContext.Products.Select(s => new ProductListItemVM
            {
                ImageUrl = s.ImageUrl,
                Title = s.Title,
                Id = s.Id,
            }).ToListAsync();
            return View(items);
        }
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            using PustokDBContext pustokDBContext = new PustokDBContext();
            Product product = new Product
            {
                Title = vm.Title,
                ImageUrl = vm.ImageUrl,
            };
            await pustokDBContext.Products.AddAsync(product);
            await pustokDBContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == null) return BadRequest();
            using PustokDBContext pustokDBContext = new();
            var data = await pustokDBContext.Products.FindAsync(id);
            if (data == null) return RedirectToAction(nameof(Index));
            pustokDBContext.Remove(data);
            await pustokDBContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
