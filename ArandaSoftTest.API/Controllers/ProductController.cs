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
using System.IO;
using Microsoft.AspNetCore.Hosting;
using ArandaSoftTest.CORE.QueryFilters;
using Newtonsoft.Json;
using ArandaSoftTest.CORE.CustomEntities;
using ArandaSoftTest.INFRASTRUCTURE.Interfaces;

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
        private readonly IWebHostEnvironment _env;
        private readonly IUriService _uriService;

        /* 
         * Inyección de dependencias
         */
        public ProductController(
            IProductService productService, 
            IMapper mapper, 
            IWebHostEnvironment env,
            IUriService uriService
        )
        {
            _productService = productService;
            _mapper = mapper;
            _env = env;
            _uriService = uriService;
        }

        [HttpGet(Name = nameof(GetProduct))]
        public async Task<IActionResult> GetProduct([FromQuery]ProductQueryFilter filters)
        {
            var products =  _productService.GetProducts(filters);
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);

            var metadata = new MetaData
            {
                TotalCount = products.TotalCount,
                PageSize = products.PageSize,
                CurrentPage = products.CurrentPage,
                TotalPages = products.TotalPages,
                HasNextPage = products.HasNextPage,
                HasPreviusPage = products.HasPreviusPage,
                NextPageNumber = products.NextPageNumber,
                PreviusPageNumber = products.PreviusPageNumber,
                NextPageUrl = _uriService.GetProductPaginationUri(filters, Url.RouteUrl(nameof(GetProduct))).ToString(),
                PreviusPageUrl = ""
            };

            var response = new ApiResponse<IEnumerable<ProductDto>>(productsDto)
            {
                Meta = metadata
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
        }

        [HttpGet]
        [Route("[action]")]
        [Route("api/Product/getCategories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories=  _productService.GetCategories();

            var response = new ApiResponse<IEnumerable<Category>>(categories);
            return Ok(response);
        }

        [HttpGet]
        [Route("[action]")]
        [Route("api/Product/productByCategoryId")]
        public async Task<IActionResult> ProductByCategoryId(int id)
        {
            var products = await _productService.GetProductsByCategoryId(id);
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

        [HttpGet]
        [Route("[action]")]
        [Route("api/Product/productById")]
        public async Task<IActionResult> ProductById(int id)
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

        [Route("UploadImage")]
        [HttpPost]
        public JsonResult UploadImage()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/images/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception ex)
            {
                return new JsonResult("No upload image product - ArandaSoftTest");
            }
        }
    }
}
