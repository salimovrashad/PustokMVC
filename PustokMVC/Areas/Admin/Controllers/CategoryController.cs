using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokMVC.Contexts;
using PustokMVC.Models;
using PustokMVC.ViewModels.CategoryVM;
using PustokMVC.ViewModels.ProductVM;
using PustokMVC.ViewModels.SliderVM;

namespace PustokMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        PustokDBContext _db { get; }

        public CategoryController(PustokDBContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _db.Categories.Select(c => new CategoryListItemVM
            {
                Id = c.Id,
                Name = c.Name,
            }).ToListAsync();
            return View(items);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            Category category = new Category
            {
                Name = vm.Name
            };
            await _db.Categories.AddAsync(category);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == null) return BadRequest();
            var data = await _db.Categories.FindAsync(id);
            if (data == null) return RedirectToAction(nameof(Index));
            _db.Remove(data);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            if (id == null) return BadRequest();
            var data = await _db.Categories.FindAsync(id);
            if (data == null) return RedirectToAction(nameof(Index));
            return View(new CategoryUpdateVM
            {
                Name = data.Name
            });
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, CategoryUpdateVM vm)
        {
            if (id == null) return BadRequest();
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var data = await _db.Categories.FindAsync(id);
            if (data == null) return NotFound();
            data.Name = vm.Name;
            await _db.SaveChangesAsync();
            TempData["Response"] = true;
            return RedirectToAction(nameof(Index));
        }
    }
}
