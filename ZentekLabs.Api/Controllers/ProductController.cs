using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZentekLabs.Models.Domain;
using ZentekLabs.Models.Dtos.Request;
using ZentekLabs.Repositories.Interfaces;

namespace ZentekLabs.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public ProductController(IProductRepository productRepository,
            IMapper mapper
            )
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }


        //fetch all products
        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetAllProducts([FromQuery] GetProductRequest request)
        {
            var result = await productRepository.GetAllProductsAsync(request);
            if (result.Sucesss)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        //create a product
        [HttpPost("CreateProduct")]
        public async Task<IActionResult> AddProduct([FromBody] CreateProductRequestDto product)
        {
            var productDomain = mapper.Map<Product>(product);

            var result = await productRepository.AddProductAsync(productDomain);
            if (result.Sucesss)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        //fetch a product by id
        [HttpGet("GetProductById/{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var result = await productRepository.GetProductByIdAsync(id);
            if (result.Sucesss)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
