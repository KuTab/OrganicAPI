using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OrganicAPI.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _service;

        private List<String> categories = new List<string> 
        {
            "Фрукты", "Овощи", "Молочная продукция", "Напитки", "Мясо", "Выпечка", "Сладости"
        };

        public ProductController(ILogger<ProductController> logger, IProductService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("all")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<ActionResult<ServiceResponse<List<GetProductDto>>>> GetAllProducts() {
            return Ok(await _service.GetAllProducts());
        }

        [Authorize]
        [HttpGet("my")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<ActionResult<ServiceResponse<List<GetProductDto>>>> GetMyProducts() {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if(identity == null)
            {
                return BadRequest();
            }
            var id = identity.Claims.Where(c=> c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            var parsedId = int.Parse(id);

            return Ok(await _service.GetMyProducts(parsedId));
        }

        [Authorize]
        [HttpPost("add")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<ActionResult<ServiceResponse<string>>> AddProduct(AddProductDto newProduct) {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if(identity == null)
            {
                return BadRequest();
            }
            var id = identity.Claims.Where(c=> c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            var parsedId = int.Parse(id);
            var role = identity.Claims.Where(c => c.Type == ClaimTypes.Role).FirstOrDefault();

            if(role == null)
            {
                return BadRequest();
            }

            bool isSupplier = bool.Parse(role.Value);

            if(isSupplier)
            {
                return Ok(await _service.AddProduct(newProduct, parsedId));
            }
            return BadRequest(new ServiceResponse<string> {
                Success = false,
                Message = "Not enough rights"
            });
        } 

        //[Authorize]
        [HttpGet("categories")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<ActionResult<ServiceResponse<List<String>>>> GetCategories() {
            var response = new ServiceResponse<List<String>>();
            response.Data = categories;
            response.Success = true;
            return Ok(response);
        }

        [Authorize]
        [HttpGet("byCategory")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<ActionResult<ServiceResponse<List<GetProductDto>>>> GetProductsByCategory(string category) {
            return Ok(await _service.GetProductsByCategory(category));
        }

        [Authorize]
        [HttpPut("addQuantity")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<ActionResult<ServiceResponse<string>>> AddQuantity( int productId) {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var id = identity.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
            int parsedId;
            if(id != null && int.TryParse(id.Value, out parsedId)) 
            {
                 return Ok(await _service.AddQuantity(parsedId, productId));
            }
            return BadRequest();
        }
    }
}