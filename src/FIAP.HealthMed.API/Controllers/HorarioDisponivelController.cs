using FIAP_HealthMed.Application.Interface;
using FIAP_HealthMed.Application.Model;
using Microsoft.AspNetCore.Mvc;

namespace FIAP_HealthMed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorarioDisponivelController : ControllerBase
    {
        private readonly IHorarioDisponivelApplicationService _horarioService;

        public HorarioDisponivelController(IHorarioDisponivelApplicationService horarioService)
        {
            _horarioService = horarioService;
        }

        /// <summary>
        /// Obter horários disponíveis de um médico
        /// </summary>
        [HttpGet("{medicoId}")]
        public async Task<IActionResult> ObterHorarios(int medicoId)
        {
            var result = await _horarioService.ObterHorarios(medicoId);
            return result.Any() ? Ok(result) : NotFound();
        }

        /// <summary>
        /// Cadastrar horários disponíveis para um médico
        /// </summary>
        [HttpPost("{medicoId}")]
        public async Task<IActionResult> CadastrarHorarios(int medicoId, [FromBody] List<DateTime> horarios)
        {
            await _horarioService.CadastrarHorarios(medicoId, horarios);
            return NoContent();
        }

        /// <summary>
        /// Editar um horário disponível
        /// </summary>
        [HttpPut("{horarioId}")]
        public async Task<IActionResult> EditarHorario(int horarioId, [FromBody] DateTime novoHorario)
        {
            await _horarioService.EditarHorario(horarioId, novoHorario);
            return NoContent();
        }

        /// <summary>
        /// Remover um horário disponível
        /// </summary>
        [HttpDelete("{horarioId}")]
        public async Task<IActionResult> RemoverHorario(int horarioId)
        {
            await _horarioService.RemoverHorario(horarioId);
            return NoContent();
        }
    }
}
