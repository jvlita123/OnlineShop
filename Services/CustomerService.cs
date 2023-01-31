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

        public List<Customer> NewsletterCustomers()
        {
            return _customerRepository.NewsletterCustomers();
        }
        public List<Customer> GetAll()
        {
            return _customerRepository.GetAll().ToList();
        }

        public Customer GetByEmail(string email)
        {
            return _customerRepository.GetAll().Where(x => x.Email == email).FirstOrDefault();
        }

        public Customer GetById(int id)
        {
            return _customerRepository.GetAll().Where(x => x.CustomerID == id).FirstOrDefault();
        }

        public void Update(Customer customer)
        {
            _customerRepository.Update(customer);
        }

        public void SaveChanges()
        {
            _customerRepository.SaveChanges();
        }

        public Customer AddNewCustomer(Customer customer)
        {
            Customer newCustomer = _customerRepository.AddAndSaveChanges(customer);

            return newCustomer;
        }

        public Customer Add(Customer customer)
        {
            Customer newCustomer = _customerRepository.AddAndSaveChanges(customer);

            return newCustomer;
        }
    }
}
