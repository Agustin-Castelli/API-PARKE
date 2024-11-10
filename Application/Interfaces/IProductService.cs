using Application.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Product Create(ProductCreateRequest product);
        void Delete(int id);
        Product GetById(int id);
        Product GetByName(string name);
        Product GetByCode(string code);
        public List<Product> GetAll();
        void Update(int id, ProductUpdateRequest product);
    }
}
