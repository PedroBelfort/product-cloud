using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll() { 
        
             var products = await productService.GetAllAsync();
            return Ok(products);

        }


        [HttpPost]
        public async Task<ActionResult<string>> CreateProduct(ProductDTO product)
        {
            return await productService.CreateProductAsync(product);
        }
    }
}
