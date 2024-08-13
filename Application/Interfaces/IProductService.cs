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
        Product Create(Product product);
        void Delete(int id);
        Product GetById(int id);
        Product GetByName(string name);
        void Update(int id, Product product);
    }
}
