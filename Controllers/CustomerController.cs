using Microsoft.AspNetCore.Authorization;
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

        private readonly MailService _mailService;

        public CustomerController(CustomerProductService customerProductService, CustomerService customerService, ProductService productService, MailService mailService)
        {
            _customerProductService = customerProductService;
            _customerService = customerService;
            _productService = productService;
            _mailService = mailService;
        }

        public class QuestionViewModel
        {
            public string? Name { get; set; }
            public string? Question { get; set; }
            public string? Email { get; set; }
        }

        // GET: CustomerController
        [Authorize(Roles ="Admin")]
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


        [HttpGet]
        public IActionResult Question()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Question(QuestionViewModel model)
        {
            if (model is null || model.Name is null || model.Email is null || model.Question is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            _mailService.SendEmail($"<h1>Pytanie do admina</h1><br /><h3>Od: {model.Email}</h3><br /><p>{model.Question}</p>", "Pytanie do admina", "mvcshopemailer@gmail.com", model.Name);

            return RedirectToAction("Index", "Home");
        }
    }
}
