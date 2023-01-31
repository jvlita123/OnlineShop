using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Sklep_MVC_Projekt.Models;
using Sklep_MVC_Projekt.Repositories;
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
        [HttpGet]
        public ActionResult AddNewOrder()
        {
            ViewBag.PaymentMethodID = new SelectList(_paymentMethodService.GetAll(), "PaymentMethodID", "Name");
            ViewBag.ShippingMethodID = new SelectList(_shippingMethodService.GetAll(), "ShippingMethodID", "Name");

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
            Product product = new Product();
            List<CustomerProduct> customerProductsList = _customerProductService.CustomerProducts(HttpContext.User.Identity.Name).ToList();

            foreach (var p in customerProductsList)
            {
                ProductOrder productOrder = new ProductOrder();
                productOrder.OrderID = order.OrderID;
                productOrder.ProductID = p.ProductID;
                productOrder.Product = p.Product;
                productOrder.Order = order;
                _productOrderService.AddProductOrder(productOrder);

                product = _productService.GetById(p.ProductID);
                product.AvailableAmmount--;
                _productService.UpdateAndSaveChanges(product);

                order.Price += p.Product.Price;
            }
            _orderService.UpdateAndSaveChanges(order);
            _customerProductService.DeleteAll(customerProductsList);

            return RedirectToAction("Details", new { id = order.OrderID });

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

		public IActionResult ChangeStatus(int id, string status)
		{
			Order order = _orderService.GetById(id);
            order.Status = status;

			_orderService.Update(order);
			_orderService.SaveChanges();
            return RedirectToAction("Index", "Order");
        }
    }
}

