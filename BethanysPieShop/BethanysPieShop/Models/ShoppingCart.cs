using Microsoft.EntityFrameworkCore;

namespace BethanysPieShop.Models
{
    public class ShoppingCart : IShoppingCart
    {
        private readonly BethanysPieShopContext _bethanysPieShopContext;
        public string? ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; } = default!;
        public ShoppingCart(BethanysPieShopContext bethanysPieShopContext)
        {
            _bethanysPieShopContext = bethanysPieShopContext;
        }
        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;

            BethanysPieShopContext context = services.GetService<BethanysPieShopContext>() ?? throw new Exception("Error initializing");

            string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();

            session?.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddToCart(Pie pie)
        {
            var shoppingCartItem = _bethanysPieShopContext.ShoppingCartItems.SingleOrDefault(
                               s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Pie = pie,
                    Amount = 1
                };

                _bethanysPieShopContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _bethanysPieShopContext.SaveChanges();
        }

        public int RemoveFromCart(Pie pie)
        {
            var shoppingCartItem = _bethanysPieShopContext.ShoppingCartItems.SingleOrDefault(
                                              s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _bethanysPieShopContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _bethanysPieShopContext.SaveChanges();

            return localAmount;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??= _bethanysPieShopContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                      .Include(s => s.Pie)
                      .ToList();
        }

        public void ClearCart()
        {
            var cartItems = _bethanysPieShopContext
                .ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _bethanysPieShopContext.ShoppingCartItems.RemoveRange(cartItems);

            _bethanysPieShopContext.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _bethanysPieShopContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Pie.Price * c.Amount).Sum();
            return total;
        }   
    }
}
