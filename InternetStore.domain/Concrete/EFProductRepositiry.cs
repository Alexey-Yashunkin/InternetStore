﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternetStore.Domain.Abstract;
using InternetStore.Domain.Entities;

namespace InternetStore.Domain.Concrete
{
    public class EFProductRepositiry : IProductRepository
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<Product> Products
        {
            get { return context.Products; }
        }

        public void SaveProduct(Product product)
        {
            if (product.ProductID == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                Product dbEntry = context.Products.Find(product.ProductID);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                }
            }
            context.SaveChanges();
        }
    }
}
