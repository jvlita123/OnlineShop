using Sklep_MVC_Projekt.Data;
using Sklep_MVC_Projekt.Models;

namespace Sklep_MVC_Projekt.Repositories
{
    public class ShippingMethodRepository : BaseRepository<ShippingMethod>
    {
        private DataContext _dataContext;

        public ShippingMethodRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
    }
}
