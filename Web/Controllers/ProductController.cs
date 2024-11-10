using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "RequireEmployeeRole")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        [Authorize(Policy = "RequireAdminRole")]
        public IActionResult Create([FromBody] ProductCreateRequest product)
        {
            try
            {
                var obj = _productService.Create(product);

                return Ok(obj);
            }
            catch (DuplicateElementException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        [HttpDelete("[action]/{id}")]
        [Authorize(Policy = "RequireAdminRole")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                _productService.Delete(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("[action]/{id}")]
        [Authorize(Policy = "RequireAdminRole")]
        public IActionResult Update([FromRoute] int id, [FromBody] ProductUpdateRequest product)
        {
            try
            {
                _productService.Update(id, product);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("[action]/{id}")]
        public ActionResult<Product> GetById([FromRoute] int id)
        {
            try
            {
                return _productService.GetById(id);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpGet("[action]")]
        public ActionResult<Product> GetByName([FromQuery] string name)
        {
            try
            {
                return _productService.GetByName(name);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("[action]")]
        public ActionResult<Product> GetByCode([FromQuery] string code)
        {
            try
            {
                return _productService.GetByCode(code);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("[action]")]
        public ActionResult<List<Product>> GetAll()
        {
            return _productService.GetAll();
        }
    }
}
