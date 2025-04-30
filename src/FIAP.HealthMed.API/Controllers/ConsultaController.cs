using FIAP_HealthMed.Application.Interface;
using FIAP_HealthMed.Application.Model.Consulta;
using FIAP_HealthMed.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace FIAP_HealthMed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultaController : ControllerBase
    {
        private readonly IConsultaApplicationService _consultaService;

        public ConsultaController(IConsultaApplicationService consultaService)
        {
            _consultaService = consultaService;
        }

        /// <summary>
        /// Agendar uma nova consulta
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ConsultaModelRequest request)
        {
            var result = await _consultaService.AgendarConsulta(request);
            return Ok(result);
        }

        /// <summary>
        /// Obter consultas por usuário
        /// </summary>
        [HttpGet("usuario/{usuarioId}")]
        public async Task<IActionResult> Get(int usuarioId, [FromQuery] Role role)
        {
            var result = await _consultaService.ObterConsultas(usuarioId, role);
            return Ok(result);
        }

        /// <summary>
        /// Cancelar uma consulta
        /// </summary>
        [HttpPut("cancelar/{id}")]
        public async Task<IActionResult> Cancelar(int id, [FromBody] string justificativa)
        {
            var result = await _consultaService.CancelarConsulta(id, justificativa);
            return Ok(result);
        }

        /// <summary>
        /// Aceitar uma consulta
        /// </summary>
        [HttpPut("aceitar/{id}")]
        public async Task<IActionResult> Aceitar(int id)
        {
            var result = await _consultaService.AceitarConsulta(id);
            return Ok(result);
        }

        /// <summary>
        /// Recusar uma consulta
        /// </summary>
        [HttpPut("recusar/{id}")]
        public async Task<IActionResult> Recusar(int id)
        {
            var result = await _consultaService.RecusarConsulta(id);
            return Ok(result);
        }
    }
}
