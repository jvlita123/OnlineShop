using Sklep_MVC_Projekt.Data;
using Sklep_MVC_Projekt.Models;

namespace Sklep_MVC_Projekt.Repositories
{
    public class PaymentMethodRepository : BaseRepository<PaymentMethod>
    {
        private DataContext _dataContext;

        public PaymentMethodRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
    }
}
