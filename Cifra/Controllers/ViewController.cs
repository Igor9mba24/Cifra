using Cifra.Data.Services;
using Cifra.Data.Static;
using Cifra.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Policy;

namespace Cifra.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class ViewController : Controller
    { 
        private readonly IViewsService _viewService;

    public ViewController(IViewsService viewService)
    {
            _viewService = viewService;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var allView = await _viewService.GetAllAsync();
        return View(allView);
    }

        // Get: View/Create
        public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create([Bind("Logo", "Name", "Description")] Models.View view)
    {
        if (!ModelState.IsValid)
            return View(view);

        await _viewService.AddAsync(view);
        return RedirectToAction("Index");
    }

        // Get: View/Details/(id)
        [AllowAnonymous]
    public async Task<IActionResult> Details(int id)
    {
        var viewDetails = await _viewService.GetByIdAsync(id);

        if (viewDetails == null)
            return View("NotFound");

        return View(viewDetails);
    }

        // Get: View/Edit(id)
        public async Task<IActionResult> Edit(int id)
    {
        var viewDetails = await _viewService.GetByIdAsync(id);

        if (viewDetails == null)
            return View("NotFound");
            return View(viewDetails);
        }
        [HttpPost]
    public async Task<IActionResult> Edit(int id, [Bind("Id", "Logo", "Name", "Description")] Models.View view)
    {
        if (!ModelState.IsValid)
            return View(view);

        await _viewService.UpdateAsync(id, view);
        return RedirectToAction("Index");
    }

        // Get: View/Delete(id)
        public async Task<IActionResult> Delete(int id)
    {
        var viewDetails = await _viewService.GetByIdAsync(id);

        if (viewDetails == null)
            return View("NotFound");

        return View(viewDetails);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var viewDetails = await _viewService.GetByIdAsync(id);

        if (viewDetails == null)
            return View("NotFound");

        await _viewService.RemoveAsync(id);

        return RedirectToAction("Index");
    }
}
}
