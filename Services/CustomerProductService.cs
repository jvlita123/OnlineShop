using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using Sklep_MVC_Projekt.Models;
using Sklep_MVC_Projekt.Repositories;

namespace Sklep_MVC_Projekt.Services
{
    public class CustomerProductService
    {
        private readonly CustomerProductRepository _customerProductRepository;

        public CustomerProductService(CustomerProductRepository customerProductRepository)
        {
            _customerProductRepository = customerProductRepository;
        }

        public List<CustomerProduct> AddCustomerProduct(CustomerProduct customerProduct)
        {
            var newCustomerProduct = new CustomerProduct()
            {
                Product = customerProduct.Product,
                ProductID = customerProduct.ProductID,
                CustomerID = customerProduct.CustomerID,
                Customer = customerProduct.Customer,
            };
            _customerProductRepository.AddAndSaveChanges(newCustomerProduct);
            return _customerProductRepository.GetAll().Include(x=>x.Customer).Include(x=>x.Product).Include(x=>x.Product.Photo).ToList();
        }

        public List<CustomerProduct> GetAll()
        {
            return _customerProductRepository.GetAll().Include(x=>x.Customer).Include(x=>x.Product).Include(x=>x.Product.Photo).ToList();
        }
        public CustomerProduct GetById(int id)
        {
            return _customerProductRepository.GetById(id);
        }

        public List<CustomerProduct> CustomerProducts(int id)
        {
            List<CustomerProduct> list = _customerProductRepository.GetAll().Include(x=>x.Product.Photo).Include(x=>x.Customer).Include(x=>x.Product).Where(x => x.Customer.CustomerID == id).ToList();

            return list;
        }

        public List<CustomerProduct> DeleteAll(List<CustomerProduct> customerProducts)
        {
            _customerProductRepository.RemoveRange(customerProducts);
            _customerProductRepository.SaveChanges();
            return _customerProductRepository.GetAll().ToList();
        }

        public List<CustomerProduct> DeleteByIdAndSaveChanges(int id)
        {
            _customerProductRepository.RemoveByIdAndSaveChanges(id);
            return _customerProductRepository.GetAll().ToList();
        }
        public void UpdateAndSaveChanges(CustomerProduct customerproduct)
        {
            _customerProductRepository.UpdateAndSaveChanges(customerproduct);
        }
        public void SaveChanges()
        {
            _customerProductRepository.SaveChanges();
        }
    }
}
