using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IProductRepository
    {
        public Product? GetByName(string name);
        public Product? GetById(int id);
        public Product Add(Product product);
        public void Update(Product product);
        public void Delete(Product product);
        public void SaveChanges();
        public Product? SearchId(int id);
    }
}
