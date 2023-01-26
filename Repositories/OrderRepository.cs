using Sklep_MVC_Projekt.Data;
using Sklep_MVC_Projekt.Models;

namespace Sklep_MVC_Projekt.Repositories
{
    public class OrderRepository : BaseRepository<Order>
    {
        private DataContext _dataContext;

        public OrderRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
    }
}
