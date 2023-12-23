using Cifra.Data.Services;
using Cifra.Data.Static;
using Cifra.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace Cifra.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class CountryController : Controller
 
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allCountrys = await _countryService.GetAllAsync();
            return View(allCountrys);
        }

        // Get: Platforms/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Logo", "Name", "URL", "Description")] Country country)
        {
            if (!ModelState.IsValid)
                return View(country);

            await _countryService.AddAsync(country);
            return RedirectToAction("Index");
        }

        // Get: Platforms/Details/(id)
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var CountryDetails = await _countryService.GetByIdAsync(id);

            if (CountryDetails == null)
                return View("NotFound");

            return View(CountryDetails);
        }

        // Get: Platforms/Edit(id)
        public async Task<IActionResult> Edit(int id)
        {
            var CountryDetails = await _countryService.GetByIdAsync(id);

            if (CountryDetails == null)
                return View("NotFound");

            return View(CountryDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id", "Logo", "Name", "URL", "Description")] Country country)
        {
            if (!ModelState.IsValid)
                return View(country);

            await _countryService.UpdateAsync(id, country);
            return RedirectToAction("Index");
        }

        // Get: Platforms/Delete(id)
        public async Task<IActionResult> Delete(int id)
        {
            var CountryDetails = await _countryService.GetByIdAsync(id);

            if (CountryDetails == null)
                return View("NotFound");

            return View(CountryDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var CountryDetails = await _countryService.GetByIdAsync(id);

            if (CountryDetails == null)
                return View("NotFound");

            await _countryService.RemoveAsync(id);

            return RedirectToAction("Index");
        }
    }
}
