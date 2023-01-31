using Sklep_MVC_Projekt.Data;
using Sklep_MVC_Projekt.Models;

namespace Sklep_MVC_Projekt.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>
    {
        private DataContext _dataContext;

        public CustomerRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }

        public List<Customer> NewsletterCustomers()
        {
            return _dataContext.Customer.Where(e => e.Newsletter).ToList();
        }
    }
}
