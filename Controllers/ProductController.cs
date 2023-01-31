using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sklep_MVC_Projekt.Models;
using Sklep_MVC_Projekt.Services;
using System.Collections.Generic;
using X.PagedList;

namespace Sklep_MVC_Projekt.Controllers
{
    public class ProductController : Controller
    {
        private ProductService _productService;
        private CurrencyService _currencyService;
        private CustomerService _customerService;

        public ProductController(ProductService productService, CurrencyService currencyService, CustomerService customerService)
        {
            _productService = productService;
            _currencyService = currencyService;
            _customerService = customerService;
        }

		public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page,string Currency)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.PriceSortParm = String.IsNullOrEmpty(sortOrder) ? "price_desc" : "";


			//List<string> currency = _currencyService.GetCurrency();
			//ViewBag.Currency = new SelectList(currency, "Currency", "Name", Currency);

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

			int pageSize = 4;
            int pageNumber = (page ?? 1);
            SetIsNew(products);
            SetIsOnSale(products);
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
            p.DateAdded=product.DateAdded;
            p.SaleEndDate=product.SaleEndDate;  
            p.CategoryID= product.CategoryID;
            p.IsNew= product.IsNew;
            p.IsOnSale= product.IsOnSale;

            _productService.Update(p);
            _productService.SaveChanges();
            return View(product);
        }

        public IActionResult Nowo()
        {
            var products = _productService.GetAll().OrderByDescending(p => p.DateAdded).Take(10).ToList();
            SetIsNew(products);
            return View(products);
        }

        public IActionResult Promo()
        {
            var products = _productService.GetAll().ToList();
            SetIsOnSale(products);
            return View(products);
        }

        private void SetIsNew(List<Product> products)
        {
            var now = DateTime.Now;
            var numberOfNewProducts = 10;

            for (var i = 0; i < products.Count && i < numberOfNewProducts; i++)
            {
                products[i].IsNew = true;
            }
        }

		private void SetIsOnSale(List<Product> products)
		{
			var now = DateTime.Now;

			foreach (var product in products)
			{
				if (product.SaleEndDate >= now)
				{
					product.IsOnSale = true;
					product.Price = product.Price * 0.9m;
				}
				else
				{
					product.IsOnSale = false;
				}
			}
		}
	}
}
