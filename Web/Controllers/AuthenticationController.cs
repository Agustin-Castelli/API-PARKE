using Application.Interfaces;
using Application.Models;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IConfiguration config, IAuthenticationService authenticationService)
        {
            _config = config;
            _authenticationService = authenticationService;
        }

        [HttpPost("[action]")]
        public IActionResult Authenticate([FromBody] CredentialsRequest credentials)
        {
            try
            {
                string token = _authenticationService.Authentication(credentials);
                return Ok(token);
            }
            catch (UnauthorizedException ex)
            {
                // Captura la excepción y devuelve un estado 401 con el mensaje  
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Manejo genérico de excepciones  
                return StatusCode(500, new { message = "An internal server error occurred." });
            }
        }
    }
}
