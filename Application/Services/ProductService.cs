using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Product Create(ProductCreateRequest product)
        {
            var newObj = new Product();

            //var productFound = _productRepository.SearchCode(product.Code);

            //if(productFound == null)
            //{
            //    newObj.ProductName = product.ProductName;
            //    newObj.Code = product.Code;
            //    newObj.ProductImage = product.ProductImage;

            //    return _productRepository.Add(newObj);
            //}

            newObj.ProductName = product.ProductName;
            newObj.Code = product.Code;
            newObj.ProductImage = product.ProductImage;

            return _productRepository.Add(newObj);

            //else
            //{
            //    throw new DuplicateElementException("Ya existe un elemento con el mismo ID.");
            //}
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

        public void Update(int id, ProductUpdateRequest product)
        {
            var obj = _productRepository.GetById(id);

            if (obj == null)
            {
                throw new NotFoundException(nameof(Product), id);
            }

            if (product.ProductName != string.Empty) obj.ProductName = product.ProductName; 
            if(product.Code != string.Empty) obj.Code = product.Code;
            obj.ProductImage = product.ProductImage;

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

        public List<Product> GetAll()
        {
            return _productRepository.GetAll();
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

        public Product GetByCode(string code)
        {
            var obj = _productRepository.GetByCode(code);

            if (obj == null)
            {
                throw new NotFoundException(nameof(Product), code);
            }

            else
            {
                return obj;
            }
        }
    }
}
