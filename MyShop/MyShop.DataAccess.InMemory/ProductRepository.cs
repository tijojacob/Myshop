using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products;
        public ProductRepository()
        {
            products = cache["products"] as List<Product>;
            if (products == null)
            {
                products = new List<Product>();
            }
        }
        public void Commit()
        {
            cache["products"] = products;
        }
        public void Insert(Product p)
        {
            products.Add(p);
        }
        public void Update(Product p)
        {
            Product productToUpdate = products.Find(x => x.Id == p.Id);
            if (productToUpdate != null)
            {
                //productToUpdate.Name = p.Name;
                //productToUpdate.Price = p.Price;
                //productToUpdate.Image = p.Image;
                productToUpdate = p;
            }
            else
            {
                throw new Exception("No Product Found!.");
            }
        }
        public Product Find(Product p)
        {
            Product product = products.Find(x => x.Id == p.Id);
            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("No Product Found!.");
            }
        }
        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }
        public void Delete(Product p)
        {
            Product productToDelete = products.Find(x => x.Id == p.Id);
            if (productToDelete != null)
            {
                products.Remove(productToDelete);
            }
            else
            {
                throw new Exception("No Product Found!.");
            }
        }
    }
}
