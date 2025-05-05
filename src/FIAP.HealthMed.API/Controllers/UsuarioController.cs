using FIAP_HealthMed.Application.Interface;
using FIAP_HealthMed.Application.Model.Auth;
using FIAP_HealthMed.Application.Model.Usuario;
using FIAP_HealthMed.Domain.Entity;

using Microsoft.AspNetCore.Mvc;

namespace FIAP_HealthMed.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioApplicationService _usuarioApplicationService;

        public UsuarioController(IUsuarioApplicationService usuarioApplicationService)
        {
            _usuarioApplicationService = usuarioApplicationService;
        }


        /// <summary>
        /// Cadastrar novo usuário
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Usuario cadastrado com sucesso</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Erro ao cadastrar usuário</response>
        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] UsuarioModelRequest request)
        {
            try
            {
                var result = await _usuarioApplicationService.CadastrarAsync(request);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Efetuar login do usuário
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Usuario cadastrado com sucesso</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Erro ao cadastrar usuário</response>
        [HttpPost("login")]
        public async Task<IActionResult> EfetuarLogin([FromBody] AuthLoginModelRequest request)
        {
            try
            {                
                return Ok(await _usuarioApplicationService.EfetuarLoginAsync(request));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPost("{usuarioId}/especialidades")]
        public async Task<IActionResult> AdicionarEspecialidades(int usuarioId,[FromBody] UsuarioEspecialidadeModelRequest request)
        {
            var result = await _usuarioApplicationService.InserirEspecialidadesUsuarioAsync(usuarioId, request.EspecialidadeIds);
            
            return Ok(new { message = result });
        }

        /// <summary>
        /// Listar médicos por especialidade (caso receba uma especialidade)
        /// </summary>
        /// <param name="especialidade">Nome da especialidade (caso tenha especialidade)</param>
        /// <returns>Lista de médicos</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Nenhum médico encontrado</response>
        [HttpGet("medicos")]
        public async Task<IActionResult> ListarMedicos([FromQuery] int? especialidadeId)
        {
            var result = await _usuarioApplicationService.ListarMedicos(especialidadeId);
            return result.Any() ? Ok(result) : NotFound();
        }

        /// <summary>
        /// Obter usuário por ID
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <returns>Dados do usuário</returns>
        /// <response code="200">Usuário encontrado</response>
        /// <response code="404">Usuário não encontrado</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var result = await _usuarioApplicationService.ObterPorId(id);
            return result is null ? NotFound() : Ok(result);
        }       
    }
}
