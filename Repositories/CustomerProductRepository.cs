using Sklep_MVC_Projekt.Data;
using Sklep_MVC_Projekt.Models;

namespace Sklep_MVC_Projekt.Repositories
{
    public class CustomerProductRepository : BaseRepository<CustomerProduct>
    {
        private DataContext _dataContext;

        public CustomerProductRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
    }
}
