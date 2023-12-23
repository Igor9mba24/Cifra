using Cifra.Data.Services;
using Cifra.Data.Static;
using Cifra.Data.ViewModels;
using Cifra.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cifra.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult NotFound()
        {
            return View("NotFound");
        }

        private async Task SetProductDropdownsAsync()
        {
            var productDropdownsData = await _productService.GetNewProductDropdownsValuesAsync();

            ViewBag.CountryId = new SelectList(productDropdownsData.Countrys, "Id", "Name");
            ViewBag.ViewId = new SelectList(productDropdownsData.Views, "Id", "Name");
            ViewBag.ManufacturerId = new SelectList(productDropdownsData.Manufacturers, "Id", "FullName");
        }

        private NewProductVM MapProductDetailsToNewProductVM(Product productDetails)
        {
            return new NewProductVM()
            {
                Id = productDetails.Id,
                ImageURL = productDetails.ImageURL,
                Name = productDetails.Name,
                Description = productDetails.Description,
                Price = productDetails.Price,
                ReleaseDate = productDetails.ReleaseDate,
                Category = productDetails.Category,

                CountryId = productDetails.CountryId,
                ViewId = productDetails.ViewId,
                ManufacturerId = productDetails.Manufacturer_Product.Select(productDeveloper => productDeveloper.ManufacturerId).ToList()
            };
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allProduct = await _productService.GetAllAsync(product => product.country);
            return View(allProduct);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allProduct = await _productService.GetAllAsync(product => product.country);

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                var filteredResult = allProduct.Where(product => product.Name.ToLower().Contains(searchString) ||
                                                    product.Description.ToLower().Contains(searchString)).ToList();

                if (filteredResult.Any())
                    return View("Index", filteredResult);
            }

            return View("ProductNotFound");
        }

        // Get: Product/Create
        public async Task<IActionResult> Create()
        {
            await SetProductDropdownsAsync();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewProductVM newProduct)
        {
            if (!ModelState.IsValid)
            {
                await SetProductDropdownsAsync();
                return View(newProduct);
            }

            await _productService.AddNewProductAsync(newProduct);
            return RedirectToAction("Index");
        }

        // Get: Product/Details/(id)
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var productDetails = await _productService.GetProductByIdAsync(id);

            if (productDetails == null)
                return View("NotFound");

            return View(productDetails);
        }

        // Get: Product/Edit/(id)
        public async Task<IActionResult> Edit(int id)
        {
            var productDetails = await _productService.GetProductByIdAsync(id);

            if (productDetails == null)
                return View("NotFound");

            var response = MapProductDetailsToNewProductVM(productDetails);

            await SetProductDropdownsAsync();

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewProductVM newProduct)
        {
            if (!ModelState.IsValid)
            {
                await SetProductDropdownsAsync();
                return View(newProduct);
            }

            await _productService.UpdateProductAsync(newProduct);
            return RedirectToAction("Index");
        }

        // Get: Product/Delete/(id)
        public async Task<IActionResult> Delete(int id)
        {
            var productDetails = await _productService.GetProductByIdAsync(id);

            if (productDetails == null)
                return View("NotFound");

            var response = MapProductDetailsToNewProductVM(productDetails);

            await SetProductDropdownsAsync();

            return View(response);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productDetails = await _productService.GetProductByIdAsync(id);

            if (productDetails == null)
                return View("NotFound");

            await _productService.RemoveProductAsync(id);

            return RedirectToAction("Index");
        }
    }
}
