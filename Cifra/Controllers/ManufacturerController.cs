using Cifra.Data.Services;
using Cifra.Data.Static;
using Cifra.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cifra.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class ManufacturerController : Controller


    {
        private readonly IManufacturersService _manufacturerService;

        public ManufacturerController(IManufacturersService manufacturersService)
        {
            _manufacturerService = manufacturersService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allManufacturers = await _manufacturerService.GetAllAsync();
            return View(allManufacturers);
        }

        // Get: Developers/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProfilePictureURL", "FullName", "Biography")] Manufacturer manufacturer)
        {
            if (!ModelState.IsValid)
                return View(manufacturer);

            await _manufacturerService.AddAsync(manufacturer);
            return RedirectToAction("Index");
        }

        // Get: Developers/Details/(id)
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var manufacturerDetails = await _manufacturerService.GetByIdAsync(id);

            if (manufacturerDetails == null)
                return View("NotFound");

            return View(manufacturerDetails);
        }

        // Get: Developers/Edit(id)
        public async Task<IActionResult> Edit(int id)
        {
            var manufacturerDetails = await _manufacturerService.GetByIdAsync(id);

            if (manufacturerDetails == null)
                return View("NotFound");

            return View(manufacturerDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id", "ProfilePictureURL", "FullName", "Biography")] Manufacturer manufacturer)
        {
            if (!ModelState.IsValid)
                return View(manufacturer);

            await _manufacturerService.UpdateAsync(id, manufacturer);
            return RedirectToAction("Index");
        }

        // Get: Developers/Delete(id)
        public async Task<IActionResult> Delete(int id)
        {
            var manufacturerDetails = await _manufacturerService.GetByIdAsync(id);

            if (manufacturerDetails == null)
                return View("NotFound");

            return View(manufacturerDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var manufacturerDetails = await _manufacturerService.GetByIdAsync(id);

            if (manufacturerDetails == null)
                return View("NotFound");

            await _manufacturerService.RemoveAsync(id);

            return RedirectToAction("Index");
        }
    }
}

