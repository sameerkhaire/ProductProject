using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAuth2._0.DTO_s;
using MyAuth2._0.IServices;
using System.Data;

namespace MyAuth2._0.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAllHeaders")]
    [ApiController]
    public class AssignRoleController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected ResponseDto _response;

        public AssignRoleController(IAuthService authService)
        {
            _authService = authService;
            _response = new();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("Rolesroyal")]

        public async Task<ActionResult> Rolesroyal([FromBody] RegisterationRequestDto model)
        {
            var assignRole = await _authService.AssignRole(model.Email, model.Role);
            if (!assignRole)
            {
                _response.IsSuccess = false;
                _response.Message = "Error Encountered";
                return BadRequest(_response);
            }

            return Ok(_response);
        }
    }
}
