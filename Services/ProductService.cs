﻿using Microsoft.EntityFrameworkCore;
using Sklep_MVC_Projekt.Models;
using Sklep_MVC_Projekt.Repositories;

namespace Sklep_MVC_Projekt.Services
{
    public class ProductService
    {
        private readonly ProductRepository _productRepository;

        public ProductService(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public List<Product> GetNewAndPromoProducts()
        {
            return _productRepository.GetNewAndPromoProducts();
        }
        public List<Product> GetAll()
        {
            return _productRepository.GetAll().Include(x=>x.Photo).ToList();
        }
        public Product GetById(int id)
        {
            return _productRepository.GetAll().Include(x=>x.Photo).Where(x=>x.ProductID == id).FirstOrDefault();
        }

        public Product AddNewProduct(Product product)
        {
            var newProduct = new Product()
            {
                ProductName = product.ProductName,
                Price = product.Price,
                Description = product.Description,
                AvailableAmmount = product.AvailableAmmount,
                Category = product.Category,
                CategoryID = product.CategoryID,
                IsNew = product.IsNew,
                IsOnSale = product.IsOnSale,
                Photo = product.Photo,
            };

            return _productRepository.AddAndSaveChanges(newProduct);
        }

        public List<Product> DeleteById(int id)
        {
            _productRepository.RemoveByIdAndSaveChanges(id);
            return _productRepository.GetAll().ToList();
        }

        public void SaveChanges()
        {
           _productRepository.SaveChanges();
        }
        public void Update(Product product)
        {
            _productRepository.Update(product);
        }

        public void UpdateAndSaveChanges(Product product)
        {
            _productRepository.UpdateAndSaveChanges(product);
        }

        public List<Product> Nowosci()
        {
            
            return _productRepository.GetNowosci();

        }

        public List<Product> Promocje()
        {

            return _productRepository.GetPromo();

        }
    }
}
