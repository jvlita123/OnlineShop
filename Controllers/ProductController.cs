using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sklep_MVC_Projekt.Models;
using Sklep_MVC_Projekt.Services;
using System.Collections.Generic;
using X.PagedList;

namespace Sklep_MVC_Projekt.Controllers
{
    public class ProductController : Controller
    {
        private ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.PriceSortParm = String.IsNullOrEmpty(sortOrder) ? "price_desc" : "";

            var products = _productService.GetAll().ToList();

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.ProductName.Contains(searchString)
                                       || s.ProductName.Contains(searchString)).ToList();
            }

            switch (sortOrder)
            {
                case "price_desc":
                    products = products.OrderByDescending(s => s.Price).ToList();
                    break;
                default:
                    products = products.OrderBy(s => s.Price).ToList();
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));
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
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProduct(int id)
        {
            _productService.DeleteById(id);
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Product product = _productService.GetById(id);
            return View(product);
        }

        [HttpPost]
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
