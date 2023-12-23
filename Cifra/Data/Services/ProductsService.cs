using Cifra.Data.Base;
using Cifra.Data.ViewModels;
using Cifra.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Cifra.Data.Services
{
    public class ProductsService : EntityBaseRepository<Product>, IProductService
    {
        private readonly ApplicationDbContext _appDbContext;
        public ProductsService(ApplicationDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddNewProductAsync(NewProductVM newProductVM)
        {
            var newProduct = new Product()
            {
                ImageURL = newProductVM.ImageURL,
                Name = newProductVM.Name,
                Description = newProductVM.Description,
                Price = newProductVM.Price,
                ReleaseDate = newProductVM.ReleaseDate,
                Category = newProductVM.Category,

                CountryId = newProductVM.CountryId,
                ViewId = newProductVM.ViewId,
            };

            await _appDbContext.AddAsync(newProduct);
            await _appDbContext.SaveChangesAsync();

            // Add Product Manufacturer
            foreach (var manufacturerId in newProductVM.ManufacturerId)
            {
                var newManufacturerProduct = new Manufacturer_Product()
                {
                    ProductId = newProduct.Id,
                    ManufacturerId = manufacturerId
                };
                await _appDbContext.AddAsync(newManufacturerProduct);
            }
            await _appDbContext.SaveChangesAsync();
        }

        public async Task RemoveProductAsync(int id)
        {
            var productDetails = await _appDbContext.Products.FirstOrDefaultAsync(product => product.Id == id);

            if (productDetails != null)
            {
                _appDbContext.Products.Remove(productDetails);

                // Remove any associated records in the Developer_Game table
                var manufacturerProduct = await _appDbContext.Manufacturers_Products.Where(manproduct => manproduct.ProductId == id).ToListAsync();
                _appDbContext.Products.RemoveRange(productDetails);

                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var productDetails = await _appDbContext.Products
                .Include(product => product.country)
                .Include(product => product.View)
                .Include(product => product.Manufacturer_Product).ThenInclude(product => product.Manufacturer)
                .FirstOrDefaultAsync(product => product.Id == id);

            return productDetails;
        }

        public async Task<NewProductDropdownsVM> GetNewProductDropdownsValuesAsync()
        {
            var response = new NewProductDropdownsVM()
            {
                Manufacturers = await _appDbContext.Manufacturers.OrderBy(manufacturer => manufacturer.FullName).ToListAsync(),
                Countrys = await _appDbContext.Countries.OrderBy(country => country.Name).ToListAsync(),
                Views = await _appDbContext.Views.OrderBy(view => view.Name).ToListAsync()
            };
            return response;
        }

        public async Task UpdateProductAsync(NewProductVM newProductVM)
        {
            var dbProduct = await _appDbContext.Products.FirstOrDefaultAsync(product => product.Id == newProductVM.Id);

            if (dbProduct != null)
            {
                dbProduct.ImageURL = newProductVM.ImageURL;
                dbProduct.Name = newProductVM.Name;
                dbProduct.Description = newProductVM.Description;
                dbProduct.Price = newProductVM.Price;
                dbProduct.ReleaseDate = newProductVM.ReleaseDate;
                dbProduct.Category = newProductVM.Category;
                dbProduct.CountryId = newProductVM.CountryId;
                dbProduct.ViewId = newProductVM.ViewId;

                // Update any associated records in the Developer_Game table
                var manufacturersProducts = await _appDbContext.Manufacturers_Products.Where(manproduct => manproduct.ProductId == newProductVM.Id).ToListAsync();
                _appDbContext.Manufacturers_Products.RemoveRange(manufacturersProducts);

                foreach (var manufacturersId in newProductVM.ManufacturerId)
                {
                    var newManufacturerProduct = new Manufacturer_Product()
                    {
                        ProductId = dbProduct.Id,
                        ManufacturerId = manufacturersId
                    };
                    await _appDbContext.AddAsync(newManufacturerProduct);
                }
            }
            await _appDbContext.SaveChangesAsync();
        }
    }
}
