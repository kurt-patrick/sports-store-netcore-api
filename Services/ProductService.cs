using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SportsStoreApi.Entities;
using SportsStoreApi.Helpers;

namespace SportsStoreApi.Services
{
    public interface IProductService
    {
        Product GetById(int id);
        IEnumerable<Product> Search(string name);
        IEnumerable<Product> GetIn(List<int> ids);
        IEnumerable<Product> GetAll();
    }

    public class ProductService : IProductService
    {
        private List<Product> _products = new List<Product>
        {
            new Product() {
                Id = 1,
                ProductName = "Jordan AJ 1 Mid",
                ProductPrice =  110.00m,
                ImageUrl = "https://images.footlocker.com/is/image/EBFL2/54724058_a1?wid=640&hei=640&fmt=png-alpha",
                Gender = Gender.mens,
                ColourDescription = "Black/Starfish/White"
            },
            new Product() {
                Id = 2,
                ProductName = "Jordan Retro 4",
                ProductPrice =  139.99m,
                ImageUrl = "https://images.footlocker.com/is/image/EBFL2/C1184617_a1?wid=640&hei=640&fmt=png-alpha",
                Gender = Gender.mens,
                ColourDescription = "Gym Red/Obsidian/White/Metallic Gold"
            },
            new Product() {
                Id = 3,
                ProductName = "Jordan Retro 9",
                ProductPrice =  190.00m,
                ImageUrl = "https://images.footlocker.com/is/image/EBFL2/32370160_a1?wid=640&hei=640&fmt=png-alpha",
                Gender = Gender.mens,
                ColourDescription = "White/Black/Gym Red"
            },
            new Product() {
                Id = 4,
                ProductName = "Jordan Retro 3",
                ProductPrice =  190.00m,
                ImageUrl = "https://images.footlocker.com/is/image/EBFL2/36064148_a1?wid=640&hei=640&fmt=png-alpha",
                Gender = Gender.mens,
                ColourDescription = "White/Old Royal/University Orange/Tech Grey"
            },
            new Product() {
                Id = 5,
                ProductName = "Jordan Retro 3",
                ProductPrice =  149.99m,
                ImageUrl = "https://images.footlocker.com/is/image/EBFL2/K4348007_a1?wid=640&hei=640&fmt=png-alpha",
                Gender = Gender.mens,
                ColourDescription = "Black/Cement Grey/Metallic Gold"
            },
            new Product() {
                Id = 6,
                ProductName = "Jordan Retro 12",
                ProductPrice =  140.00m,
                ImageUrl = "https://images.footlocker.com/is/image/EBFL2/53265014_a1?wid=640&hei=640&fmt=png-alpha",
                Gender = Gender.mens,
                ColourDescription = "Black/Game Royal/Metallic Silver"
            },
            new Product() {
                Id = 7,
                ProductName = "Jordan AJ 1 Mid",
                ProductPrice =  99.99m,
                ImageUrl = "https://images.footlocker.com/is/image/EBFL2/Q6472111_a1?wid=640&hei=640&fmt=png-alpha",
                Gender = Gender.mens,
                ColourDescription = "White/White"
            },
            new Product() {
                Id = 8,
                ProductName = "Jordan AJ 1 Low",
                ProductPrice =  90.00m,
                ImageUrl = "https://images.footlocker.com/is/image/EBFL2/53558128_a1?wid=640&hei=640&fmt=png-alpha",
                Gender = Gender.mens,
                ColourDescription = "White/Black/Starfish"
            },
            new Product() {
                Id = 9,
                ProductName = "Jordan 6 Rings",
                ProductPrice =  134.99m,
                ImageUrl = "https://images.footlocker.com/is/image/EBFL2/22992062_a1?wid=640&hei=640&fmt=png-alpha",
                Gender = Gender.mens,
                ColourDescription = "Black/Varsity Red/White"
            }
        };

        public ProductService()
        {
        }

        public Product GetById(int id) => _products.FirstOrDefault(p => p.Id == id);

        public IEnumerable<Product> Search(string name)
        {
            return GetAll().Where(p => p.ProductName.Contains(name, StringComparison.InvariantCultureIgnoreCase));
        }

        public IEnumerable<Product> GetAll()
        {
            return _products;
        }

        public IEnumerable<Product> GetIn(List<int> ids)
        {
            return GetAll().Where(p => ids.Contains(p.Id));
        }

    }
}
