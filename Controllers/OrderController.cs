using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Sklep_MVC_Projekt.Models;
using Sklep_MVC_Projekt.Services;
using System.Net;

namespace Sklep_MVC_Projekt.Controllers
{
    public class OrderController : Controller
    {

        private ProductService _productService;
        private ProductOrderService _productOrderService;
        private CategoryService _categoryService;
        private OrderService _orderService;
        private CustomerService _customerService;
        private CustomerProductService _customerProductService;
        private PaymentMethodService _paymentMethodService;
        private ShippingMethodService _shippingMethodService;

        public OrderController(ProductService productService, CategoryService categoryService, OrderService orderService, CustomerService customerService, CustomerProductService customerProductService, ProductOrderService productOrderService, PaymentMethodService paymentMethodService, ShippingMethodService shippingMethodService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _orderService = orderService;
            _customerService = customerService;
            _customerProductService = customerProductService;
            _productOrderService = productOrderService;
            _paymentMethodService = paymentMethodService;
            _shippingMethodService = shippingMethodService;
        }
        public ActionResult Index()
        {
            var orders = _orderService.GetAll();
            return View(orders);
        }

        public ActionResult AddNewOrder()
        {
            ViewBag.Id = new SelectList(_customerService.GetAll(), "Id", "FirstName");
            ViewBag.PaymentMethodID = new SelectList(_paymentMethodService.GetAll(), "PaymentMethodID", "Name");
            ViewBag.ShippingMethodID = new SelectList(_shippingMethodService.GetAll(), "ShippingMethodID", "Name");
            ViewBag.OrderID = new SelectList(_orderService.GetAll(), "OrderID", "OrderID");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewOrder(Order order)
        {
            List<CustomerProduct> customerProducts = _customerProductService.CustomerProducts(HttpContext.User.Identity.Name);
            List<ProductOrder> productOrders = new List<ProductOrder>();

            ViewBag.PaymentMethodID = new SelectList(_paymentMethodService.GetAll(), "PaymentMethodID", "Name", order.PaymentMethodID);
            ViewBag.ShippingMethodID = new SelectList(_shippingMethodService.GetAll(), "ShippingMethodID", "Name", order.ShippingMethodID);

            order.CustomerID = _customerService.GetByEmail(HttpContext.User.Identity.Name).CustomerID;
            order = _orderService.AddNewOrder(order);

            List<CustomerProduct> customerProductsList = _customerProductService.CustomerProducts(HttpContext.User.Identity.Name).ToList();

            foreach (var p in customerProductsList)
            {
                ProductOrder productOrder = new ProductOrder();
                productOrder.OrderID = order.OrderID;
                productOrder.ProductID = p.ProductID;
                _productOrderService.AddProductOrder(productOrder);
            }

            _customerProductService.DeleteAll(customerProductsList);

            return View(order);
        }
        public ActionResult GetCustomerOrders()
        {
            List<Order> orders = _orderService.GetCustomerOrders(HttpContext.User.Identity.Name);
            return View(orders);
        }

        public IActionResult Details(int id)
        {
            Order order = _orderService.GetById(id);
            order.ProductOrders = _productOrderService.GetAll().Where(x => x.OrderID == id).ToList();

            return View(order);
        }
    }
}

