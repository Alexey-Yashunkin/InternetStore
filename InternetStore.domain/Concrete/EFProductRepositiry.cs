using System;
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
    }
}
