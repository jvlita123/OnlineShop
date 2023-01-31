using Sklep_MVC_Projekt.Data;
using Sklep_MVC_Projekt.Models;

namespace Sklep_MVC_Projekt.Repositories
{
    public class ProductRepository : BaseRepository<Product>
    {
        private DataContext _dataContext;

        public ProductRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }

        public List<Product> GetNewAndPromoProducts()
        {
            return _dataContext.Product.Where(e => e.IsNew || e.IsOnSale).ToList();
        }
    }
}
