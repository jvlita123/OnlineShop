using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sklep_MVC_Projekt.Models;
using Sklep_MVC_Projekt.Services;

namespace Sklep_MVC_Projekt.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        public ActionResult Index()
        {
            return View(_productService.GetAll().ToList());
        }

        public ActionResult Details(int id)
        {
            return View(_productService.GetById(id));
        }

        public ActionResult AddNewProduct()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewProduct(Product product)
        {
            _productService.AddNewProduct(product);
            return View(product);
        }

        public ActionResult DeleteProduct()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProduct(int id)
        {
            _productService.DeleteById(id);
            return View();

        }
    }
}
