using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sklep_MVC_Projekt.Models;
using Sklep_MVC_Projekt.Services;
using System.Net;

namespace Sklep_MVC_Projekt.Controllers
{
    public class CustomerProductController : Controller
    {

        private CustomerProductService _customerProductService;
        private CustomerService _customerService;
        private ProductService _productService;

        public CustomerProductController(CustomerProductService customerProductService, CustomerService customerService, ProductService productService)
        {
            _customerProductService = customerProductService;
            _customerService = customerService;
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View(_customerProductService.GetAll());
        }

        public IActionResult GetCustomerProducts()
        {
            Customer customer = _customerService.GetByEmail(HttpContext.User.Identity.Name);
            var customerProducts = _customerProductService.GetAll().Where(x=>x.CustomerID== customer.CustomerID);

            return View(customerProducts.ToList());
        }

        public IActionResult AddCustomerProduct(int id)
        {
            CustomerProduct customerProduct = new CustomerProduct();
            string c = HttpContext.User.Identity.Name;
            customerProduct.CustomerID = _customerService.GetByEmail(HttpContext.User.Identity.Name).CustomerID;
            customerProduct.ProductID = id;
            _customerProductService.AddCustomerProduct(customerProduct);

            return RedirectToAction("Index", "Product");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCustomerProduct(CustomerProduct customerProduct)
        {
            customerProduct.CustomerID = _customerService.GetByEmail(HttpContext.User.Identity.Name).CustomerID;
            _customerProductService.AddCustomerProduct(customerProduct);

            ViewBag.CustomerID = new SelectList(_customerService.GetAll(), "CustomerID", "FirstName", customerProduct.CustomerID);
            ViewBag.ProductID = new SelectList(_productService.GetAll(), "ProductID", "ProductName", customerProduct.ProductID);
            return RedirectToAction("Product");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCustomerProduct(int id)
        {
            _customerProductService.DeleteByIdAndSaveChanges(id);
            return RedirectToAction("GetCustomerProducts");
        }

        public IActionResult Details(int id)
        {
            Product product = _productService.GetById(id);

            return View(product);
        }
        // GET: CustomerProductController/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomerProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
