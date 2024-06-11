namespace BethanysPieShop.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BethanysPieShopContext _bethanysPieShopContext;

        public CategoryRepository(BethanysPieShopContext bethanysPieShopContext)
        {
            _bethanysPieShopContext = bethanysPieShopContext;
        }

        public IEnumerable<Category> AllCategories =>
            _bethanysPieShopContext.Categories.OrderBy(c => c.CategoryName);
    }
}
