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
        PustokDBContext _db { get; }
        public ProductController(PustokDBContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            var items = await _db.Products.Select(s => new ProductListItemVM
            {
                ImageUrl = s.ImageUrl,
                Title = s.Title,
                Id = s.Id,
                Category = s.Category,
            }).ToListAsync();
            return View(items);
        }
        public IActionResult Create()
        {
            ViewBag.Categories = _db.Categories;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            Product product = new Product
            {
                Title = vm.Title,
                ImageUrl = vm.ImageUrl,
                CategoryId = vm.CategoryId,
            };
            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == null) return BadRequest();
            var data = await _db.Products.FindAsync(id);
            if (data == null) return RedirectToAction(nameof(Index));
            _db.Remove(data);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
