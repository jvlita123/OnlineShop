using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sklep_MVC_Projekt.Models;
using Sklep_MVC_Projekt.Services;

namespace Sklep_MVC_Projekt.Controllers
{
    public class CustomerController : Controller
    {
        private CustomerProductService _customerProductService;
        private CustomerService _customerService;
        private ProductService _productService;

        public CustomerController(CustomerProductService customerProductService, CustomerService customerService, ProductService productService)
        {
            _customerProductService = customerProductService;
            _customerService = customerService;
            _productService = productService;
        }

        // GET: CustomerController
        public ActionResult Index()
        {
            return View(_customerService.GetAll().ToList());
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(int id)
        {
            Customer customer = _customerService.GetById(id);

            return View(customer);
        }

        [HttpGet]
        public IActionResult EditCustomer(int id)
        {
            Customer customer = _customerService.GetById(id);
            return View(customer);
        }

        [HttpPost]
        public IActionResult EditCustomer(Customer customer)
        {
            Customer c = _customerService.GetById(customer.CustomerID);
            c.AdressFlat = customer.AdressFlat;
            c.AdressCity = customer.AdressCity;
            c.AdressStreet = customer.AdressStreet;
            c.AdressCountry = customer.AdressCountry;
            c.AdressBuilding = customer.AdressBuilding;
            c.Email = customer.Email;
            c.LastName = customer.LastName;
            c.FirstName = customer.FirstName;
            c.Postcode = customer.Postcode;


            _customerService.Update(c);
            _customerService.SaveChanges();
            return View(customer);
        }

        // GET: CustomerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
