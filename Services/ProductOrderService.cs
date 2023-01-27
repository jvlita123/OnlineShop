using Microsoft.EntityFrameworkCore;
using Sklep_MVC_Projekt.Models;
using Sklep_MVC_Projekt.Repositories;

namespace Sklep_MVC_Projekt.Services
{
    public class ProductOrderService
    {
        private readonly ProductOrderRepository _productOrderRepository;
        private readonly OrderRepository _orderRepository;

        public ProductOrderService(ProductOrderRepository productOrderRepository, OrderRepository orderRepository)
        {
            _productOrderRepository = productOrderRepository;
            _orderRepository = orderRepository; 
        }

        public List<ProductOrder> GetAll()
        {
            return _productOrderRepository.GetAll().Include(x=>x.Product).Include(x=>x.Order).Include(x=>x.Product.Photo).ToList();
        }

        public List<ProductOrder> GetById(int id)
        {
            return _productOrderRepository.GetAll().Where(x => x.ProductOrderID == id).Include("Product").Include("Order").ToList();
        }

        public ProductOrder AddProductOrder(ProductOrder productOrder)
        {
            var newProductOrder = new ProductOrder()
            {
                Product = productOrder.Product,
                ProductID = productOrder.ProductID,
                OrderID = productOrder.OrderID,
                Order = productOrder.Order,
            };

            _productOrderRepository.AddAndSaveChanges(newProductOrder);
            return newProductOrder;
        }
        public List<ProductOrder> AddProductsOrder(List<ProductOrder> ProductsOrder)
        {
            _productOrderRepository.AddRange(ProductsOrder);
            return _productOrderRepository.GetAll().Include("Product").Include("Order").ToList();
        }

        public void SaveChanges()
        {
            _productOrderRepository.SaveChanges();
        }

        public void Add(List<ProductOrder> productOrders)
        {
            _productOrderRepository.AddRange(productOrders);
        }

        public List<Product> GetOrderProducts(int id)
        {
            return _productOrderRepository.GetAll().Include("Product").Include("Order").Where(x => x.OrderID == id).Select(x => x.Product).ToList();
        }

        public List<ProductOrder> DeleteByIdAndSaveChanges(int id)
        {
            _productOrderRepository.RemoveByIdAndSaveChanges(id);
            return _productOrderRepository.GetAll().Include("Product").Include("Order").ToList();
        }
    }
}
