using Microsoft.AspNetCore.Authorization;
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

        public ActionResult ProductDetails(int id)
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
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProduct(int id)
        {
            _productService.DeleteById(id);
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            Product product = _productService.GetById(id);
            return View(product);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Product product)
        {
            Product p = _productService.GetById(product.ProductID);
            p.ProductID = product.ProductID;
            p.ProductName = product.ProductName;
            p.Price= product.Price;
            p.Description= product.Description;
            p.IsNew= product.IsNew;
            p.IsOnSale= product.IsOnSale;

            _productService.Update(p);
            _productService.SaveChanges();
            return View(product);
        }
    }
}
