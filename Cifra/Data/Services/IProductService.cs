using Cifra.Data.Base;
using Cifra.Data.ViewModels;
using Cifra.Models;

namespace Cifra.Data.Services
{
    public interface IProductService : IEntityBaseRepository<Product>
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<NewProductDropdownsVM> GetNewProductDropdownsValuesAsync();
        Task AddNewProductAsync(NewProductVM newProductVM);
        Task UpdateProductAsync(NewProductVM newProductVM);
        Task RemoveProductAsync(int id);
    }
}
