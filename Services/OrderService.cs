using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sklep_MVC_Projekt.Models;
using Sklep_MVC_Projekt.Repositories;

namespace Sklep_MVC_Projekt.Services
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;
        private readonly ProductOrderRepository _productOrderRepository;
        private readonly ProductRepository _productRepository;

        public OrderService(OrderRepository orderRepository, ProductOrderRepository productOrderRepository, ProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productOrderRepository = productOrderRepository;
            _productRepository = productRepository; 
        }

        public List<Order> GetAll()
        {
            var order = _orderRepository.GetAll().Include(x => x.Customer).Include(x => x.PaymentMethod).Include(x => x.ShippingMethod).Include(x => x.ProductOrders);
            return order.ToList();
        }

        public Order GetById(int id)
        {
            return _orderRepository.GetAll().Where(x => x.OrderID == id).Include(x=>x.ProductOrders).FirstOrDefault();
        }

        public Order AddNewOrder(Order order)
        {

            var newOrder = new Order()
            {
                OrderID = order.OrderID,
                Customer = order.Customer,
                CustomerID = order.CustomerID,
                ShippingMethod = order.ShippingMethod,
                ShippingMethodID = order.ShippingMethodID,
                PaymentMethod = order.PaymentMethod,
                PaymentMethodID = order.PaymentMethodID,
                Price = order.Price,
                Status = order.Status,
                DeliveryAdressFlat = order.DeliveryAdressFlat,
                DeliveryAdressBuilding = order.DeliveryAdressBuilding,
                DeliveryAdressStreet = order.DeliveryAdressStreet,
                DeliveryAdressCity = order.DeliveryAdressCity,
                DeliveryAdressCountry = order.DeliveryAdressCountry,
                Postcode = order.Postcode,
                PhoneNumber = order.PhoneNumber,
                ProductOrders= order.ProductOrders,
            };

            _orderRepository.AddAndSaveChanges(newOrder);
            return newOrder;
        }
        public List<Order> GetCustomerOrders(string email)//!!!!!
        {
            List<Order> list = _orderRepository.GetAll().Where(x => x.Customer.IdentityUser.Email == email).ToList();

            return list;
        }

        public void SaveChanges()
        {
            _orderRepository.SaveChanges();
        }
        public void UpdateAndSaveChanges(Order order)
        {
            _orderRepository.UpdateAndSaveChanges(order);
        }
    }
}
