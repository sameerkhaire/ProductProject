using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAuth2._0.DTO_s;
using MyAuth2._0.IServices;
using System.Data;

namespace MyAuth2._0.Controllers
{
    [Route("api/Auth")]
    [EnableCors("AllowAllHeaders")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected ResponseDto _response;
        
        public AuthController(IAuthService authService)
        {
            _authService = authService;
            _response = new();
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterationRequestDto model)
        {
            var errormessage = await _authService.Register(model);
            if (!string.IsNullOrEmpty(errormessage))
            {
                _response.IsSuccess = false;
                _response.Message = errormessage;
                return BadRequest(_response);
            }
            return Ok();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginRequestDto model)
        {
            var loginRespo = await _authService.Login(model);
            if (loginRespo.userDto==null)
            {
                _response.IsSuccess = false;
                _response.Message = "User password is incorrect";
                return BadRequest(_response);
            }
            _response.Result = loginRespo;
            return Ok(_response);
        }

        

    }
}
