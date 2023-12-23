using Cifra.Models;

namespace Cifra.Data.Services
{
    public interface IOrdersService
    {
        Task StoreOrderAsync(List<CartItem> items, string userId, string userEmailAddress);
        Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole);
    }
}
