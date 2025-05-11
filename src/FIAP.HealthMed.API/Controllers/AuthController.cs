using FIAP_HealthMed.Application.Interfaces;
using FIAP_HealthMed.Application.Model.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.HealthMed.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthApplicationService _authService;

        public AuthController(IAuthApplicationService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Efetuar login do usuário.
        /// </summary>
        /// <param name="request">Modelo contendo as credenciais de login do usuário.</param>
        /// <returns>Retorna um token JWT caso as credenciais sejam válidas.</returns>
        [HttpPost("loginUsuario")]
        public async Task<IActionResult> Login([FromBody] AuthLoginModelRequest request)
        {
            try
            {
                var token = await _authService.AutenticarLoginAsync(request);
                return Ok(token);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro: " + ex.Message);
            }          
        }
    }
}
