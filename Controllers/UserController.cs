using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OrganicAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _service;
        public UserController(ILogger<UserController> logger, IUserService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPut("updateMe")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<ActionResult<ServiceResponse<string>>> UpdateInfo(UpdateUserDto newUser)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var id = identity.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
            int parsedId;
            if(id != null && int.TryParse(id.Value, out parsedId)) 
            {
                newUser.Id = parsedId;
                return Ok(await _service.UpdateUser(newUser));
            }
            return BadRequest();
        }

        [HttpGet("getMe")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> GetMe()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var id = identity.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
            int parsedId;
            if(id != null && int.TryParse(id.Value, out parsedId)) 
            {
                return Ok(await _service.GetMe(parsedId));
            }
            return BadRequest();
        }
    }
}