using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OrganicAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/chat")]
    public class ChatRoomController : ControllerBase
    {
        private readonly ILogger<ChatRoomController> _logger;
        private readonly IChatService _service;

        public ChatRoomController(ILogger<ChatRoomController> logger, IChatService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost("createChat")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<ActionResult<ServiceResponse<int>>> CreateChat(int receiverId) {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var id = identity.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
            var role = identity.Claims.Where(c => c.Type == ClaimTypes.Role).FirstOrDefault();
            int parsedId;
            bool isSupplier;
            if(id != null && int.TryParse(id.Value, out parsedId)) 
            {
                if(role != null && bool.TryParse(role.Value, out isSupplier)) {
                    if (isSupplier) {
                        return Ok(await _service.CreateChat(receiverId, parsedId));
                    } else {
                        return Ok(await _service.CreateChat(parsedId, receiverId));
                    }
                }
            }
            return BadRequest();
        }

        [HttpGet("getChat")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<ActionResult<ServiceResponse<int>>> GetChat(int receiverId) {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var id = identity.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
            var role = identity.Claims.Where(c => c.Type == ClaimTypes.Role).FirstOrDefault();
            int parsedId;
            bool isSupplier;
            if(id != null && int.TryParse(id.Value, out parsedId)) 
            {
                if(role != null && bool.TryParse(role.Value, out isSupplier)) {
                    if (isSupplier) {
                        return Ok(await _service.GetChat(receiverId, parsedId));
                    } else {
                        return Ok(await _service.GetChat(parsedId, receiverId));
                    }
                }
            }
            return BadRequest();
        }

        [HttpGet("getMyChats")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<ActionResult<ServiceResponse<List<GetChatDto>>>> GetMyChats()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var id = identity.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
            var role = identity.Claims.Where(c => c.Type == ClaimTypes.Role).FirstOrDefault();
            int parsedId;
            bool isSupplier;
            if(id != null && int.TryParse(id.Value, out parsedId)) 
            {
                if(role != null && bool.TryParse(role.Value, out isSupplier)) {
                    return Ok(await _service.GetMyChats(parsedId, isSupplier));
                }
            }
            return BadRequest();
        }
    }
}