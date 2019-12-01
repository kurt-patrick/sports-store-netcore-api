using System;
using System.Collections.Generic;
using System.Linq;
using SportsStoreApi.Entities;
using SportsStoreApi.Interfaces;

namespace SportsStoreApi.Services
{
    public class ProductService : IProductService
    {
        private readonly StoreContext _storeContext;
        public ProductService(StoreContext storeContext)
        {
            if(storeContext == null)
            {
                throw new ArgumentNullException(nameof(storeContext));
            }
            this._storeContext = storeContext;
            // this call is required for the table to be created
            storeContext.Database.EnsureCreated();
        }

        public Product GetById(int id)
        {
            Console.WriteLine("ProductService.GetById");
            return _storeContext.Products.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Product> Search(string name)
        {
            Console.WriteLine($"ProductService.Search({name??""})");
            return _storeContext.Products.Where(p => p.ProductName.Contains(name));
        }

        public IEnumerable<Product> GetAll()
        {
            Console.WriteLine("ProductService.GetAll");
            return _storeContext.Products.ToList();
        }

        public IEnumerable<Product> GetIn(List<int> ids)
        {
            Console.WriteLine("ProductService.GetIn");
            return from product in _storeContext.Products
                where ids.Contains(product.Id)
                select product;
        }

    }
}
