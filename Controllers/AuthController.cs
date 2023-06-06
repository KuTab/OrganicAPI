using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OrganicAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        
        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<ActionResult<ServiceResponse<LoginDto>>> Register(UserRegisterDto request)
        {
            var response = await _service.Register(new Model.User {Email = request.Email, isSupplier = request.isSupplier}, request.Password);
            if (!response.Success) 
            {
                var errorResponse = new ServiceResponse<LoginDto>();
                errorResponse.Data = new LoginDto();
                errorResponse.Data.IsSupplier = false;
                errorResponse.Data.Token = string.Empty;
                errorResponse.Success = false;
                errorResponse.Message = response.Message;
                return BadRequest(errorResponse);
            }
            var newResponse = await _service.Login(request.Email, request.Password);
            return Ok(newResponse);
        }

        [HttpPost("login")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<ActionResult<ServiceResponse<LoginDto>>> Login(UserLoginDto request)
        {
            var response = await _service.Login(request.Email, request.Password);
            if(!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}