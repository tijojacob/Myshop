﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Contracts;
using MyShop.Core.Models;

namespace Myshop.WebUI.Tests.Mocks
{
    public class MockContext<T> : IInMemoryRepository<T> where T : BaseEntity
    {
        List<T> items;
        string className=String.Empty;
        public MockContext()
        {            
            items = new List<T>();           
        }
        public void Commit()
        {
            return;
        }
        public void Insert(T t)
        {
            items.Add(t);
        }
        public void Update(T t)
        {
            T tToUpdate = items.Find(i => i.Id == t.Id);
            if (tToUpdate == null)
            {
                tToUpdate = t;
            }
            else
            {
                throw new Exception(className + " Not Found ");
            }
        }
        public T Find(string Id)
        {
            T tToFind = items.Find(i => i.Id == Id);
            if (tToFind != null)
            {
                return tToFind;
            }
            else
            {
                throw new Exception(className + " Not Found ");
            }
        }
        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }
        public void Delete(T t)
        {
            T tToDelete = items.Find(i => i.Id == t.Id);
            if (tToDelete != null)
            {
                items.Remove(tToDelete);
            }
            else
            {
                throw new Exception(className + " Not Found ");
            }
        }
    }
}
