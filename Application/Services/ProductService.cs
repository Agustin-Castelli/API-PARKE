using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Product Create(Product product)
        {
            var newObj = new Product();

            var productFound = _productRepository.SearchId(product.Id);

            if(productFound == null)
            {
                newObj.ProductName = product.ProductName;
                newObj.ProductImage = product.ProductImage;

                return _productRepository.Add(newObj);
            }

            else
            {
                throw new DuplicateElementException("Ya existe un elemento con el mismo ID.");
            }
        }

        public void Delete(int id)
        {
            var obj = _productRepository.GetById(id);

            if (obj == null) 
            {
                throw new NotFoundException(nameof(Product), id);
            }

            _productRepository.Delete(obj);
        }

        public void Update(int id, Product product)
        {
            var obj = _productRepository.GetById(id);

            if (obj == null)
            {
                throw new NotFoundException(nameof(Product), id);
            }

            if (product.ProductName != string.Empty) obj.ProductName = product.ProductName; 
            if (product.ProductImage != string.Empty) obj.ProductImage = product.ProductImage;

            _productRepository.Update(obj);
        }

        public Product GetById(int id)
        {
            var obj = _productRepository.GetById(id);

            if (obj == null)
            {
                throw new NotFoundException(nameof(Product), id);
            }

            else
            {
                return obj;
            }
        }

        public Product GetByName(string name)
        {
            var obj = _productRepository.GetByName(name);

            if (obj == null)
            {
                throw new NotFoundException(nameof(Product), name);
            }

            else
            {
                return obj;
            }
        }


    }
}
