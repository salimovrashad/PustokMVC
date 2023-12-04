using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokMVC.Contexts;
using PustokMVC.Models;
using PustokMVC.ViewModels.SliderVM;
using System.Drawing;

namespace PustokMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        PustokDBContext _db { get; }
        public SliderController(PustokDBContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            var items = await _db.Sliders.Select(s => new SliderListItemVM
            {
                ImageUrl = s.ImageUrl,
                Title = s.Title,
                Description = s.Description,
                IsLeft = s.IsLeft,
                Id = s.Id,
            }).ToListAsync();
            return View(items);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SliderCreateVM vm)
        {
            if (vm.Position < -1 || vm.Position > 1)
            {
                ModelState.AddModelError("Position", "Invalid Input");
            }
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            Slider slider = new Slider
            {
                Title = vm.Title,
                Description = vm.Description,
                ImageUrl = vm.ImageUrl,
                IsLeft = vm.Position switch
                {
                    0 => null,
                    -1 => true,
                    1 => false
                }
            };
            await _db.Sliders.AddAsync(slider);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof (Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            TempData["Response"] = false;
            if (id == null) return BadRequest();
            var data = await _db.Sliders.FindAsync(id);
            if (data == null) return RedirectToAction(nameof(Index));
            _db.Remove(data);
            await _db.SaveChangesAsync();
            TempData["Response"] = true;
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            if (id == null) return BadRequest();
            var data = await _db.Sliders.FindAsync(id);
            if (data == null) return RedirectToAction(nameof(Index));
            return View(new SliderUpdateVM
            {
                ImageUrl = data.ImageUrl,
                Position = data.IsLeft switch
                {
                    true => -1,
                    null => 0,
                    false => 1,
                },
                Description = data.Description,
                Title = data.Title
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, SliderUpdateVM vm)
        {
            if (id == null) return BadRequest();
            if (vm.Position < -1 || vm.Position > 1)
            {
                ModelState.AddModelError("Position", "Invalid Input");
            }
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var data = await _db.Sliders.FindAsync(id);
            if (data == null) return NotFound();
            data.ImageUrl = vm.ImageUrl;
            data.Title = vm.Title;
            data.Description = vm.Description;
            data.IsLeft = vm.Position switch
            {
                0 => null,
                -1 => true,
                1 => false
            };
            await _db.SaveChangesAsync();
            TempData["Response"] = true;
            return RedirectToAction(nameof(Index));
        }
    }
}
