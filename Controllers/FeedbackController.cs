using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OrganicAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/feedback")]
    public class FeedbackController : ControllerBase
    {
        private readonly ILogger<FeedbackController> _logger;
        private readonly IFeedbackService _service;

        public FeedbackController(ILogger<FeedbackController> logger, IFeedbackService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("getSupplierFeedback")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<ActionResult<ServiceResponse<List<SupplierFeedbackDto>>>> GetSupplierFeedback(int id)
        {
            return Ok(await _service.GetSupplierFeedback(id));
        }

        [HttpGet("getProductFeedback")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<ActionResult<ServiceResponse<List<ProductFeedbackDto>>>> GetProductFeedback(int id)
        {
            return Ok(await _service.GetProductFeedback(id));
        }

        [HttpPost("postProductFeedback")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<ActionResult<ServiceResponse<string>>> SendProductFeedback(AddProductFeedbackDto feedback)
        {
            return Ok(await _service.SendProductFeedback(feedback));
        }

        [HttpPost("postSupplierFeedback")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<ActionResult<ServiceResponse<string>>> SendSupplierFeedback(AddSupplierFeedbackDto feedback)
        {
            return Ok(await _service.SendSupplierFeedback(feedback));
        }
    }
}