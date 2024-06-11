using Microsoft.EntityFrameworkCore;

namespace BethanysPieShop.Models
{
    public class PieRepository : IPieRepository
    {
        private readonly BethanysPieShopContext _bethanysPieShopContext;

        public PieRepository(BethanysPieShopContext bethanysPieShopContext)
        {
            _bethanysPieShopContext = bethanysPieShopContext;
        }

        public IEnumerable<Pie> AllPies
        {
            get { return _bethanysPieShopContext.Pies.Include(c => c.Category); }
        }
        public IEnumerable<Pie> PiesOfTheWeek
        {
            get
            {
                return _bethanysPieShopContext
                    .Pies.Include(c => c.Category)
                    .Where(p => p.IsPieOfTheWeek);
            }
        }

        public Pie? GetPieById(int pieId)
        {
            return _bethanysPieShopContext.Pies.FirstOrDefault(p => p.PieId == pieId);
        }
    }
}
