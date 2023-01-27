using Microsoft.EntityFrameworkCore;
using Sklep_MVC_Projekt.Models;
using Sklep_MVC_Projekt.Repositories;

namespace Sklep_MVC_Projekt.Services
{
    public class CustomerService
    {
        private readonly CustomerRepository _customerRepository;

        public CustomerService(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public List<Customer> GetAll()
        {
            return _customerRepository.GetAll().Include(x=>x.IdentityUser).ToList();
        }

        public Customer GetByEmail(string email)
        {
            return _customerRepository.GetAll().Where(x => x.Email == email).FirstOrDefault();
        }

        public Customer AddNewCustomer(Customer customer)
        {
            Customer newCustomer = _customerRepository.AddAndSaveChanges(customer);

            return newCustomer;
        }
    }
}
