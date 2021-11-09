using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productsCatg;
        public ProductCategoryRepository()
        {
            productsCatg = cache["productsCategory"] as List<ProductCategory>;
            if (productsCatg == null)
            {
                productsCatg = new List<ProductCategory>();
            }
        }
        public void Commit()
        {
            cache["productsCategory"] = productsCatg;
        }
        public void Insert(ProductCategory p)
        {
            productsCatg.Add(p);
        }
        public void Update(ProductCategory p)
        {
            ProductCategory productCategoryRepositoryToUpdate = productsCatg.Find(x => x.Id == p.Id);
            if (productCategoryRepositoryToUpdate != null)
            {                
                productCategoryRepositoryToUpdate.Catagery = p.Catagery;
                productCategoryRepositoryToUpdate.Id = p.Id;
                //productToUpdate = p;
            }
            else
            {
                throw new Exception("No Product Category Found!.");
            }
        }
        public ProductCategory Find(string Id)
        {
            ProductCategory productCat = productsCatg.Find(x => x.Id == Id);
            if (productCat != null)
            {
                return productCat;
            }
            else
            {
                throw new Exception("No Product Category Found!.");
            }
        }
        public IQueryable<ProductCategory> Collection()
        {
            return productsCatg.AsQueryable();
        }
        public void Delete(ProductCategory p)
        {
            ProductCategory productCatToDelete = productsCatg.Find(x => x.Id == p.Id);
            if (productCatToDelete != null)
            {
                productsCatg.Remove(productCatToDelete);
            }
            else
            {
                throw new Exception("No Product Category Found!.");
            }
        }
    }
}
