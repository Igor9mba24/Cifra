using Cifra.Models;
using Microsoft.EntityFrameworkCore;

namespace Cifra.Data.Cart
{
    public class ShoppingCart
    {
        public ApplicationDbContext _appDbContext { get; set; }

        public string CartId { get; set; }
        public List<CartItem> Carts { get; set; }

        public ShoppingCart(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public static ShoppingCart GetCart(IServiceProvider serviceProvider)
        {
            ISession session = serviceProvider.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session ??
                                                throw new InvalidOperationException("HttpContextAccessor or Session не доступен");

            var context = serviceProvider.GetService<ApplicationDbContext>() ??
                                                throw new InvalidOperationException("ApplicationDbContext не доступен");

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { CartId = cartId };
        }

        public async Task AddItemToCartAsync(Product product)
        {
            var CartI = await _appDbContext.CartItems
                                            .FirstOrDefaultAsync(cart => cart.Product.Id == product.Id && cart.CartId == CartId);

            if (CartI == null)
            {
                CartI = new CartItem()
                {
                    CartId = CartId,
                    Product = product,
                    Quantity = 1
                };

                await _appDbContext.CartItems.AddAsync(CartI);
            }
            else
                CartI.Quantity++;

            await _appDbContext.SaveChangesAsync();
        }

        public async Task RemoveItemFromCartAsync(Product product)
        {
            var Cart = await _appDbContext.CartItems
                                .FirstOrDefaultAsync(cart => cart.Product.Id == product.Id && cart.CartId == CartId);

            if (Cart != null)
            {
                if (Cart.Quantity > 1)
                    Cart.Quantity--;
                else
                    _appDbContext.CartItems.Remove(Cart);
            }

            await _appDbContext.SaveChangesAsync();
        }

        public async Task ClearCartAsync()
        {
            var items = await _appDbContext.CartItems
                                            .Where(cart => cart.CartId == CartId).ToListAsync();

            _appDbContext.CartItems.RemoveRange(items);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<CartItem>> GetCartsAsync()
        {
            return Carts ?? (Carts = await _appDbContext.CartItems
                                            .Where(cart => cart.CartId == CartId)
                                            .Include(cart => cart.Product).ToListAsync());
        }

        public double GetCartTotal()
        {
            return _appDbContext.CartItems.Where(cart => cart.CartId == CartId)
                                            .Select(cart => cart.Product.Price * cart.Quantity).Sum();
        }
    }
}


