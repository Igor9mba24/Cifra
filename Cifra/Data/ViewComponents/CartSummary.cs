using Microsoft.AspNetCore.Mvc;
using Cifra.Data.Cart;

namespace Cifra.Data.ViewComponents
{
    public class CartSummary : ViewComponent
    {
        private readonly ShoppingCart _shoppingCart;

        public CartSummary(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await _shoppingCart.GetCartsAsync();
            return View(items.Count);
        }
    }
}
