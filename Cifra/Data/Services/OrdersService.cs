using Cifra.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Cifra.Data.Services
{
public class OrdersService : IOrdersService
    {
        private readonly ApplicationDbContext _appDbContext;
        public OrdersService(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole)
        {


            var orders = await _appDbContext.Orders.Include(order => order.OrderItems)
                                             .ThenInclude(orderItem => orderItem.Product).Include(order => order.User).ToListAsync();

            if (userRole != "Администратор")
                orders = orders.Where(order => order.UserId == userId).ToList();

            return orders;
        }

        public async Task StoreOrderAsync(List<CartItem> items, string userId, string userEmailAddress)
        {
            var order = new Order()
            {
                UserId = userId,
                Email = userEmailAddress,
                OrderItems = items.Select(item => new OrderItems
                {
                    Quantity = item.Quantity,
                    Price = item.Product.Price,
                    ProductId = item.Product.Id
                }).ToList()
            };
            await _appDbContext.Orders.AddAsync(order);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
