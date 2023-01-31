using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sklep_MVC_Projekt.Models;
using Sklep_MVC_Projekt.Services;

namespace Sklep_MVC_Projekt.Controllers
{
    public class ProductOrderController : Controller
    {
        private ProductService _productService;
        private ProductOrderService _productOrderService;
        private OrderService _orderService;
        private CustomerService _customerService;
        private CustomerProductService _customerProductService;

        public ProductOrderController(ProductService productService, OrderService orderService, ProductOrderService productOrderService, CustomerService customerService, CustomerProductService customerProductservice)
        {
            _productOrderService = productOrderService;
            _productService = productService;
            _orderService = orderService;
            _customerService = customerService;
            _customerProductService = customerProductservice;
        }
        public ActionResult Index()
        {
            return View(_productOrderService.GetAll());
        }

        [HttpGet]
        public ActionResult AddProductOrder(int id)
        {
            List<CustomerProduct> customerProductsList = _customerProductService.CustomerProducts(HttpContext.User.Identity.Name).ToList();

            foreach (var p in customerProductsList)
            {
                ProductOrder productOrder = new ProductOrder();
                productOrder.OrderID = id;
                productOrder.ProductID = p.ProductID;
                _productOrderService.AddProductOrder(productOrder);
                _orderService.GetById(id).ProductOrders.Add(productOrder);
            }
            _customerProductService.DeleteAll(customerProductsList);
            return RedirectToAction("Index", "Product");
        }

        public ActionResult GetProductsOrder(int id)
        {
            //  Order order = _orderService.GetAll().Where(x => x.OrderID == id).FirstOrDefault();
            return View(_productOrderService.GetById(id));
        }

        public ActionResult AddProductOrder()
        {
            ViewBag.OrderID = new SelectList(_orderService.GetAll(), "OrderID", "Status");
            ViewBag.ProductID = new SelectList(_productService.GetAll(), "ProductID", "ProductName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProductOrder(ProductOrder productOrder)
        {
            _productOrderService.AddProductOrder(productOrder);

            ViewBag.OrderID = new SelectList(_orderService.GetAll(), "OrderID", "Status", productOrder.OrderID);
            ViewBag.ProductID = new SelectList(_productService.GetAll(), "ProductID", "ProductName", productOrder.ProductID);
            return View(productOrder);
        }
	}
}