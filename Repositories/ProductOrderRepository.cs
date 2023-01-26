using Sklep_MVC_Projekt.Data;
using Sklep_MVC_Projekt.Models;

namespace Sklep_MVC_Projekt.Repositories
{
    public class ProductOrderRepository : BaseRepository<ProductOrder>
    {
        private DataContext _dataContext;

        public ProductOrderRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
    }
}
