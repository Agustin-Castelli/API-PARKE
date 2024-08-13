using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationContext _context;

        public ProductRepository(ApplicationContext context)
        {
            _context = context;
        }

        public Product? GetByName(string name)
        {
            return _context.Products.FirstOrDefault(p => p.ProductName == name);
        }

        public Product? GetById(int id)
        { 
            return _context.Products.FirstOrDefault(p => p.Id == id); 
        }

        public Product Add(Product product)
        {
            _context.Set<Product>().Add(product);
            _context.SaveChanges();
            return product;
        }

        public void Update(Product product)
        {
            _context.SaveChanges();
        }

        public void Delete(Product product)
        {
            _context.Remove(product);
            _context.SaveChanges();
        }

        public void SaveChanges() { _context.SaveChanges(); }

        public Product? SearchId(int id)
        {
            var productFound = _context.Set<Product>().FirstOrDefault(p => p.Id == id);

            return productFound;
        }
    }
}