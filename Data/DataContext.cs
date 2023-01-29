using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sklep_MVC_Projekt.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Sklep_MVC_Projekt.Data
{
        public class DataContext : IdentityDbContext
    {
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Photo> Photo { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<ShippingMethod> ShippingMethod { get; set; }
        public DbSet<PaymentMethod> PaymentMethod { get; set; }
        public DbSet<ProductOrder> ProductOrder { get; set; }
        public DbSet<CustomerProduct> CustomerProduct { get; set; }
        public DbSet<ShopAttribute> ShopAttributes { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }

        public void AddEntity<TEntity>(TEntity entity) where TEntity : class, new()
        {
            Set<TEntity>().Add(entity);
        }

        public void AddEntityAndSaveChanges<TEntity>(TEntity entity) where TEntity : class, new()
        {
            AddEntity(entity);
            SaveChanges();
        }

        public void AddEntitiesRange<TEntity>(IEnumerable<TEntity> entity) where TEntity : class, new()
        {
            Set<TEntity>().AddRange(entity);
        }

        public void AddEntitiesRangeAndSaveChanges<TEntity>(IEnumerable<TEntity> entity) where TEntity : class, new()
        {
            AddEntitiesRange(entity);
            SaveChanges();
        }

        public void UpdateEntity<TEntity>(TEntity entity) where TEntity : class, new()
        {
            Entry(entity).State = EntityState.Modified;
        }

        public void UpdateEntitiesRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, new()
        {
            foreach (var entity in entities)
            {
                UpdateEntity(entity);
            }
        }

        public void UpdateEntitiesRangeAndSaveChanges<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, new()
        {
            UpdateEntitiesRange(entities);
            SaveChanges();
        }

        public void RemoveEntity<TEntity>(TEntity entity) where TEntity : class, new()
        {
            Set<TEntity>().Remove(entity);
        }

        public void RemoveEntitiesRange<TEntity>(IEnumerable<TEntity> entity) where TEntity : class, new()
        {
            Set<TEntity>().RemoveRange(entity);
        }

        public void RemoveEntitiesRangeAndSaveChanges<TEntity>(IEnumerable<TEntity> entity) where TEntity : class, new()
        {
            RemoveEntitiesRange(entity);
            SaveChanges();
        }
    }
}