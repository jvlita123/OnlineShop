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

        public OrderService(OrderRepository orderRepository, ProductOrderRepository productOrderRepository)
        {
            _orderRepository = orderRepository;
            _productOrderRepository = productOrderRepository;
        }

        public List<Order> GetAll()
        {
            var order = _orderRepository.GetAll().Include(o => o.Customer).Include(o => o.PaymentMethod).Include(o => o.ShippingMethod).Include(o => o.ProductOrders);
            return order.ToList();
        }

        public Order GetById(int id)
        {
            return _orderRepository.GetAll().Where(x => x.OrderID == id).Include(x=>x.ProductOrders).Include("Photo").FirstOrDefault();
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
