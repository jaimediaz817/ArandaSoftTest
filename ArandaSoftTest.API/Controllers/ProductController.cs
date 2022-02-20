using ArandaSoftTest.API.Responses;
using ArandaSoftTest.CORE.DTOs;
using ArandaSoftTest.CORE.Entities;
using ArandaSoftTest.CORE.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArandaSoftTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        // Repositorio de productos a través de la interfaz
        private readonly IProductService _productService;

        // Obteniendo el mapper
        private readonly IMapper _mapper;

        /* 
         * Inyección de dependencias
         */
        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {
            var products = await _productService.GetProducts();
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);

            var response = new ApiResponse<IEnumerable<ProductDto>>(productsDto);
            /*var productsDto = products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Image = p.Image,
                CategoryId = p.CategoryId
            });*/

            return Ok(response);
        }

        [HttpGet("{name}")]
        [Route("[action]")]
        [Route("api/Product/getProductByName")]
        public async Task<IActionResult> GetProductByName(string name)
        {
            var products = await _productService.GetProductByName(name);
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);

            var response = new ApiResponse<IEnumerable<ProductDto>>(productsDto);
            /*var productsDto = products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Image = p.Image,
                CategoryId = p.CategoryId
            });*/

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productService.GetProductById(id);
            var productDto = _mapper.Map<ProductDto>(product);

            var response = new ApiResponse<ProductDto>(productDto);

            /*var productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Image = product.Image,
                CategoryId = product.CategoryId
            };*/

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Product(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);

            /*var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Image = productDto.Image,
                CategoryId = productDto.CategoryId
            };*/


            // Regla de negocio ARANDASOFT: evidenciar manejo/gestión de errores
            try
            {
                await _productService.InsertProduct(product);
            }
            catch(Exception ex)
            {
                var responseErr = new ApiResponse<Exception>(ex);
                return BadRequest(responseErr);
            }
            
            productDto = _mapper.Map<ProductDto>(product);

            var response = new ApiResponse<ProductDto>(productDto);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            product.Id = id;

            var result = await _productService.UpdateProduct(product);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _productService.DeleteProduct(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
