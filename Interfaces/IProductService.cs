using System.Collections.Generic;
using SportsStoreApi.Entities;

namespace SportsStoreApi.Interfaces
{
    public interface IProductService
    {
        Product GetById(int id);
        IEnumerable<Product> Search(string name);
        IEnumerable<Product> GetIn(List<int> ids);
        IEnumerable<Product> GetAll();
    }
}