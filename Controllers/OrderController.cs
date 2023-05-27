using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OrganicAPI.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _service;

        public OrderController(ILogger<OrderController> logger, IOrderService service)
        {
            _logger = logger;
            _service = service;
        }

        [Authorize]
        [HttpGet("all")]
        public async Task<ActionResult<ServiceResponse<List<GetOrderDto>>>> GetAllOrders()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var id = identity.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
            int parsedId;
            if(id != null && int.TryParse(id.Value, out parsedId)) 
            {
                return await _service.GetAllOrders(parsedId);
            }
            return BadRequest();
        }

        [Authorize]
        [HttpGet("orderById")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<ActionResult<ServiceResponse<List<GetProductDto>>>> GetOrderById(int orderId)
        {
            return Ok(await _service.GetOrder(orderId));
        }

        [Authorize]
        [HttpPost("create")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<ActionResult<ServiceResponse<string>>> CreateOrder(CreateOrderDto orderDto)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var id = identity.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
            int parsedId;
            if(id != null && int.TryParse(id.Value, out parsedId)) 
            {
                return Ok(await _service.CreateOreder(orderDto, parsedId));
            }
            return BadRequest();
        }

        [Authorize]
        [HttpGet("myUserOrders")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<ActionResult<ServiceResponse<GetUserOrderDto>>> GetMyUserOrders()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var id = identity.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
            int parsedId;
            if(id != null && int.TryParse(id.Value, out parsedId)) 
            {
                return Ok(await _service.GetMyUserOrders(parsedId));
            }
            return BadRequest();
        }

        [Authorize]
        [HttpGet("mySupplierOrders")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<ActionResult<ServiceResponse<List<GetUserOrderDto>>>> GetMySupplierOrders() {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var id = identity.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
            int parsedId;
            if(id != null && int.TryParse(id.Value, out parsedId)) 
            {
                return Ok(await _service.GetMySupplierOrders(parsedId));
            }
            return BadRequest();
        }
    }
}